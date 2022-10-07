using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    [SerializeField] Gun gun;

    private PlayerInput playerInput; //New private value, referencing to PlayerInput class
    public PlayerInput.OnFootActions onFoot; //Reference to OnfootActions map
    
    private PlayerMotor motor; //Property for player motor script
    private PlayerLook look;

    [SerializeField] private int playerIndex;

    public int PlayerIndex { get => playerIndex; }



    //Start is called before the first frame update
    void Awake() 
    {
        playerInput = new PlayerInput(); //Creating new INSTANCE of class PlayerInput(one unique version of this PI-template which contains values) and naming it playerInput
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump(); // Read about Events. Callback context to jump function /performed/started/canceled

        onFoot.Shoot.performed += _ => gun.Shoot();
        

    }


    
   
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (TurnManager.GetInstance().IsItPlayerTurn(playerIndex))
        {
            
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

            //tell the playermotor to move using the VALUE from the movement action

        }
    }
    private void LateUpdate()
    {
        if (TurnManager.GetInstance().IsItPlayerTurn(playerIndex))
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

        }
    }
    private void OnEnable() //When object becomes active=call Enable method
    {
        onFoot.Enable();
    }
    private void OnDisable()  //When object turns deactive=call Disable method
    {
        onFoot.Disable();
    }
}
