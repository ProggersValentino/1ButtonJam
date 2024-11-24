using System;
using UnityEngine;

/// <summary>
/// the purpose of this script to act as an intermedier between the player controller and whatever other scripts it needs to access or
/// vice versa
/// </summary>
public class PlayerEventSystem : MonoBehaviour
{
    public static event Action HitNote;
    public static event Action MissNote;

    
    public static void OnHitNote() => HitNote?.Invoke();
    public static void OnMissNote() => MissNote?.Invoke();

}
