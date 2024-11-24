using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrackSO", menuName = "Scriptable Objects/MusicTrackSO")]
public class MusicTrackSO : ScriptableObject
{
    public AudioClip track;
    public float tempo;
    public List<Note> notes;
}
