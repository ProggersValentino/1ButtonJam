using System;
using TMPro;
//using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    public TextMeshProUGUI perfectGradeTxt;
    public TextMeshProUGUI earlyGradeTxt;
    public TextMeshProUGUI lateGradeTxt;

    public float currentScore;

    private void OnEnable()
    {
        ScoreEventSystem.UpdateUI += UpdateScoreTxt;
        ScoreEventSystem.UpdateUI += ShowGradeTxt;
    }

    private void OnDisable()
    {
        ScoreEventSystem.UpdateUI -= UpdateScoreTxt;
        ScoreEventSystem.UpdateUI -= ShowGradeTxt;
    }

    /// <summary>
    /// updates the score UI
    /// </summary>
    public void UpdateScoreTxt()
    {
        currentScore = ScoreEventSystem.OnRetrieveScore(); //retrieving score from scoreSystem
        
        scoreText.text = currentScore.ToString();
    }


    public async void ShowGradeTxt()
    {
        ScoreSystem.TimingType grade = ScoreEventSystem.OnRetrieveGradeType();
        
        TextMeshProUGUI selectedGradeTxt = DetermineGradeTxt(grade);
        
        selectedGradeTxt.gameObject.SetActive(true);

        await Task.Delay(2000); // 2 seconds delay
        if(selectedGradeTxt != null)
            selectedGradeTxt.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// determine what grading text to show based off the timing of player
    /// </summary>
    /// <param name="gradeType"></param>
    /// <returns></returns>
    public TextMeshProUGUI DetermineGradeTxt(ScoreSystem.TimingType gradeType)
    {
        switch (gradeType)
        {
            case ScoreSystem.TimingType.Early:
                return earlyGradeTxt;
            case ScoreSystem.TimingType.Late:
                return lateGradeTxt;
            case ScoreSystem.TimingType.Perfect:
                return perfectGradeTxt;
        }
        
        return null;
    }
}
