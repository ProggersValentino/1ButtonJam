using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    //public Sprite sprite;
    public float tempo;
    public bool canBePressed;
    public bool areStopping;
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
        if(!areStopping)transform.position -= new Vector3(tempo * Time.deltaTime, 0f, 0f);
    }
    

    /// <summary>
    /// note goes bye bye 
    /// </summary>
    public virtual async void DeathNote()
    {
        //play audio cue
        //death anim
        areStopping = true;
        renderer.sprite = ND.deathSprite;
        //Debug.Log(gameObject);
        await Task.Delay(200);
        
        Destroy(gameObject);
    }
}
