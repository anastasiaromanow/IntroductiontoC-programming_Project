using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField] private GameObject door; 
    private bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //in this function i design the interaction using code
    protected override void Interact()
    {
        doorOpen = !doorOpen; //toggle true/false
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen); 
    }
}
