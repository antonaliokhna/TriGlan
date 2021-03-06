using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBot : AbstructBots
{
    // Start is called before the first frame update
    void Start()
    {
        this.botID = 2;
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        ServerSingleGame = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        this.MainTrictangle = GameObject.Find("TrictangleMain");
        SetBotInfo(ServerSingleGame.BotsDictionaty[this.botID]);
        this.thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
    }
}
