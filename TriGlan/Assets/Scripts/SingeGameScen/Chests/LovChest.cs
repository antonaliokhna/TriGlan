using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovChest : AbstructChest
{
    // Start is called before the first frame update
    void Start()
    {
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        CasheThisAudioSource = this.gameObject.GetComponent<AudioSource>();
        this.price = 15;
        this.MinMaxGenerateWeaponValue = (6, 8);
        GenerateObject();
    }

    
}
