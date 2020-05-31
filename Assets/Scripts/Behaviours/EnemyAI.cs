// Credits to Rob Pearson
// https://forum.unity.com/threads/free-simple-ai-behavioral-script.89543

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class EnemyAi : MonoBehaviour
{

    private const float Antigravity = 2;

    private readonly float _gravity = Physics.gravity.y;

    private CharacterController _characterController;

    private bool _enemyCanAttack;

    private bool _enemyIsAttacking;

    private int _estCheckDirection;

    private float _estGravityTimer;

    private float _estHeight;

    private bool _executeBufferState;

    private bool _go = true;

    private bool _initialGo;

    private float _lastShotFired;

    private Vector3 _lastVisTargetPos;

    private float _lostPlayerTimer;

    private bool _monitorRunTo;

    private bool _pauseWpControl;

    private bool _playerHasBeenSeen;

    private Vector3 _randomDirection;

    private float _randomDirectionTimer;

    private bool _smoothAttackRangeBuffer;

    private bool _targetIsOutOfSight;

    private bool _walkInRandomDirection;

    private bool _wpCountdown;

    private int _wpPatrol;

    public float attackRange = 10.0f;

    public float attackTime = 0.50f;

    public bool canFly;

    public bool estimateElevation;

    public float estRayTimer = 1.0f;

    public float floatHeight;

    public float huntingTimer = 5.0f;

    public float moveableRadius = 20;

    public bool on = true;

    public bool pauseAtWaypoints;

    public float pauseMax = 3;

    public float pauseMin = 1;

    public int randomSpeed = 7;

    public bool requireTarget = true;

    public bool reversePatrol = true;

    public float rotationSpeed = 20;

    public bool runAway;

    public float runBufferDistance = 50;

    public float runDistance = 25;

    public int runSpeed = 6;

    public bool runTo = true;

    public Transform target;

    public bool useWaypoints;

    public float visualRadius = 100;

    public int walkSpeed = 6;

    public Transform[] waypoints;

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        if (estimateElevation && floatHeight > 0.0f)
            _estGravityTimer = Time.time;
        _characterController = gameObject.GetComponent<CharacterController>();
        _initialGo = true;
        yield return null;
    }

    private void Update()
    {
        if (!on || !_initialGo)
            return;
        AiFunctionality();
    }

    private void AiFunctionality()
    {
        if (!target && requireTarget)
            return;
        var tPos = target.position;
        _lastVisTargetPos = tPos;
        var cPos = transform.position;
        var moveToward = _lastVisTargetPos - cPos;
        var moveAway = cPos - _lastVisTargetPos;
        var distance = Vector3.Distance(cPos, tPos);
        if (_go)
            MonitorGravity();
        if (!requireTarget) { Patrol(); }
        else if (TargetIsInSight())
        {
            if (!_go)
                return;
            if (distance > attackRange && !runAway && !runTo)
            {
                _enemyCanAttack = false;
                MoveTowards(moveToward);
            }
            else if (_smoothAttackRangeBuffer && distance > attackRange + 5.0f)
            {
                _smoothAttackRangeBuffer = false;
                WalkNewPath();
            }
            else if ((runAway || runTo) && distance > runDistance && !_executeBufferState)
            {
                if (_monitorRunTo)
                    _monitorRunTo = false;
                if (runAway)
                    WalkNewPath();
                else
                    MoveTowards(moveToward);
            }
            else if ((runAway || runTo) && distance < runDistance && !_executeBufferState)
            {
                _enemyCanAttack = false;
                if (!_monitorRunTo)
                    _executeBufferState = true;
                _walkInRandomDirection = false;
                MoveTowards(runAway ? moveAway : moveToward);
            }
            else if (_executeBufferState && runAway && distance < runBufferDistance || runTo && distance > runBufferDistance) { MoveTowards(runAway ? moveAway : moveToward); }
            else if (_executeBufferState && (runAway && distance > runBufferDistance || runTo && distance < runBufferDistance))
            {
                _monitorRunTo = true;
                _executeBufferState = false;
            }
            if (!(distance < attackRange) && (runAway || runTo || !(distance < runDistance)))
                return;
            if (runAway)
                _smoothAttackRangeBuffer = true;
            if (Time.time > _lastShotFired + attackTime)
                StartCoroutine(Attack());
        }
        else if (_playerHasBeenSeen && !_targetIsOutOfSight && _go)
        {
            _lostPlayerTimer = Time.time + huntingTimer;
            StartCoroutine(HuntDownTarget(_lastVisTargetPos));
        }
        else if (useWaypoints) { Patrol(); }
        else if (!_playerHasBeenSeen && _go && (Math.Abs(moveableRadius) < 1 || distance < moveableRadius)) { WalkNewPath(); }
    }

    private IEnumerator Attack()
    {
        _enemyCanAttack = true;
        if (_enemyIsAttacking)
            yield break;
        _enemyIsAttacking = true;
        while (_enemyCanAttack)
        {
            _lastShotFired = Time.time;
            yield return new WaitForSeconds(attackTime);
        }
    }

    private bool TargetIsInSight()
    {
        if (moveableRadius > 0 && Vector3.Distance(transform.position, target.position) > moveableRadius)
            _go = false;
        else
            _go = true;
        if (visualRadius > 0 && Vector3.Distance(transform.position, target.position) > visualRadius)
            return false;
        if (!Physics.Linecast(transform.position, target.position, out var sight))
            return false;
        if (!_playerHasBeenSeen && sight.transform == target)
            _playerHasBeenSeen = true;
        return sight.transform == target;
    }

    private IEnumerator HuntDownTarget(Vector3 position)
    {
        _targetIsOutOfSight = true;
        while (_targetIsOutOfSight)
        {
            var moveToward = position - transform.position;
            MoveTowards(moveToward);
            if (TargetIsInSight())
            {
                _targetIsOutOfSight = false;
                break;
            }
            if (Time.time > _lostPlayerTimer)
            {
                _targetIsOutOfSight = false;
                _playerHasBeenSeen = false;
                break;
            }
            yield return null;
        }

    }

    private void Patrol()
    {
        if (_pauseWpControl)
            return;
        var destination = CurrentPath();
        var position = transform.position;
        var moveToward = destination - position;
        var distance = Vector3.Distance(position, destination);
        MoveTowards(moveToward);
        if (!(distance <= 1.5f + floatHeight))
            return;
        if (pauseAtWaypoints)
        {
            if (_pauseWpControl)
                return;
            _pauseWpControl = true;
            StartCoroutine(WaypointPause());
        }
        else { NewPath(); }
    }

    private IEnumerator WaypointPause()
    {
        yield return new WaitForSeconds(Random.Range(pauseMin, pauseMax));
        NewPath();
        _pauseWpControl = false;
    }

    private Vector3 CurrentPath()
    {
        return waypoints[_wpPatrol].position;
    }

    private void NewPath()
    {
        if (!_wpCountdown)
        {
            _wpPatrol++;
            if (_wpPatrol < waypoints.GetLength(0))
                return;
            if (reversePatrol)
            {
                _wpCountdown = true;
                _wpPatrol -= 2;
            }
            else { _wpPatrol = 0; }
        }
        else if (reversePatrol)
        {
            _wpPatrol--;
            if (_wpPatrol >= 0)
                return;
            _wpCountdown = false;
            _wpPatrol = 1;
        }

    }

    private void WalkNewPath()
    {
        if (!_walkInRandomDirection)
        {
            _walkInRandomDirection = true;
            _randomDirection = !_playerHasBeenSeen ? new Vector3(Random.Range(-0.15f, 0.15f), 0, Random.Range(-0.15f, 0.15f)) : new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            _randomDirectionTimer = Time.time;
        }
        else if (_walkInRandomDirection) { MoveTowards(_randomDirection); }
        if (Time.time - _randomDirectionTimer > 2)
            _walkInRandomDirection = false;
    }

    private void MoveTowards(Vector3 direction)
    {
        direction.y = 0;
        var speed = walkSpeed;
        if (_walkInRandomDirection)
            speed = randomSpeed;
        if (_executeBufferState)
            speed = runSpeed;
        Transform trans;
        (trans = transform).rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        trans.eulerAngles = new Vector3(0, trans.eulerAngles.y, 0);
        var forward = transform.TransformDirection(Vector3.forward);
        var speedModifier = Vector3.Dot(forward, direction.normalized);
        speedModifier = Mathf.Clamp01(speedModifier);
        direction = forward * (speed * speedModifier);
        if (!canFly && floatHeight <= 0.0f)
            direction.y -= _gravity;
        _characterController.Move(direction * Time.deltaTime);

    }

    private void MonitorGravity()
    {
        var direction = new Vector3(0, 0, 0);
        if (!canFly && floatHeight > 0.0f)
        {
            if (estimateElevation && estRayTimer > 0.0f)
            {
                if (Time.time > _estGravityTimer)
                {
                    if (Physics.Raycast(transform.position, -Vector3.up, out var floatCheck))
                    {
                        if (floatCheck.distance < floatHeight - 0.5f)
                        {
                            _estCheckDirection = 1;
                            _estHeight = floatHeight - floatCheck.distance;
                        }
                        else if (floatCheck.distance > floatHeight + 0.5f)
                        {
                            _estCheckDirection = 2;
                            _estHeight = floatCheck.distance - floatHeight;
                        }
                        else { _estCheckDirection = 3; }
                    }
                    else
                    {
                        _estCheckDirection = 2;
                        _estHeight = floatHeight * 2;
                    }
                    _estGravityTimer = Time.time + estRayTimer;
                }
                switch (_estCheckDirection)
                {
                    case 1:
                        direction.y += Antigravity;
                        _estHeight -= direction.y * Time.deltaTime;
                        break;
                    case 2:
                        direction.y -= _gravity;
                        _estHeight -= direction.y * Time.deltaTime;
                        break;
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, -Vector3.up, out var floatCheck, floatHeight + 1.0f))
                {
                    if (floatCheck.distance < floatHeight)
                        direction.y += Antigravity;
                }
                else { direction.y -= _gravity; }
            }
        }
        else
        {
            if (estimateElevation && estRayTimer > 0.0f)
            {
                if (Time.time > _estGravityTimer)
                {
                    if (Physics.Raycast(transform.position, -Vector3.up, out var floatCheck))
                    {
                        if (floatCheck.distance < floatHeight - 0.5f)
                        {
                            _estCheckDirection = 1;
                            _estHeight = floatHeight - floatCheck.distance;
                        }
                        else if (floatCheck.distance > floatHeight + 0.5f)
                        {
                            _estCheckDirection = 2;
                            _estHeight = floatCheck.distance - floatHeight;
                        }
                        else { _estCheckDirection = 3; }
                    }
                    _estGravityTimer = Time.time + estRayTimer;
                }
                switch (_estCheckDirection)
                {
                    case 1:
                        direction.y += Antigravity;
                        _estHeight -= direction.y * Time.deltaTime;
                        break;
                    case 2:
                        direction.y -= Antigravity;
                        _estHeight -= direction.y * Time.deltaTime;
                        break;
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, -Vector3.up, out var floatCheck))
                {
                    if (floatCheck.distance < floatHeight - 0.5f)
                        direction.y += Antigravity;
                    else if (floatCheck.distance > floatHeight + 0.5f)
                        direction.y -= Antigravity;
                }

            }

        }
        if (!estimateElevation || estimateElevation && _estHeight >= 0.0f)
            _characterController.Move(direction * Time.deltaTime);
    }

}