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
    public float multi = 2;
    public TMP_Text countDownUI;

    public bool startPlaying;
    public NoteSpawner NS;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            if (countDownUI)
            {
                countDown = delayInSecs;
                countDownUI.text = (Mathf.CeilToInt(countDown)).ToString();
            }
            startPlaying = true;
            NS.hasStarted = true;
            Debug.Log(countDown.ToString() + " " + delayInSecs.ToString());
            music.PlayDelayed(delayInSecs);

        }
        else if (!music.isPlaying)
        {

            if (countDownUI)
            {
                countDown -= Time.deltaTime;
                if (countDown >= 0)
                {
                    countDownUI.text = (Mathf.CeilToInt(countDown)).ToString();
                }
                else
                {
                    countDownUI.gameObject.SetActive(false);
                }
            }
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
        NS.currentTempo = currentTrack.tempo * multi; 
        NS.multi = multi;
        NS.sections = currentTrack.sections;
    }

}
