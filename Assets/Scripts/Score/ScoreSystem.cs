using System;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// the purpsoe of this script to calculate the score of the player 
/// </summary>
public class ScoreSystem : MonoBehaviour
{
    
    public enum TimingType {Late, Perfect, Early}
    TimingType timingType;

    public float overallScore {get{return score;}
        private set { value = score; }
    }
    private float score; 
    
    [Tooltip("The base arbutary number that is tied to the multipliers when the player taps")]
    public float tapScoreBase;
    
    [Tooltip("The base arbutary number that is tied to the multipliers when the player holds")]
    public float holdScoreBase;
    
    [Header("Multipliers")]
    
    float currentSetMultiplier = 0.0f; //the current multiplier that is set
    
    [Range(0f, 5f)]
    public float perfectMultiplier;
    
    [Range(0f, 5f)]
    public float earlyMultiplier;
    
    [Range(0f, 5f)]
    public float lateMultiplier;


    private void OnEnable()
    {
        ScoreEventSystem.UpdateScore += CalculateNewScore;
        ScoreEventSystem.RetrieveScore += GetScore;
        ScoreEventSystem.RetriveGradeType += GetTimingType;
    }

    private void OnDisable()
    {
        ScoreEventSystem.UpdateScore -= CalculateNewScore;
        ScoreEventSystem.RetrieveScore -= GetScore;
        ScoreEventSystem.RetriveGradeType -= GetTimingType;
    }

    public void CalculateNewScore(float rawdisValue, Note noteType)
    {
        timingType = DetermineTimingOfNoteHit(rawdisValue);
        
        Debug.LogWarning($"Raw disValue: {rawdisValue} timing type: {timingType}");
        
        //setting score values depending on the type of note executed
        if(noteType is Tap) score += tapScoreBase * currentSetMultiplier;
        else score += holdScoreBase * currentSetMultiplier;
        
        Debug.LogWarning($"score: {score} timing type: {timingType}"); 
        
        //update UI
        ScoreEventSystem.OnUpdateUI(); //update UI of score
    }
    
    /// <summary>
    /// to determine how well the player timed their tap
    /// </summary>
    /// <param name="distanceFromZone">the raw value from DetectNote script</param>
    /// <returns></returns>
    public TimingType DetermineTimingOfNoteHit(float distanceFromZone)
    {
        switch (distanceFromZone)
        {
            case > 0.2f: //Early
                currentSetMultiplier = earlyMultiplier;
                return TimingType.Early;
            case < -0.2f: //Late
                currentSetMultiplier = lateMultiplier;
                return TimingType.Late;
            default: //Perfect
                currentSetMultiplier = perfectMultiplier;
                return TimingType.Perfect;
        }
    }

    public float GetScore()
    {
        return overallScore;
    }

    public TimingType GetTimingType()
    {
        return timingType;
    }
}
