using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    
    public List<MusicTrackSO> musicTracks;
    public MusicTrackSO currentTrack;

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
            if (Input.GetKeyDown(KeyCode.W))
            {
                startPlaying = true;
                NS.hasStarted = true;
                
                music.Play();
            }
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
                Debug.Log("end game");
            }
        }
    }

    private void CueTrack()
    {
        currentTrack = musicTracks[0];
        musicTracks.Remove(musicTracks[0]); //dequeue
        
        music.clip = currentTrack.track;
        NS.currentTempo = currentTrack.tempo;
        NS.notes = currentTrack.notes;
    }
}
