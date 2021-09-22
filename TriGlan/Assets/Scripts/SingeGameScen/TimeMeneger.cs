using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMeneger : MonoBehaviour
{
    private InterfaceManager interfaceManager;
    void Start()
    {
        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
    }
    void Update()
    {
        if (!interfaceManager.PauseCanvas.active)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Escape))
            {
                if (Time.timeScale > 0.3f)
                    Time.timeScale -= 0.05f;
            }
            else
            {
                if (Time.timeScale < 1f)
                    Time.timeScale += 0.05f;
            }
        }
    }

}
