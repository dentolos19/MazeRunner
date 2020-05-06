// Credits to Rob Pearson
// https://forum.unity.com/threads/free-simple-ai-behavioral-script.89543

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyAi : MonoBehaviour
{

    private readonly float _antigravity = 2.0f; //force at which floating/flying enemies repel

    public float attackRange = 10.0f; //How close does the enemy need to be in order to attack?

    public float attackTime = 0.50f; //How frequent or fast an enemy can attack (cool down time).

    public bool canFly; //Flying alters float behavior to ignore gravity. The enemy will fly up or down only to sustain floatHeight level.

    private CharacterController _characterController; //CC used for enemy movement and etc.

    private bool _enemyCanAttack; //Used to determine if the enemy is within range to attack, regardless of moving or not.

    private bool _enemyIsAttacking; //An attack interuption method.

    private int _estCheckDirection; //used to determine if AI is falling or not when estimating elevation.

    private float _estGravityTimer; //floating/flying creatures using estimated elevation will use this to actually monitor time values.

    private float _estHeight; //floating/flying creatures using estimated elevation use this to estimate height necessities and gravity impacts.

    public bool estimateElevation; //This implements a pause between raycasts for heights and guestimates the need to move up/down in height based on the previous raycast.

    public float estRayTimer = 1.0f; //The amount of time in seconds between raycasts for gravity and elevation checks.

    private bool _executeBufferState; //Smooth AI buffer for runAway AI. Also used as a speed control variable.

    public float floatHeight; //If it can fly/hover, you need to let the AI know how high off the ground it should be.

    private bool _go = true; //An on/off override variable

    private readonly float gravity = 20.0f; //force of gravity pulling the enemy down.

    public float huntingTimer = 5.0f; //Search for player timer in seconds. Minimum of 0.1

    //private script handled variables

    private bool _initialGo; //AI cannot function until it is initialized.

    private float _lastShotFired; //Used in conjuction with attackTime to monitor attack durations.

    private Vector3 _lastVisTargetPos; //Monitor target position if we lose sight of target. provides semi-intelligent AI.

    private float _lostPlayerTimer; //Used for hunting down the player.

    private bool _monitorRunTo; //when AI is set to runTo, they will charge in, and then not charge again to after far enough away.

    public float moveableRadius = 200.0f; //If the player is too far away, the AI will auto-matically shut down. Set to 0 to remove this limitation.
    
    public bool on = true; //Is the AI active? this can be used to place pre-set enemies in you scene.

    public bool pauseAtWaypoints; //if true, patrol units will pause momentarily at each waypoint as they reach them.

    public float pauseMax = 3.0f; //If pauseAtWaypoints is true, the unit will pause momentarily formaximum of this time.

    public float pauseMin = 1.0f; //If pauseAtWaypoints is true, the unit will pause momentarily for minmum of this time.

    private bool _pauseWpControl; //makes sure unit pauses appropriately.

    private bool _playerHasBeenSeen; //An enhancement to how the AI functions prior to visibly seeing the target. Brings AI to life when target is close, but not visible.

    private Vector3 _randomDirection; //Random movement behaviour setting.

    private float _randomDirectionTimer; //Random movement behaviour tracking.

    public int randomSpeed = 10; //Movement speed if the AI is moving in random directions.

    public bool requireTarget = true; //Waypoint ONLY functionality (still can fly and hover).

    public bool reversePatrol = true; //if true, patrol units will walk forward and backward along their patrol.

    public float rotationSpeed = 20.0f; //Rotation during movement modifier. If AI starts spinning at random, increase this value. (First check to make sure it's not due to visual radius limitations)

    public bool runAway; //Is it the goal of this AI to keep it's distance? If so, it needs to have runaway active.

    public float runBufferDistance = 50.0f; //Smooth AI buffer. How far apart does AI/Target need to be before the run reason is ended.

    public float runDistance = 25.0f; //If the enemy should keep its distance, or charge in, at what point should they begin to run?

    public int runSpeed = 15; //Movement speed if it needs to run.

    public bool runTo; //Opposite to runaway, within a certain distance, the enemy will run toward the target.

    private bool _smoothAttackRangeBuffer; //for runAway AI to not be so messed up by their visual radius and attack range.

    public Transform target; //The target, or whatever the AI is looking for.

    private bool _targetIsOutOfSight; //Player tracking overload prevention. Makes sure we do not call the same coroutines over and over.

    public bool useWaypoints;

    public float visualRadius = 100.0f;

    private bool _walkInRandomDirection;

    public int walkSpeed = 10;

    public Transform[] waypoints;

    private bool _wpCountdown;

    private int _wpPatrol;

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

    //---Main Functionality---//

    private void Update()
    {

        if (!on || !_initialGo)
            return;
        AIFunctionality();

    }

    private void AIFunctionality()
    {

        if (!target && requireTarget)
            return; //if no target was set and we require one, AI will not function.

        //Functionality Updates

        _lastVisTargetPos = target.position; //Target tracking method for semi-intelligent AI

        var moveToward = _lastVisTargetPos - transform.position; //Used to face the AI in the direction of the target

        var moveAway = transform.position - _lastVisTargetPos; //Used to face the AI away from the target when running away

        var distance = Vector3.Distance(transform.position, target.position);

        if (_go)
            MonitorGravity();

        if (!requireTarget)
        {

            //waypoint only functionality

            Patrol();

        }
        else if (TargetIsInSight())
        {

            if (!_go) //useWaypoints is false and the player has exceeded moveableRadius, shutdown AI until player is near.

                return;

            if (distance > attackRange && !runAway && !runTo)
            {

                _enemyCanAttack = false; //the target is too far away to attack

                MoveTowards(moveToward); //move closer

            }
            else if (_smoothAttackRangeBuffer && distance > attackRange + 5.0f)
            {

                _smoothAttackRangeBuffer = false;

                WalkNewPath();

            }
            else if ((runAway || runTo) && distance > runDistance && !_executeBufferState)
            {

                //move in random directions.

                if (_monitorRunTo)
                    _monitorRunTo = false;

                if (runAway)
                    WalkNewPath();
                else
                    MoveTowards(moveToward);

            }
            else if ((runAway || runTo) && distance < runDistance && !_executeBufferState)
            {
                //make sure they do not get too close to the target

                //AHH! RUN AWAY!...  or possibly charge :D

                _enemyCanAttack = false; //can't attack, we're running!

                if (!_monitorRunTo)
                    _executeBufferState = true; //smooth buffer is now active!

                _walkInRandomDirection = false; //obviously we're no longer moving at random.

                if (runAway)
                    MoveTowards(moveAway); //move away
                else
                    MoveTowards(moveToward); //move toward

            }
            else if (_executeBufferState && runAway && distance < runBufferDistance || runTo && distance > runBufferDistance)
            {

                //continue to run!

                if (runAway)
                    MoveTowards(moveAway); //move away
                else
                    MoveTowards(moveToward); //move toward

            }
            else if (_executeBufferState && (runAway && distance > runBufferDistance || runTo && distance < runBufferDistance))
            {

                _monitorRunTo = true; //make sure that when we have made it to our buffer distance (close to user) we stop the charge until far enough away.

                _executeBufferState = false; //go back to normal activity

            }

            //start attacking if close enough

            if (distance < attackRange || !runAway && !runTo && distance < runDistance)
            {

                if (runAway)
                    _smoothAttackRangeBuffer = true;

                if (Time.time > _lastShotFired + attackTime)
                    StartCoroutine(Attack());

            }

        }
        else if (_playerHasBeenSeen && !_targetIsOutOfSight && _go)
        {

            _lostPlayerTimer = Time.time + huntingTimer;

            StartCoroutine(HuntDownTarget(_lastVisTargetPos));

        }
        else if (useWaypoints) { Patrol(); }
        else if (!_playerHasBeenSeen && _go && (moveableRadius == 0 || distance < moveableRadius))
        {

            //the idea here is that the enemy has not yet seen the player, but the player is fairly close while still not visible by the enemy

            //it will move in a random direction continuously altering its direction every 2 seconds until it does see the player.

            WalkNewPath();

        }

    }

    //attack stuff...

    private IEnumerator Attack()
    {

        _enemyCanAttack = true;

        if (!_enemyIsAttacking)
        {

            _enemyIsAttacking = true;

            while (_enemyCanAttack)
            {

                _lastShotFired = Time.time;

                //implement attack variables here

                yield return new WaitForSeconds(attackTime);

            }

        }

    }

    //----Helper Functions---//

    //verify enemy can see the target

    private bool TargetIsInSight()
    {

        //determine if the enemy should be doing anything other than standing still

        if (moveableRadius > 0 && Vector3.Distance(transform.position, target.position) > moveableRadius)
            _go = false;
        else
            _go = true;

        //then lets make sure the target is within the vision radius we allowed our enemy

        //remember, 0 radius means to ignore this check

        if (visualRadius > 0 && Vector3.Distance(transform.position, target.position) > visualRadius)
            return false;

        //Now check to make sure nothing is blocking the line of sight

        RaycastHit sight;

        if (Physics.Linecast(transform.position, target.position, out sight))
        {

            if (!_playerHasBeenSeen && sight.transform == target)
                _playerHasBeenSeen = true;

            return sight.transform == target;

        }
        return false;

    }

    //target tracking

    private IEnumerator HuntDownTarget(Vector3 position)
    {

        //if this function is called, the enemy has lost sight of the target and must track him down!

        //assuming AI is not too intelligent, they will only move toward his last position, and hope they see him

        //this can be fixed later to update the lastVisTargetPos every couple of seconds to leave some kind of trail

        _targetIsOutOfSight = true;

        while (_targetIsOutOfSight)
        {

            var moveToward = position - transform.position;

            MoveTowards(moveToward);

            //check if we found the target yet

            if (TargetIsInSight())
            {

                _targetIsOutOfSight = false;

                break;

            }

            //check to see if we should give up our search

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

        var moveToward = destination - transform.position;

        var distance = Vector3.Distance(transform.position, destination);

        MoveTowards(moveToward);

        if (distance <= 1.5f + floatHeight)
        {
            // || (distance < floatHeight+1.5f)) {

            if (pauseAtWaypoints)
            {

                if (!_pauseWpControl)
                {

                    _pauseWpControl = true;

                    StartCoroutine(WaypointPause());

                }

            }
            else { NewPath(); }

        }

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

            if (_wpPatrol >= waypoints.GetLength(0))
            {

                if (reversePatrol)
                {

                    _wpCountdown = true;

                    _wpPatrol -= 2;

                }
                else { _wpPatrol = 0; }

            }

        }
        else if (reversePatrol)
        {

            _wpPatrol--;

            if (_wpPatrol < 0)
            {

                _wpCountdown = false;

                _wpPatrol = 1;

            }

        }

    }

    //random movement behaviour

    private void WalkNewPath()
    {

        if (!_walkInRandomDirection)
        {

            _walkInRandomDirection = true;

            if (!_playerHasBeenSeen)
                _randomDirection = new Vector3(Random.Range(-0.15f, 0.15f), 0, Random.Range(-0.15f, 0.15f));
            else
                _randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

            _randomDirectionTimer = Time.time;

        }
        else if (_walkInRandomDirection) { MoveTowards(_randomDirection); }

        if (Time.time - _randomDirectionTimer > 2) //choose a new random direction after 2 seconds

            _walkInRandomDirection = false;

    }

    //standard movement behaviour

    private void MoveTowards(Vector3 direction)
    {

        direction.y = 0;

        var speed = walkSpeed;

        if (_walkInRandomDirection)
            speed = randomSpeed;

        if (_executeBufferState)
            speed = runSpeed;

        //rotate toward or away from the target

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //slow down when we are not facing the target

        var forward = transform.TransformDirection(Vector3.forward);

        var speedModifier = Vector3.Dot(forward, direction.normalized);

        speedModifier = Mathf.Clamp01(speedModifier);

        //actually move toward or away from the target

        direction = forward * speed * speedModifier;

        if (!canFly && floatHeight <= 0.0f)
            direction.y -= gravity;

        _characterController.Move(direction * Time.deltaTime);

    }

    //continuous gravity checks

    private void MonitorGravity()
    {

        var direction = new Vector3(0, 0, 0);

        if (!canFly && floatHeight > 0.0f)
        {

            //we need to make sure our enemy is floating.. using evil raycasts! bwahahahah!

            if (estimateElevation && estRayTimer > 0.0f)
            {

                if (Time.time > _estGravityTimer)
                {

                    RaycastHit floatCheck;

                    if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck))
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

                        direction.y += _antigravity;

                        _estHeight -= direction.y * Time.deltaTime;

                        break;

                    case 2:

                        direction.y -= gravity;

                        _estHeight -= direction.y * Time.deltaTime;

                        break;

                }

            }
            else
            {

                RaycastHit floatCheck;

                if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck, floatHeight + 1.0f))
                {

                    if (floatCheck.distance < floatHeight)
                        direction.y += _antigravity;

                }
                else { direction.y -= gravity; }

            }

        }
        else
        {

            //bird like creature! Again with the evil raycasts! :p

            if (estimateElevation && estRayTimer > 0.0f)
            {

                if (Time.time > _estGravityTimer)
                {

                    RaycastHit floatCheck;

                    if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck))
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

                        direction.y += _antigravity;

                        _estHeight -= direction.y * Time.deltaTime;

                        break;

                    case 2:

                        direction.y -= _antigravity;

                        _estHeight -= direction.y * Time.deltaTime;

                        break;

                }

            }
            else
            {

                RaycastHit floatCheck;

                if (Physics.Raycast(transform.position, -Vector3.up, out floatCheck))
                {

                    if (floatCheck.distance < floatHeight - 0.5f)
                        direction.y += _antigravity;
                    else if (floatCheck.distance > floatHeight + 0.5f)
                        direction.y -= _antigravity;

                }

            }

        }

        if (!estimateElevation || estimateElevation && _estHeight >= 0.0f)
            _characterController.Move(direction * Time.deltaTime);

    }

}