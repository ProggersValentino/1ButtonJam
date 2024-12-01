using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    
    public List<MusicTrackSO> musicTracks;
    private MusicTrackSO currentTrack;
    public float delayInSecs;
    private float countDown;

    public TMP_Text countDownUI;

    public bool startPlaying;
    public NoteSpawner NS;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (countDownUI)
        {
            countDown = delayInSecs;
            countDownUI.text = (Mathf.CeilToInt(countDown)).ToString(); 
        }
        
        if (musicTracks.Count > 0)
        {
            CueTrack();
        }
        else
        {
            Debug.Log("no tracks");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            startPlaying = true;
            NS.hasStarted = true;

            music.PlayDelayed(delayInSecs);
        }
        else if (!music.isPlaying)
        {
            //cue next track
            startPlaying = false;
            
            if (musicTracks.Count > 0)
            {
                CueTrack();
            }
            else
            {
                SceneManager.LoadScene("EndScene");
            }
        }

        if (countDownUI)
        {
            countDown = countDown - Time.deltaTime;
            if (countDown >= 0)
            {
                countDownUI.text = (Mathf.CeilToInt(countDown)).ToString();
            }
            else
            {
                countDownUI.gameObject.SetActive(false);
            }
        }

        //dev cheat
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("EndScene"); //prematurely load end scene
        }*/
    }

    private void CueTrack()
    {
        currentTrack = musicTracks[0];
        musicTracks.Remove(musicTracks[0]); //dequeue
        
        music.clip = currentTrack.track;
        NS.currentTempo = currentTrack.tempo; 
        NS.sections = currentTrack.sections;
    }

}
