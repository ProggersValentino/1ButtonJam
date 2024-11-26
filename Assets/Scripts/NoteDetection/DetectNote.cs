using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// the purpose of this script is to update keep track and process notes that go in zone
/// as well as execute behaviours on the notes depending on the player's reaction
/// </summary>
public class DetectNote : MonoBehaviour
{
    [CanBeNull] Note currentNote = null;


    private void OnEnable()
    {
        NoteEventSystem.NoteIsInZone += DoWeHaveNote; //adding method to our chain so that when the event is called this is called
        PlayerEventSystem.HitNote += ProcessNoteKill;
        PlayerEventSystem.MissNote += ProcessNoteKill;
    }

    private void OnDisable()
    {
        //prevent from having memory leaks
        NoteEventSystem.NoteIsInZone -= DoWeHaveNote;
        PlayerEventSystem.HitNote -= ProcessNoteKill;
        PlayerEventSystem.MissNote -= ProcessNoteKill;
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
            if(currentNote) currentNote.DeathNote(); //player missed note
            
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
        //testing async calc allowing calculation to be done on another thread
        Task<float> disCalc = DetermineNoteDisFromCentre();
        
        await disCalc; 
        
        Debug.Log($"our distance from centre of zone is {disCalc.Result}");
        
        ScoreEventSystem.OnUpdateScore(disCalc.Result, currentNote); //transmitting the necessary data for calculating & updating the score
        
        currentNote?.DeathNote(); //kill note
    }

    /// <summary>
    /// this takes the current pos and the current notes pos and spits out a float of distance
    /// </summary>
    /// <returns>float of distance between centre of zone and current note</returns>
    public Task<float> DetermineNoteDisFromCentre()
    {
        float disFromCentre = Vector2.Distance(transform.position, currentNote.transform.position);
        
        //getting the direction of note last pos
        Vector2 direction = (currentNote.transform.position - transform.position).normalized;
        
        disFromCentre *= direction.x; //apply directional factor to get the direction the note was when the player tapped
        
        
        return Task.FromResult(disFromCentre);
    }
}
