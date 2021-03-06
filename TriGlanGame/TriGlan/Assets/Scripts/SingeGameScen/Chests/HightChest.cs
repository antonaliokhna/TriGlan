using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightChest : AbstructChest
{
    // Start is called before the first frame update
    void Start()
    {
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        CasheThisAudioSource = this.gameObject.GetComponent<AudioSource>();
        this.price = 35;
        this.MinMaxGenerateWeaponValue = (1, 8);
        GenerateObject();
    }
}
