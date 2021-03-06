using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OrangeBot : AbstructBots
{
    // Start is called before the first frame update
    void Start()
    {
        this.botID = 0;
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        ServerSingleGame = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        MainTrictangle = GameObject.Find("TrictangleMain");
        SetBotInfoOveride(ServerSingleGame.BotsDictionaty[this.botID]);
        this.thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void SetBotInfoOveride(string[] values)
    {
         this.BotName = values[1];
         this.Hp = Convert.ToInt32(values[2]);
         this.countCoin = Convert.ToInt32(values[3]);
         this.speed = (float)Convert.ToInt32(values[4]) + (float)(r.Next(-3, 4));
    }
}
