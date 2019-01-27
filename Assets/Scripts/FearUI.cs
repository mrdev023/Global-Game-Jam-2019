using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearUI : MonoBehaviour
{

    public FearLevel fear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale = new Vector3(1 - fear.GetFearLevel() / 100.0f, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
    }
}
