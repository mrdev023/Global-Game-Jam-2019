using System;
using UnityEngine;

public class CombustibleItem
{
    private float quantityOfEnergy;
    private Sprite sprite;
    private GameObject gameObject;

    public CombustibleItem(float quantityOfEnergy, Sprite sprite)
    {
        this.quantityOfEnergy = quantityOfEnergy;
        this.sprite = sprite;
    }

    public void SetQuantityOfEnergy(float quantityOfEnergy)
    {
        this.quantityOfEnergy = quantityOfEnergy;
    }

    public float GetQuantityOfEnergy()
    {
        return this.quantityOfEnergy;
    }

    public Sprite GetSprite()
    {
        return this.sprite;
    }

    public void SetGameObject (GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public GameObject GetGameObject ()
    {
        return this.gameObject;
    }
}
