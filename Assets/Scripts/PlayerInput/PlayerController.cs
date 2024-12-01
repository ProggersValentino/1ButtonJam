using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private PlayerInput pi;
    private InputAction uno;


    private bool swatAnimationFinishing;
    
    public AnimationCurve animationCurve;
    
    public GameObject flySwat;
    public AnimationClip flySwatAnim;
    public AnimationClip flySwatIdle;
    Animator flySwatAnimator;
    public GameObject bugSpray;
    
    public enum CurrentPlayerObject {FlySwat, BugSpray}

    private GameObject currentObject;
    
    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
        uno = pi.actions.FindAction("Uno");

        Debug.LogWarning($"uno {uno.name}");
        
        flySwatAnimator = flySwat.GetComponent<Animator>();
        
        //if we want to differentiate what type the player pressed with both activating than we must separate tap and hold into two different callbacks
        uno.started += context =>
        {
            if (context.interaction is HoldInteraction)
            {
                if (NoteEventSystem.OnNoteIsInZone() is Hold)
                {
                  
                }
                DetermineSwatOrSpray(CurrentPlayerObject.BugSpray);
                
                PlayerEventSystem.OnHoldNoteActivate(true);
            }
            
        };

        uno.performed += context =>
        {
            
            //if(context.interaction is MultiTap)
            
            if (context.interaction is TapInteraction)
            {
                Debug.LogWarning("tapped in");
                
                DetermineSwatOrSpray(CurrentPlayerObject.FlySwat); //select fly swat 
                
                //determine if we have the right type of note in zone
                if (NoteEventSystem.OnNoteIsInZone() is Tap)
                {
                    swatAnimationFinishing = true;
                    
                    AnimateFlySwat();
                }
                
                else Debug.LogWarning($"wrong input for note");

            }
        };
        
    }

    private void Update()
    {
        if (uno.WasReleasedThisFrame())
        {
            PlayerEventSystem.OnHoldNoteActivate(false);
        }
    }

    public void DetermineSwatOrSpray(CurrentPlayerObject playerObject)
    {
        if(currentObject) currentObject.SetActive(false);
        switch (playerObject)
        {
            case CurrentPlayerObject.FlySwat:
                currentObject = flySwat;
                currentObject.SetActive(true);
                break;
            case CurrentPlayerObject.BugSpray:
                currentObject = bugSpray;
                currentObject.SetActive(true);
                break;
        }
    }

    public async void AnimateFlySwat()
    {
        float minusEarly = 0.2f * 1000;
        
        //start animation
        flySwatAnimator.speed = 1f;
        //flySwatAnim.SetCurve("", typeof(Sprite), "m_Sprite" , animationCurve);
        flySwatAnimator.Play(flySwatAnim.name);
        
        
        float time = (flySwatAnim.length - 0.2f) * (1000 - minusEarly);
        
        Debug.LogWarning($"flySwat {time}");
        
        await Task.Delay((int)time); //await for animation to finish

        swatAnimationFinishing = true;
        
        PlayerEventSystem.OnTapNote();
        
        await Task.Delay((int)minusEarly);
        
        Debug.LogWarning($"waited");

        //go back to default
        flySwatAnimator.speed = 10f;
        flySwatAnimator.Play(flySwatIdle.name);
    }
    
   
}
