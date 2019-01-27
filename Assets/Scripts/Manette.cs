using System;
using UnityEngine;

public class Manette
{
    public static bool IsUp ()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            float value = Input.GetAxis("Vertical");
            if (value > 0.01) return true;
            else return false;
        }
        return false;
    }

    public static bool IsDown()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            float value = Input.GetAxis("Vertical");
            if (value < -0.01) return true;
            else return false;
        }
        return false;
    }

    public static bool IsRight()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            float value = Input.GetAxis("Horizontal");
            if (value > 0.01) return true;
            else return false;
        }
        return false;
    }

    public static bool IsLeft()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            float value = Input.GetAxis("Horizontal");
            if (value < -0.01) return true;
            else return false;
        }
        return false;
    }

    public static bool IsUse()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            return Input.GetButtonDown("Fire1");
        }
        return false;
    }

    public static bool IsTorch()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            return Input.GetButtonDown("Fire2");
        }
        return false;
    }

    public static bool IsPlaceBlock()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            return Input.GetButtonDown("Space");
        }
        return false;
    }
}
