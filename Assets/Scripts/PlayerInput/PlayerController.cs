using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private PlayerInput pi;
    private InputAction uno;

    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
        uno = pi.actions.FindAction("Uno");

        Debug.LogWarning($"uno {uno.name}");
        
        //if we want to differentiate what type the player pressed than we must separate tap and hold into two different callbacks
        uno.started += context =>
        { 
            if(context.interaction is HoldInteraction) Debug.LogWarning("held in");
            
        };

        uno.performed += context =>
        {
            if (context.interaction is TapInteraction)
            {
                Debug.LogWarning("tapped in");

                //determine if we have a note in zone
                if (NoteEventSystem.OnNoteIsInZone())
                {
                    //execute tap note stuff
                    PlayerEventSystem.OnHitNote();
                }

            }
        };




    }

    public void OnBtnPressed(ContextCallback callback)
    {
        
    }
}
