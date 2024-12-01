using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "Scriptable Objects/NoteData")]
public class NoteData : ScriptableObject
{
    public Sprite sprite;
    public Sprite deathSprite;
    public AnimationClip animation;
    public AudioClip sfx;
}

