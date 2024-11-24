using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "Scriptable Objects/NoteData")]
public class NoteData : ScriptableObject
{
    public Sprite sprite;
    public Animation animation;
    public AudioClip sfx;
}
