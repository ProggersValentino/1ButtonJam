using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<Note> notes;
    public bool hasStarted;
    public float currentTempo;
    private float rnd;

    private float timeElapsed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //notes = new List<Note>();
        rnd = Random.Range(0.1f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            //temporary code for testing
            if (Time.time > timeElapsed + rnd)
            {
                if (notes.Count > 0)
                {
                    SpawnNote(notes[0]);
                    RemoveNote(notes[0]);
                }
                else
                {
                    Debug.Log("out of notes");
                }
            
                rnd = Random.Range(0.1f, 2);
                timeElapsed = Time.time + rnd;
            }
        }
    }

    public void SpawnNote(Note note)
    {
        Note temp = Instantiate(note, this.gameObject.transform);
        temp.tempo = currentTempo;
    }

    public void AddNote(Note note)
    {
        notes.Add(note);
    }

    public void RemoveNote(Note note)
    {
        notes.Remove(note);
    }
}
