using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBot : AbstructBots
{
    void Start()
    {
        this.botID = 3;
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        ServerSingleGame = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        MainTrictangle = GameObject.Find("TrictangleMain");
        this.SetBotInfo(ServerSingleGame.BotsDictionaty[this.botID]);
        this.thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
    }
}
