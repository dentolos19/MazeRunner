using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController _controller;

    public float speed = 10;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var thing = transform;
        var move = thing.right * x + thing.forward * z;
        _controller.Move(move * (speed * Time.deltaTime));
    }

}