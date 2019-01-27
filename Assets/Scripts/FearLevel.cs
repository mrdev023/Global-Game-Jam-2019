using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearLevel : MonoBehaviour
{
    private readonly int UP_SPEED = 2, DOWN_SPEED = 10;
    public GameObject fire, player;
    private float fearLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, fire.transform.position) < (fire.GetComponent<FireScript>().GetFireIntensity() / 5) || player.GetComponent<PlayerTorch>().TorchIsActive())
        {
            if (fearLevel <= 0) return;
            DecrementLevel(Time.deltaTime * DOWN_SPEED);
        }
        else
        {
            if (fearLevel >= 100)
            {
                Global.Dead();
                return;
            }
            IncrementLevel(Time.deltaTime * UP_SPEED);
        }
        Debug.Log(fearLevel + " " + fire.GetComponent<FireScript>().GetFireIntensity());
    }

    public float GetFearLevel()
    {
        return fearLevel;
    }

    public void DecrementLevel(float amount)
    {
        this.fearLevel -= amount;
        if (this.fearLevel < 0) this.fearLevel = 0;
    }

    public void IncrementLevel(float amount)
    {
        this.fearLevel += amount;
        if (this.fearLevel > 100) this.fearLevel = 100;
    }
}
