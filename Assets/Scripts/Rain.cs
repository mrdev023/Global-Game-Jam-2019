using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    ParticleSystem particle;
    public float time = 0, updateTime;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateTime > Time.realtimeSinceStartup) return;
        updateTime = Time.realtimeSinceStartup + 1;
        if (!particle.isPlaying && Random.value > 0.98)
        {
            particle.Play();
            time = Time.realtimeSinceStartup;
            time += Random.Range(10, 30);
            Debug.Log("Start Rain : " + (time - Time.realtimeSinceStartup));
        }
        else if (particle.isPlaying && time < Time.realtimeSinceStartup)
        {
            particle.Stop();
        }
    }

    public bool IsRaining()
    {
        return particle.isPlaying;
    }
}
