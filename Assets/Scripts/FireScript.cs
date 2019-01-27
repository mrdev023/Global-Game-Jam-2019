using UnityEngine;
using System.Collections.Generic;

public class FireScript : MonoBehaviour
{
    public GameObject fire1, fire2, fire3, pointLight, fireGUI;
    public PlayerScript player;
    public readonly float NUMBER_OF_ENERGY_LOST_PER_SECOND = 1;
    public readonly float MAX_FIRE_INTENSITY = 100;
    public GameObject prefabsFireElement;
    public Rain rain;

    public Sprite startUpSpriteCombustibleElement;
    public float startupCombustionEnergyElement = 100;

    private PlayerScript playerTriggered;
    private Vector2 startScaleFire1, startScaleFire2, startScaleFire3, startFireGUI;
    private float startLightRange;
    private int startOrderFire1, startOrderFire2, startOrderFire3;
    private Light pLight;
    private List<CombustibleItem> combustibleItems = new List<CombustibleItem>();
    private bool playerHasFeared = false;
    private float ratioEnergyLostBooster = 1;

    // Start is called before the first frame update
    void Start()
    {
        startScaleFire1 = new Vector2(fire1.gameObject.transform.localScale.x, fire1.gameObject.transform.localScale.y);
        startScaleFire2 = new Vector2(fire2.gameObject.transform.localScale.x, fire2.gameObject.transform.localScale.y);
        startScaleFire3 = new Vector2(fire3.gameObject.transform.localScale.x, fire3.gameObject.transform.localScale.y);
        startFireGUI = new Vector2(fireGUI.gameObject.transform.localScale.x, fireGUI.gameObject.transform.localScale.y);
        pLight = pointLight.GetComponent<Light>();
        startOrderFire1 = fire1.GetComponent<Renderer>().sortingOrder;
        startOrderFire2 = fire2.GetComponent<Renderer>().sortingOrder;
        startOrderFire3 = fire3.GetComponent<Renderer>().sortingOrder;
        startLightRange = pLight.range;
        if (startUpSpriteCombustibleElement is Sprite) AddIntensity(new CombustibleItem[] { new CombustibleItem(startupCombustionEnergyElement, startUpSpriteCombustibleElement) });
    }

    // Update is called once per frame
    void Update()
    {
        ratioEnergyLostBooster = (rain.IsRaining()) ? 2 : 1;
        if (GetFireIntensity() > 0)
        {
            DecrementIntensity(Time.deltaTime * NUMBER_OF_ENERGY_LOST_PER_SECOND * ratioEnergyLostBooster);
            playerHasFeared = false;
        }
        else if (!playerHasFeared)
        {
            player.GetComponent<FearLevel>().IncrementLevel(50);
            foreach (CombustibleItem combustibleItem in combustibleItems)
            {
                Destroy(combustibleItem.GetGameObject());
            }
            combustibleItems.Clear();
            playerHasFeared = true;
        }
        float ratioFireIntensity = GetFireIntensity() / MAX_FIRE_INTENSITY;
        fire1.gameObject.transform.localScale = new Vector3(startScaleFire1.x * ratioFireIntensity, startScaleFire1.y * ratioFireIntensity, 0);
        fire2.gameObject.transform.localScale = new Vector3(startScaleFire2.x * ratioFireIntensity, startScaleFire2.y * ratioFireIntensity, 0);
        fire3.gameObject.transform.localScale = new Vector3(startScaleFire3.x * ratioFireIntensity, startScaleFire3.y * ratioFireIntensity, 0);
        fireGUI.gameObject.transform.localScale = new Vector3(startFireGUI.x * ratioFireIntensity, startFireGUI.y * ratioFireIntensity, 0);
        float newRange = startLightRange * ratioFireIntensity;
        pLight.range = newRange + (Mathf.Cos(Time.timeSinceLevelLoad * 8) * newRange * 0.01f);
        if (player.transform.position.y > transform.position.y * 2 + player.transform.localScale.y)
        {
            fire1.GetComponent<Renderer>().sortingOrder = startOrderFire1 + 100;
            fire2.GetComponent<Renderer>().sortingOrder = startOrderFire2 + 100;
            fire3.GetComponent<Renderer>().sortingOrder = startOrderFire3 + 100;
        }
        else
        {
            fire1.GetComponent<Renderer>().sortingOrder = startOrderFire1;
            fire2.GetComponent<Renderer>().sortingOrder = startOrderFire2;
            fire3.GetComponent<Renderer>().sortingOrder = startOrderFire3;
        }

        if (playerTriggered is PlayerScript)
        {
            if (Input.GetKeyDown(KeyCode.E) || Manette.IsUse())
            {
                AddIntensity(playerTriggered.BurnObjects(MAX_FIRE_INTENSITY - GetFireIntensity()));
                playerTriggered = null;
            }
        }
    }

    public void AddIntensity (CombustibleItem[] combustibleItems)
    {
        for (int i = 0; i < combustibleItems.Length; i++)
        {
            GameObject go = Instantiate(prefabsFireElement, this.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            go.GetComponent<SpriteRenderer>().sprite = combustibleItems[i].GetSprite();
            combustibleItems[i].SetGameObject(go);
            this.combustibleItems.Add(combustibleItems[i]);
        }
    }

    public void DecrementIntensity (float intensity)
    {
        if (this.combustibleItems.Count == 0) return;
        if (GetFireIntensity() < intensity) intensity = GetFireIntensity();
        float intensityLeft = this.combustibleItems[0].GetQuantityOfEnergy() - intensity;
        if (intensityLeft <= 0)
        {
            Destroy(this.combustibleItems[0].GetGameObject());
            this.combustibleItems.RemoveAt(0);
            if (this.combustibleItems.Count > 0)
            {
                this.combustibleItems[0].SetQuantityOfEnergy(this.combustibleItems[0].GetQuantityOfEnergy() + intensityLeft);
            }
        }
        else
        {
            this.combustibleItems[0].SetQuantityOfEnergy(intensityLeft);
        }
    }

    public float GetFireIntensity ()
    {
        float t = 0;
        foreach (CombustibleItem c in this.combustibleItems)
        {
            t += c.GetQuantityOfEnergy();
        }
        return t;
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
