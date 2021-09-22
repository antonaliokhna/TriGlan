using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MainTrictangle : MonoBehaviour
{
    public ServerSingleGame Server;

    public float speed = 1f;
    public float startTime;
    private int countCoin;
    public GameObject ammo;

    public AudioClip GetCoinAudio;
    public AudioClip HitMe;
    public AudioClip takeGun;
    public AudioClip takeBoost;
    public AudioClip takeJump;

    public GameObject textMeshBoost;

    public GameObject Interface;
    public InterfaceManager InterfaceManager;
    public WeaponScript objectWeapon;

    public GlowMeneger glowMeneger;

    private GameObject TrailManegerObject;
    private TrailRenderer TrailRenderer;

    public int slot = 1;

    private List<int> TakedWeaponsID;
    public GameObject WeaponCreateObject;
    private WeaponScript firstWeapon;
    private WeaponScript SecondWeapon;
    private WeaponScript usedWeapon;

    private float timeShot;

    private float isGodTime = 0f;
    private float isGodTimeLightning = 0f;

    public bool isJumping = false;
    public float waitForIsJumping = 0f;

    public float jumpAnimateTime = 0f;
    private float isJumpTime = 0f;
    private Vector2 secondPosJump = new Vector2();
    private float TimeJump = 0.15f;

    private float boostSpeedAmmo = 0;
    private float boostStartTime = 0;

    private Camera cacheCamera;
    private SpriteRenderer thisSpriteRender;
    private AudioSource thisAudioSourse;

    public CameraAnimation cameraAnimation;

    private float cofMoving = 1;

    public static System.Random r = new System.Random();

    // Start is called before the first frame update

    public int CountCoin
    {
        get => countCoin;
        set
        {
            countCoin = value;
            InterfaceManager.CountCoinI = countCoin;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TakedWeaponsID = new List<int>();
        cacheCamera = Camera.main;
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        Server = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        InterfaceManager = Interface.GetComponent<InterfaceManager>();
        objectWeapon = new WeaponScript();
        firstWeapon = new WeaponScript();
        SecondWeapon = new WeaponScript();
        usedWeapon = new WeaponScript();
        TrailManegerObject = this.gameObject.transform.Find("TrailObjectMeneger").gameObject;
        TrailRenderer = TrailManegerObject.GetComponent<TrailRenderer>();
        thisSpriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        thisAudioSourse = this.gameObject.GetComponent<AudioSource>();
        Time.timeScale = 0.3f;
        CountCoin = 10;
    }
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                SpawAndSetGunVisible();

            if (Input.GetKeyDown(KeyCode.G))
                DropWeapon();

            Jumping();
            Moving();
            Shoting();
        }
        RotateMainTrictangle();
    }

    private void DropWeapon()
    {
        if (usedWeapon.weapon != null)
        {
            if (slot == 1)
            {
                CreateDroppedWeapon(firstWeapon);
                firstWeapon = new WeaponScript();
                usedWeapon = firstWeapon;
            }
            else
            {
                CreateDroppedWeapon(SecondWeapon);
                SecondWeapon = new WeaponScript();
                usedWeapon = SecondWeapon;
            }
            InterfaceManager.useWeaponSprite = null;
            InterfaceManager.OnChengeSkin();
        }
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //boost jump
        {
            if (InterfaceManager.boostBar.fillAmount == 1)
            {
                glowMeneger.JumpEffect();
                cameraAnimation.Shake();
                ClipAudio(takeJump);
                var mousePoint = cacheCamera.ScreenToWorldPoint(Input.mousePosition);
                secondPosJump = new Vector2();
                if (transform.position.x - mousePoint.x >= 0)
                    secondPosJump.x = (mousePoint.x - 100);
                else
                    secondPosJump.x = (mousePoint.x + 100);

                secondPosJump.y = (secondPosJump.x - transform.position.x) * (mousePoint.y - transform.position.y) /
                     (mousePoint.x - transform.position.x) + transform.position.y;

                TrailRenderer.widthMultiplier = 1f;

                waitForIsJumping = 0.5f;
                isJumping = true;

                jumpAnimateTime = TimeJump;
                InterfaceManager.boostBar.fillAmount = 0;
            }
        }
    }

    private void Shoting()
    {
        if (timeShot <= 0f && usedWeapon.speed != 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                for (int i = 1; i < usedWeapon.countShot + 1; i++)
                {
                    var ammoObject = Instantiate(ammo, GameObject.Find($"ShotMain{i}").transform.position, transform.rotation);
                    CircleAttacScript circleAttacScriptAmmo = ammoObject.GetComponent<CircleAttacScript>();
                    circleAttacScriptAmmo.damage = usedWeapon.damage;
                    circleAttacScriptAmmo.TimeScale = Time.timeScale;
                    circleAttacScriptAmmo.speed = usedWeapon.speed + this.boostSpeedAmmo;
                    circleAttacScriptAmmo.UserID = ServerMenu.UserId;
                    ammoObject.transform.localScale = new Vector3(usedWeapon.sizeBoolet, usedWeapon.sizeBoolet, 1);
                }
                timeShot = usedWeapon.timeShot - this.boostStartTime;
            }
        }
        else
            timeShot -= Time.deltaTime;
    }

    private void Moving()
    {

        if ((Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) ||
                        (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
            cofMoving = 1.5f;
        else
            cofMoving = 1f;

        if (Input.GetKey(KeyCode.W))
            transform.position += ((new Vector3(0, 20.0f / cofMoving, 0) * speed) * Time.timeScale) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position += ((new Vector3(0, -20.0f / cofMoving, 0) * speed) * Time.timeScale) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position += ((new Vector3(-20.0f / cofMoving, 0, 0) * speed) * Time.timeScale) * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += ((new Vector3(20.0f / cofMoving, 0, 0) * speed) * Time.timeScale) * Time.deltaTime;
    }

    public void GameOver()
    {
        cameraAnimation.Shake();
        firstWeapon = new WeaponScript();
        SecondWeapon = new WeaponScript();
        usedWeapon = new WeaponScript();
        this.CountCoin = 0;
        this.transform.position = new Vector3(0, 0, 1);
        this.slot = 1;
        this.timeShot = 0;

        this.isGodTime = 0f;
        this.isGodTimeLightning = 0f;

        this.isJumping = false;
        this.waitForIsJumping = 0f;

        this.jumpAnimateTime = 0f;
        this.isJumpTime = 0f;
        this.secondPosJump = new Vector2();
        this.TimeJump = 0.15f;

        this.boostSpeedAmmo = 0;
        this.boostStartTime = 0;
        this.objectWeapon = new WeaponScript();
    }

    private void SpawAndSetGunVisible()
    {
        if (slot == 1)
        {
            slot = 2;
            usedWeapon = SecondWeapon;
        }
        else
        {
            slot = 1;
            usedWeapon = firstWeapon;
        }
        InterfaceManager.UseWeapon = this.slot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isJumpTime += Time.deltaTime;

        if (jumpAnimateTime > 0f)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, secondPosJump, Time.deltaTime * 75f);
            jumpAnimateTime -= Time.deltaTime;
        }

        if (this.isJumping)
        {
            if (this.waitForIsJumping < -0.25f)
                this.isJumping = false;
            else
                this.waitForIsJumping -= Time.deltaTime;
        }

        TrailLine();
        CheckGodTime();
    }

    private void TrailLine()
    {
        if (TrailRenderer.widthMultiplier != 0)
        {
            TrailRenderer.widthMultiplier -= Time.deltaTime * 2f;
            if (TrailRenderer.widthMultiplier <= 0)
            {
                TrailRenderer.widthMultiplier = 0;
            }
        }
    }

    private void CheckGodTime()
    {
        isGodTime += Time.deltaTime;

        if (isGodTimeLightning > 0f)
        {
            if (isGodTime >= 0.05f)
            {
                if (thisSpriteRender.enabled == true)
                    thisSpriteRender.enabled = false;
                else
                    thisSpriteRender.enabled = true;

                isGodTime = 0;
            }
            isGodTimeLightning -= Time.deltaTime;
        }
        else
            thisSpriteRender.enabled = true;
    }

    private void RotateMainTrictangle()
    {
        Vector3 diferent = cacheCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diferent.y, diferent.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boost")
        {
            Time.timeScale = 1f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                ClipAudio(takeBoost);
                glowMeneger.TakeBoost();
                BoostsScript otherBoostScript = other.gameObject.GetComponent<BoostsScript>();
                switch (otherBoostScript.BoostName)
                {
                    case "Speed":
                        this.speed += otherBoostScript.value;
                        break;

                    case "SpeedJump":
                        this.TimeJump += otherBoostScript.value;
                        break;

                    case "SpeedShot":
                        this.boostStartTime += otherBoostScript.value;
                        this.boostSpeedAmmo += otherBoostScript.value;
                        break;

                    case "Health":
                        InterfaceManager.HpInt += (int)otherBoostScript.value;
                        break;
                }
                StartCoroutine(Server.CreateMatchBoost(otherBoostScript.BoostID));
                var text = Instantiate(textMeshBoost, new Vector2(transform.position.x, transform.position.y + 3.5f), new Quaternion(0, 0, 0, 0));
                text.gameObject.GetComponent<TextMeshPro>().text = "+" + otherBoostScript.BoostName;
                Destroy(text, 1.5f);
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.tag == "Weapon")
        {
            Time.timeScale = 1f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                ClipAudio(takeGun);
                glowMeneger.TakeGun();

                WeaponScript weaponScriptOther = other.gameObject.GetComponent<WeaponScript>();
                InterfaceManager.useWeaponSprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
                InterfaceManager.OnChengeSkin();

                if (slot == 1)
                {
                    if (firstWeapon.weapon == null)
                        firstWeapon = weaponScriptOther;
                    else
                    {
                        CreateDroppedWeapon(firstWeapon);
                        firstWeapon = weaponScriptOther;
                    }
                    usedWeapon = firstWeapon;
                }
                else
                {
                    if (SecondWeapon.weapon == null)
                        SecondWeapon = weaponScriptOther;
                    else
                    {
                        CreateDroppedWeapon(SecondWeapon);
                        SecondWeapon = weaponScriptOther;
                    }
                    usedWeapon = SecondWeapon;
                }
                if (!TakedWeaponsID.Contains(weaponScriptOther.weaponID))
                {
                    TakedWeaponsID.Add(weaponScriptOther.weaponID);
                    StartCoroutine(Server.CreateMatchWeapon(weaponScriptOther.weaponID));
                }
                Destroy(other.gameObject);
            }
        }
    }

    private void CreateDroppedWeapon(WeaponScript weaponDrop)
    {
        WeaponScript DropWeapon = weaponDrop;
        var weapon = Instantiate(this.WeaponCreateObject, new Vector3(this.transform.position.x + (float)r.Next(-5, 6), this.transform.position.y + (float)r.Next(-5, 6)), this.transform.rotation *= Quaternion.Euler(0f, 0f, (float)r.Next(1, 361)));
        weapon.gameObject.GetComponent<WeaponScript>().SpawnWeaponObject(Server.WeaponsDictionaty[DropWeapon.weaponID - 1]);
        Destroy(weapon, 30f);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            glowMeneger.TakeCoin();
            ClipAudio(GetCoinAudio);
            CountCoin++;
            InterfaceManager.countAllGetCoins++;
            Destroy(other.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (isGodTimeLightning <= 0 && !isJumping)
        {
            if (other.gameObject.tag == "RedAmmo")
            {
                ClipAudio(HitMe);
                SetDamageAndStartGodTime();
                cameraAnimation.Shake();
            }
        }
    }
    public void OnCollisionStay2D(Collision2D other)
    {
        if (isGodTimeLightning <= 0 && !isJumping)
        {
            if (other.gameObject.tag == "Orange")
            {
                ClipAudio(HitMe);
                SetDamageAndStartGodTime();
                cameraAnimation.Shake();
            }
        }
    }
    private void SetDamageAndStartGodTime()
    {
        InterfaceManager.HpInt--;
        this.isGodTimeLightning = 1f;

        if (InterfaceManager.HpInt < 2)
        glowMeneger.HitPlayer();
    }

    private void ClipAudio(AudioClip clip)
    {
        thisAudioSourse.clip = clip;
        thisAudioSourse.Play();
    }
}