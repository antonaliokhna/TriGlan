using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstructBots : MonoBehaviour
{
    public GameObject Ammo;
    public GameObject Coin;
    public ParticleSystem DestroyEffect;
    public AudioClip hitAudio;
    public AudioClip deadAudio;

    protected ServerSingleGame ServerSingleGame;
    protected AudioSource thisAudioSourse;
    protected GameObject MainTrictangle;

    protected int botID;
    protected int countCoin;
    protected float speed;
    protected int hp;
    protected float sizeBollet;
    protected int speedAmmo;
    protected string BotName;
    protected float startTime;
    protected float restartAmmoWait;
    protected float restartAmooShot;
    protected float distance;
    protected float timeShot;

    protected int LastAttacUserID;

    protected float restartAmmoWaitTime;
    protected float restartAmmoWaitShot;
    protected static System.Random r = new System.Random();

    protected GlowMeneger glowMeneger;

    protected CameraAnimation cameraAnimation;

    protected int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                IsDead();
            }
        }
    }

    void Start()
    {
        this.cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        this.glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        this.ServerSingleGame = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        this.MainTrictangle = GameObject.FindGameObjectWithTag("Player");
        this.thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
        this.SetBotInfo(ServerSingleGame.BotsDictionaty[this.botID]);

    }

    void Update()
    {
        if (this.restartAmmoWaitShot <= this.restartAmooShot)
        {
            if (this.timeShot <= 0f)
            {
                for (int i = 1; i < this.gameObject.transform.childCount + 1; i++)
                {
                    var ammo = Instantiate(this.Ammo, this.transform.Find($"Shot{i}").transform.position, this.transform.rotation);
                    ammo.GetComponent<SpriteRenderer>().color = this.gameObject.GetComponent<SpriteRenderer>().color;
                    BotAttacScript botAttacScript = ammo.GetComponent<BotAttacScript>();
                    botAttacScript.mainTrictangle = this.MainTrictangle;
                    botAttacScript.speed = this.speedAmmo;
                    ammo.transform.localScale = new Vector3(this.sizeBollet, this.sizeBollet, 1);
                }
                this.timeShot = startTime;
            }
            else
                this.timeShot -= Time.deltaTime;

            this.restartAmmoWaitShot += Time.deltaTime;
        }
        else
        {
            if (this.restartAmmoWaitTime <= this.restartAmmoWait)
                this.restartAmmoWaitTime += Time.deltaTime;
            else
            {
                this.restartAmmoWaitTime = 0;
                this.restartAmmoWaitShot = 0;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<MainTrictangle>().waitForIsJumping > -0.2f)
            {
                this.thisAudioSourse.clip = hitAudio;
                this.thisAudioSourse.Play();
                this.Hp -= 33;
            }
        }
        if (other.gameObject.tag == "BlueAmmo")
        {
            this.thisAudioSourse.clip = hitAudio;
            this.thisAudioSourse.Play();
            CircleAttacScript circleAttacScript = other.gameObject.GetComponent<CircleAttacScript>();
            this.Hp -= circleAttacScript.damage;
            LastAttacUserID = circleAttacScript.UserID;
            Destroy(other.gameObject);
        }
    }

    public void FixedUpdate()
    {
        if (Math.Sqrt((Math.Pow(MainTrictangle.transform.position.x - transform.position.x, 2d) + (Math.Pow(MainTrictangle.transform.position.y - transform.position.y, 2d)))) > distance)
            transform.position = Vector3.MoveTowards(transform.position, MainTrictangle.transform.position, Time.deltaTime * speed);
        BotsFindMe();
    }

    public void SetBotInfo(string[] values)
    {
        this.BotName = values[1];
        this.Hp = Convert.ToInt32(values[2]);
        this.countCoin = Convert.ToInt32(values[3]) + r.Next(-1, 2);
        this.speed = Convert.ToSingle(values[4], CultureInfo.InvariantCulture) + (float)(r.Next(-3, 4));
        this.distance = Convert.ToSingle(values[5], CultureInfo.InvariantCulture) + (float)r.Next(-5, 5);
        this.sizeBollet = Convert.ToSingle(values[6], CultureInfo.InvariantCulture);
        this.speedAmmo = Convert.ToInt32(values[7]) + r.Next(-3, 4);
        this.restartAmmoWait = Convert.ToSingle(values[8], CultureInfo.InvariantCulture) + (float)(r.Next(-5, 6) / 10);
        this.restartAmooShot = Convert.ToSingle(values[9], CultureInfo.InvariantCulture) + (float)r.Next(-1, 2);
        this.startTime = Convert.ToSingle(values[10], CultureInfo.InvariantCulture) + (float)(r.Next(-2, 2) / 10);
        this.timeShot = this.startTime;
    }

    private void BotsFindMe()
    {
        Vector3 diferent = MainTrictangle.transform.position - transform.position;
        float rotateZ = Mathf.Atan2(diferent.y, diferent.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

    public void IsDead()
    {
        cameraAnimation.Shake();
        glowMeneger.BotDead();
        StartCoroutine(this.ServerSingleGame.CreateMatchKill(this.botID + 1));
        this.thisAudioSourse.clip = deadAudio;
        this.thisAudioSourse.Play();
        for (int i = 1; i <= countCoin; i++)
        {
            var coin = Instantiate(Coin, new Vector3(transform.position.x + (float)r.Next(-5, 6), transform.position.y + (float)r.Next(-5, 6)), transform.rotation *= Quaternion.Euler(0f, 0f, (float)r.Next(1, 361)));
            Destroy(coin.gameObject, 30f);
        }
        SpawnParticalEffect();
        Destroy(this.gameObject, 0.3f);
    }
    public void SpawnParticalEffect()
    {
        this.DestroyEffect.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        Vector3 Brickpos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(Brickpos.x, Brickpos.y, Brickpos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        Destroy(effect, DestroyEffect.main.startLifetime.constant / DestroyEffect.main.simulationSpeed - 0.5f);
    }
}
