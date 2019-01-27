using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public int protectionZone = 10;
    public int houseLevel = 15;
    public GameObject player;
    public Sprite maison;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool playerIsSafe()
    {
        return Vector2.Distance(this.transform.position, player.transform.position) < this.protectionZone;
    }

    public bool isHouse()
    {
        return protectionZone >= houseLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsSafe())
        {
            if (player.GetComponent<PlayerScript>().GetNumberOfWood() >= 30)
            {
                player.GetComponent<PlayerScript>().SetInteractMessage(true, protectionZone >= houseLevel ? "maison" : 
                    string.Format("tente ({0} / {1})", 5 - (houseLevel - protectionZone), 5));

                if (Input.GetKey(KeyCode.E) || Manette.IsUse())
                {
                    player.GetComponent<PlayerScript>().SetInteractMessage(false, "");
                    player.GetComponent<PlayerScript>().BurnObjects(300);
                    protectionZone += 1;

                    if (isHouse())
                    {
                        this.GetComponent<SpriteRenderer>().sprite = maison;
                        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    }
                }
            }
        }
    }
}
