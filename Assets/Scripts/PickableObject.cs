using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private PlayerScript playerTriggered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTriggered is PlayerScript)
        {
            if (Input.GetKeyDown(KeyCode.E) || Manette.IsUse())
            {
                if (playerTriggered.PickupObject(new CombustibleItem(Random.Range(0,10), GetComponent<SpriteRenderer>().sprite))) Destroy(this.gameObject);
                playerTriggered = null;
            }
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
