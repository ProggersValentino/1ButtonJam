using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    //public Sprite sprite;
    public float tempo;
    public bool canBePressed;
    public bool areStopping = false;
    public NoteData ND;

    private SpriteRenderer renderer;
    private Animator animator;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        renderer.sprite = ND.sprite;

        tempo = tempo / 60f; //align tempo with 60fps
    }

    private void Update()
    {
        if(!areStopping) transform.position -= new Vector3(tempo * Time.deltaTime, 0f, 0f);
    }
    

    /// <summary>
    /// note goes bye bye 
    /// </summary>
    public virtual async void DeathNote()
    {
        //play audio cue
        //death anim
        areStopping = true;
        animator.SetBool("Death", true);
        //Debug.Log(gameObject);
        await Task.Delay(300);
        
        Destroy(gameObject);
    }
}
