using System;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    //public Sprite sprite;
    public float tempo;
    public bool canBePressed;
    public NoteData ND;

    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = ND.sprite;

        tempo = tempo / 60f; //align tempo with 60fps
    }

    private void Update()
    {
        transform.position -= new Vector3(tempo * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }

    /// <summary>
    /// note goes bye bye 
    /// </summary>
    public void DeathNote()
    {
        //play audio cue
        //death anim
        Destroy(gameObject);
    }
}
