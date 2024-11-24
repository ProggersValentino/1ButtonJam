using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private List<Note> notes;
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

    public void SpawnNote(Note note)
    {
        Instantiate(note, this.gameObject.transform);
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
