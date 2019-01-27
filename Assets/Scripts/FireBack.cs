using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBack : MonoBehaviour
{
    public Rain rain;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rain.IsRaining()) item.SetActive(true);
        else item.SetActive(false);
    }
}
