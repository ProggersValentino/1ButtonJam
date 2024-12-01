using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class NoteSpawner : MonoBehaviour
{
    public bool hasStarted;
    public float currentTempo;

    [SerializeField] private Note tapPrefab;
    [SerializeField] private Note holdPrefab;
    [SerializeField] private MenuNote menuPrefab;
    public NoteSection[] sections;
    private int noteSectionIndex = 0;
    private int noteSpaceIndex = 0; //0 for tap and 1 for hold

    private float timeElapsed = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var section in sections)
        {
            if (section.noteSpaces.Length != section.noteTypes.Length)
            {
                Debug.Log("Warning: note spaces and note type arrays are not the same size");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if (Time.time > timeElapsed + sections[noteSectionIndex].noteSpaces[noteSpaceIndex])
            {
                if (sections[noteSectionIndex].noteTypes[noteSpaceIndex] == 1)
                {
                    SpawnNote(holdPrefab);
                }
                else if(sections[noteSectionIndex].noteTypes[noteSpaceIndex] == 0)
                {
                    SpawnNote(tapPrefab);
                }
                else if(sections[noteSectionIndex].noteTypes[noteSpaceIndex] == 2)
                {
                    SpawnNote(menuPrefab, noteSpaceIndex);
                }
                else
                {
                    Debug.Log("wrong format");
                }

                noteSpaceIndex++;
                if (noteSpaceIndex >= sections[noteSectionIndex].noteSpaces.Length)
                {
                    noteSectionIndex++;
                    noteSpaceIndex = 0;
                    if (noteSectionIndex >= sections.Length)
                    {
                        noteSectionIndex = 0;
                    }
                }
                //Debug.Log(noteSpaceIndex);
                timeElapsed = Time.time + sections[noteSectionIndex].noteSpaces[noteSpaceIndex];
            }
        }
    }

    public void SpawnNote(Note note)
    {
        Note temp = Instantiate(note, this.gameObject.transform);
        temp.tempo = currentTempo;
    }

    // Override for spawning menu notes, this allows the index to be passed so it can update the menu choice appropriately]
    // Might change this when moving on from main menu
    public void SpawnNote(MenuNote note, int noteIndex)
    {
        MenuNote temp = Instantiate(note, this.gameObject.transform);
        temp.tempo = currentTempo;
        temp.sceneIndex = noteIndex;
        temp.SetText();
    }

}
