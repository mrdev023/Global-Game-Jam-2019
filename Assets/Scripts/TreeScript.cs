using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject prefabBois;
    public int life = 5;
    public GameObject spawn;
    public PlayerScript player;

    private PlayerScript playerTriggered;
    private bool isAlreadySpawn = false;
    private int startupOrder;
    // Start is called before the first frame update
    void Start()
    {
        startupOrder = this.GetComponent<Renderer>().sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTriggered is PlayerScript)
        {
            if (Input.GetKeyDown(KeyCode.E) || Manette.IsUse())
            {
                if (--life <= 0)
                {
                    int n = (int)Random.Range(2, 6);
                    SpawnLoot(n);
                    Destroy(this.gameObject);
                }
                this.GetComponent<AudioSource>().Play();
            }
        }

        if ((((int)Time.realtimeSinceStartup) % 2) == 0)
        {
            float p = Random.value;
            if (p < .005f && !isAlreadySpawn)
            {
                SpawnLoot(1);
                isAlreadySpawn = true;
            }
        }
        else
        {
            isAlreadySpawn = false;
        }

        if (player.transform.position.y > transform.position.y * 2 + player.transform.localScale.y)
        {
            this.GetComponent<Renderer>().sortingOrder = startupOrder + 100;
        }
        else
        {
            this.GetComponent<Renderer>().sortingOrder = startupOrder;
        }
    }

    void SpawnLoot (int n)
    {
        float x = spawn.gameObject.transform.position.x;
        float y = spawn.gameObject.transform.position.y;

        for (int i = 0; i < n; i++)
        {
            float nx = Random.Range(x - 0.5f, x + 0.5f);
            float ny = Random.Range(y - 0.5f, y + 0.5f);
            Instantiate(prefabBois, new Vector3(nx, ny, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Player"))
        {
            playerTriggered = c.gameObject.GetComponent<PlayerScript>();
            playerTriggered.SetInteractMessage(true, this.gameObject.tag);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Player"))
        {
            playerTriggered = null;
            c.gameObject.GetComponent<PlayerScript>().SetInteractMessage(false, this.gameObject.tag);
        }
    }
}
