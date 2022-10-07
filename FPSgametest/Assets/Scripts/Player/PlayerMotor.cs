using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    private CharacterController controller;
    private Vector3 playerVelocity; 
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f; //'Default'
    public float jumpHeight = 3f;
    [SerializeField] public float movementDuration = 10;
    public float currentMoveTurnTime;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); //Assign reference value?? to controller
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    public void ProcessMove(Vector2 input) //Method that receives the input for the Inputmanager script(?)and apply to the character controller
    {

        currentMoveTurnTime += Time.deltaTime; //1 unit per second

        if (currentMoveTurnTime <= movementDuration)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
            playerVelocity.y += gravity * Time.deltaTime; //Adding constant downwoard force even when grounded
            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;
            controller.Move(playerVelocity * Time.deltaTime); //?

        }
        if (TurnManager.GetInstance().currentTurnTime >= 14.9f)
        {
            currentMoveTurnTime = 0;
        }




    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
}
