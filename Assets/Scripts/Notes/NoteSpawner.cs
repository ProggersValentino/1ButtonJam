using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public bool hasStarted;
    public float currentTempo;

    [SerializeField] private Note tapPrefab;
    [SerializeField] private Note holdPrefab;
    public float[] noteSpaces;
    private int noteSpaceIndex = 0;
    public float[] noteTypes; //0 for tap and 1 for hold

    private float timeElapsed = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (noteSpaces.Length != noteTypes.Length)
        {
            Debug.Log("Warning: note spaces and note type arrays are not the same size");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if (Time.time > timeElapsed + noteSpaces[noteSpaceIndex])
            {
                if (noteTypes[noteSpaceIndex] == 1)
                {
                    SpawnNote(holdPrefab);
                }
                else if(noteTypes[noteSpaceIndex] == 0)
                {
                    SpawnNote(tapPrefab);
                }
                else
                {
                    Debug.Log("wrong format");
                }

                noteSpaceIndex++;
                if (noteSpaceIndex >= noteSpaces.Length)
                {
                    noteSpaceIndex = 0;
                }
                Debug.Log(noteSpaceIndex);
                timeElapsed = Time.time + noteSpaces[noteSpaceIndex];
            }
        }
    }

    public void SpawnNote(Note note)
    {
        Note temp = Instantiate(note, this.gameObject.transform);
        temp.tempo = currentTempo;
    }

}
