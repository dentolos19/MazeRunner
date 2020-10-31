using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 5;
    
    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var motion = transform.right * horizontal + transform.forward * vertical;
        if (controller != null)
            controller.Move(motion * (speed * Time.deltaTime));
    }
    
}