using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boussole : MonoBehaviour
{

    public GameObject fire;
    public GameObject player;
    public GameObject encadrement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = Quaternion.FromToRotation(new Vector3(1, 0, 0), new Vector3(fire.transform.position.x - player.transform.position.x, fire.transform.position.y - player.transform.position.y, 0)).eulerAngles;
        rot.z -= 45;
        this.transform.rotation = Quaternion.Euler(rot);
    }
}
