using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
 {
    public override void OnInspectorGUI() //this function is called everytime Unity updates the editor interface
    { 
        Interactable interactable = (Interactable)target; //target is accessable due to the inheritance of Editor. "target" is the currently selected gameobject that is inspected. Casting to interactable is neccessary.
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents",MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }

        }
        else
        {
        base.OnInspectorGUI();
        if(interactable.useEvents)//if useEvents=add component.
        {
            if(interactable.GetComponent<InteractionEvent>() == null) //Null-check. So it doesnt add several. 
                interactable.gameObject.AddComponent<InteractionEvent>();
        }
        else 
        
            if(interactable.GetComponent<InteractionEvent>() != null) //if not null means that component is already there
            {
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
 }

