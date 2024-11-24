using System;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// the purpose of this script is to update keep track and process notes that go in zone
/// as well as execute behaviours on the notes depending on the player's reaction
/// </summary>
public class DetectNote : MonoBehaviour
{
    /*bool */
    
    [CanBeNull] Note currentNote = null;


    private void Awake()
    {
        NoteEventSystem.NoteIsInZone += DoWeHaveNote; //adding method to our chain so that when the event is called this is called
        PlayerEventSystem.HitNote += KillCurrentNote;
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
    public void KillCurrentNote()
    {
        currentNote?.DeathNote(); //kill note
    }
}
