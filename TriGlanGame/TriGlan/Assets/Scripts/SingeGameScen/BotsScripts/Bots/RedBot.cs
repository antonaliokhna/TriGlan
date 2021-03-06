using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RedBot : AbstructBots
{
    void Start()
    {
        this.botID = 1;
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        ServerSingleGame = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        this.MainTrictangle = GameObject.Find("TrictangleMain");
        SetBotInfo(ServerSingleGame.BotsDictionaty[this.botID]);
        this.thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
    }
}
