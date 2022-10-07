using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script will contain all of the logic to detect interactables and handle players input

public class PlayerInteract : MonoBehaviour
{
    //Reference to camera
    private Camera cam;
    //How far the ray is gonna go
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); //Message get cleared when not looking at interactable
        //create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance); //make it visable
        RaycastHit hitInfo; //variable to store collision information
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) //raycasting to the center of the scene, bool if hit something
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)//checking if our gameobject has a interactable component
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();//if it has, then storing interactable in a variable
                playerUI.UpdateText(interactable.promptMessage);//storing it in on screen text
                if (inputManager.onFoot.Interact.triggered)//when triggering the interaction(E)
                {
                    interactable.BaseInteract(); //Run interact function (in keypad-script)
                }
            }
        }
    }
}
