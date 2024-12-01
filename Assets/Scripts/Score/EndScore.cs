using TMPro;
using UnityEngine;

public class EndScore : MonoBehaviour
{
    public TMP_Text score;
    private ScoreUI scoreUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>();
        if (scoreUI)
        {
            score.text = "Score\n" + scoreUI.currentScore;
            scoreUI.gameObject.SetActive(false); 
        }
    }

}
