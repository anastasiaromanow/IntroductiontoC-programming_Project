using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public bool useEvents;//Add/remove an InteractionEvent component to this gameobject


    //attribute w message that is displayed to player when looking at an interactable object
 [SerializeField] public string promptMessage; 

    //this function will be called from the player
    public void BaseInteract()
    {
       
        {
            if (useEvents)
                GetComponent<InteractionEvent>().OnInteract.Invoke();
            Interact();
        }
    }

    protected virtual void Interact()
    {
        //No code written in this function
        //Its a template function to be overridden by the subclasses
    }
}
