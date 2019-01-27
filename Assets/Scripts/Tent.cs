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
        return Vector2.Distance(this.transform.position, player.transform.position) < this.GetComponent<Tent>().protectionZone;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < this.protectionZone &&
             player.GetComponent<PlayerScript>().GetNumberOfWood() >= 30)
        {
            player.GetComponent<PlayerScript>().SetInteractMessage(true, protectionZone >= houseLevel ? "maison" : "tente");

            if (Input.GetKey(KeyCode.E) || Manette.IsUse())
            {
                player.GetComponent<PlayerScript>().SetInteractMessage(false, "");
                player.GetComponent<PlayerScript>().BurnObjects(300);
                protectionZone += 1;

                if (protectionZone >= houseLevel)
                {
                    this.GetComponent<SpriteRenderer>().sprite = maison;
                    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
    }
}
