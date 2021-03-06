using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBot : AbstructBots
{
    void Start()
    {
        this.botID = 4;
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        ServerSingleGame = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        this.MainTrictangle = GameObject.Find("TrictangleMain");
        SetBotInfo(ServerSingleGame.BotsDictionaty[this.botID]);
        this.thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
    }    
}
