using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuNote : Note
{
    [HideInInspector] public int sceneIndex;
    [SerializeField] private TextMeshProUGUI tm;


    public void SetText(){
        switch (sceneIndex) {
            case 0: 
                tm.text = "Gameplay";
                break;
            case 1:
                tm.text = "MainMenu";
                break;
            case 2:
                tm.text = "Quit";
                break;
        }
    }

    public void DeathNote(float disCalc)
    {
        Debug.Log(disCalc);
        if (Mathf.Abs(disCalc) > 1.0f){
            base.DeathNote();
        } else {
            if (tm.text == "Quit"){
                Application.Quit();
                EditorApplication.isPlaying = false;
            }
            Debug.Log(tm.text);
            SceneManager.LoadScene(tm.text);
        }
        
    }

}