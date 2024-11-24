using System;
using UnityEngine;

/// <summary>
/// the purpose of this script to act as an intermedier between the note and whatever other scripts it needs to access or
/// vice versa
/// </summary>
public class NoteEventSystem : MonoBehaviour
{
    public static event Func<Note> NoteIsInZone;


    public static Note OnNoteIsInZone() => NoteIsInZone?.Invoke(); //call event, if null default return false
}
