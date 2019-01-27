using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{

    public GameObject torch;
    private float previousTime;

    // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F) || Manette.IsTorch()) && GetComponent<PlayerScript>().GetNumberOfWood() > 0)
        {
            torch.SetActive(!torch.active);
        }
        if (Time.realtimeSinceStartup - previousTime > 1.0f && torch.active)
        {
            GetComponent<PlayerScript>().BurnObjects(10);
            previousTime = Time.realtimeSinceStartup;
        }

        if (GetComponent<PlayerScript>().GetNumberOfWood() <= 0)
        {
            torch.SetActive(false);
        }
    }

    public bool TorchIsActive ()
    {
        return torch.active;
    }
}
