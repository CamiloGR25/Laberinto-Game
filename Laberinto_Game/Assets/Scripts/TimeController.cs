using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private float startTime;
    public bool activar = false;

    void Start()
    {
        startTime = Time.time;
    }

    public void StartTime()
    {
        if (!activar)
        {
            activar = true;
        }
    }

    public void StopTime()
    {
        if (activar)
        {
            activar = false;
        }
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }
}
