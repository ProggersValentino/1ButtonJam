using System;
using Unity.Burst;
using UnityEngine;

public class ScoreEventSystem : MonoBehaviour
{
    public static event Action<float, Note> UpdateScore;
    public static event Func<float> RetrieveScore; 
    public static event Func<ScoreSystem.TimingType> RetriveGradeType; 
    public static event Action UpdateUI;
    
    public static void OnUpdateScore(float score, Note note) => UpdateScore?.Invoke(score, note);
    public static float OnRetrieveScore() => RetrieveScore?.Invoke() ?? 0f;
    public static ScoreSystem.TimingType OnRetrieveGradeType() => RetriveGradeType?.Invoke() ?? ScoreSystem.TimingType.Perfect;
    public static void OnUpdateUI() => UpdateUI?.Invoke();
}
