using UnityEngine;

public class Hold : Note
{
    public float secsHeld = 1f; //temp

    public float health = 1000f;

    public void TakeHealth(float amount)
    {
        health -= amount;
    }
    
}
