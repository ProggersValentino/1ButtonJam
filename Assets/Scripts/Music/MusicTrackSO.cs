using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrackSO", menuName = "Scriptable Objects/MusicTrackSO")]
public class MusicTrackSO : ScriptableObject
{
    public AudioClip track;
    public float tempo;
    public float[] noteSpaces; //seconds between each note
    public float[] noteTypes; //0 for tap and 1 for hold
}
