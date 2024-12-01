using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;

/// <summary>
/// the purpose of this script is to update keep track and process notes that go in zone
/// as well as execute behaviours on the notes depending on the player's reaction
/// </summary>
public class DetectNote : MonoBehaviour
{
    [CanBeNull] Note currentNote = null;

    private bool onHoldActivated;

    private Note nextPotentialNote;
    
    private void OnEnable()
    {
        NoteEventSystem.NoteIsInZone += DoWeHaveNote; //adding method to our chain so that when the event is called this is called
        PlayerEventSystem.TapNote += ProcessNoteKill;
        PlayerEventSystem.MissNote += ProcessNoteKill;
        PlayerEventSystem.HoldNoteActivate += HoldActivated;
    }

    private void OnDisable()
    {
        //prevent from having memory leaks
        NoteEventSystem.NoteIsInZone -= DoWeHaveNote;
        PlayerEventSystem.TapNote -= ProcessNoteKill;
        PlayerEventSystem.MissNote -= ProcessNoteKill;
        PlayerEventSystem.HoldNoteActivate -= HoldActivated;
    }

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log(other.gameObject.name);
        //if we detect any object that falls under the note classes including children then do this
        if (other.TryGetComponent<Note>(out Note note))
        {
           // if(currentNote) currentNote.DeathNote(); //player missed note
            
            Debug.Log($"the note is{note}");
            currentNote = note; //set note
        }
    }

    //note missed
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Note>(out Note note))
        {
            PlayerEventSystem.OnMissNote();
            currentNote = null;
        }   
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (onHoldActivated && currentNote is Hold) 
        {
            Debug.LogWarning($"we are currently holding {onHoldActivated}");
            
            //stop note
            Hold holdNote = (Hold)currentNote;
            
            currentNote.areStopping = true;
            
            holdNote.TakeHealth(250f);
        }
        else
        {
            currentNote.areStopping = false;
        }
    }

    private void Update()
    {
        if (onHoldActivated)
        {
            Debug.LogWarning($"we are currently holding {onHoldActivated}");
        }
    }

    /// <summary>
    /// takes in the currentNote and determines if there is a current note in zone
    /// </summary>
    /// <returns>true or false</returns>
    public Note DoWeHaveNote()
    {
        if(currentNote != null) return currentNote;
        
        return null;
    }

    
    /// <summary>
    /// Kills the note in the zone
    /// </summary>
    public async void ProcessNoteKill()
    {
        Note targetNote = currentNote;
        
        //testing async calc allowing calculation to be done on another thread
        Task<float> disCalc = DetermineNoteDisFromCentre(targetNote);
        
        await disCalc; 
        
        Debug.Log($"our distance from centre of zone is {disCalc.Result}");

        
        
        ScoreEventSystem.OnUpdateScore(disCalc.Result, currentNote); //transmitting the necessary data for calculating & updating the score

        if (targetNote.TryGetComponent<MenuNote>(out MenuNote note)){
            note.DeathNote(disCalc.Result);
        } else {
            targetNote?.DeathNote();
        }
    }

    /// <summary>
    /// this takes the current pos and the current notes pos and spits out a float of distance
    /// </summary>
    /// <returns>float of distance between centre of zone and current note</returns>
    public Task<float> DetermineNoteDisFromCentre(Note noteToInput)
    {
        float disFromCentre = Vector2.Distance(transform.position, currentNote.transform.position);
        
        //getting the direction of note last pos
        Vector2 direction = (currentNote.transform.position - transform.position).normalized;
        
        disFromCentre *= direction.x; //apply directional factor to get the direction the note was when the player tapped
        
        
        return Task.FromResult(disFromCentre);
    }

    public void HoldActivated(bool isActivated)
    {
        onHoldActivated = isActivated;
    }

    /// <summary>
    /// whats the next note in line
    /// </summary>
    /// <returns></returns>
    public Note GetNextNote()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(currentNote.transform.position, Vector2.right, 10f);
        
        
        foreach (RaycastHit2D hit in hits)
        {
        
            Debug.DrawLine(this.transform.position, 20f * Vector2.right, Color.red, 0.5f);
            Debug.DrawLine(this.transform.position, hit.point, Color.green, 0.5f);

            if (hit.collider.TryGetComponent<Note>(out Note note))
            {
                Debug.Log($"the next note of this is {note.name}");
                return note;
            }    
            
        }
        
        Debug.LogWarning($"UnsuccessfulHit");
        return null;
    }

    public float GetNoteDisFromCentre()
    {
        Note potentialNote = GetNextNote();
        Debug.LogWarning($"the next note is {nextPotentialNote.name}");
        return Vector2.Distance(transform.position, potentialNote.transform.position);
    }
}
