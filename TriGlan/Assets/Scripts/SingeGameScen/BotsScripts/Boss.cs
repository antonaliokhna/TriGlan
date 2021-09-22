using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int hp = 1000;
    public GameObject Ammo;
    public GameObject ArmorBoss;
    public GameObject hpBar;
    public float TimeOnShot = 0.5f;
    public ParticleSystem effect;
    public float TimeSpawnParticalEffect;
    public Color[] colors;
    public GameObject GameEnd;
    private GameObject Player;

    private CameraAnimation cameraAnimation;
    private InterfaceManager interfaceManager;
    private GlowMeneger glowMeneger;

    public static System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
        cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        colors = new Color[] { Color.red, Color.white, Color.blue, Color.yellow, Color.cyan};
        hp = 1000;
        Player = GameObject.FindGameObjectWithTag("Player");
        GameEnd = GameObject.FindGameObjectWithTag("GameEndMeneger");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hp > 0)
        {
            if (!ArmorBoss.gameObject.active)
            {
                if (this.hp < 500)
                {
                    if (TimeOnShot >= 0.4f)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            var ammo = Instantiate(Ammo.gameObject, this.transform.Find($"Shot{i}").gameObject.transform.position, this.transform.rotation);
                            ammo.GetComponent<SpriteRenderer>().color = Color.green;
                            ammo.GetComponent<BotAttacScript>().mainTrictangle = Player;
                            ammo.transform.localScale = new Vector3(8f, 8f, 1);
                        }
                        TimeOnShot = 0f;
                    }
                    else
                        TimeOnShot += Time.deltaTime;
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnParticalEffect();
                cameraAnimation.Shake();
            }
            GameEnd.gameObject.GetComponent<GameEndMeneger>().enabled = true;
            Time.timeScale = 0.1f;
            interfaceManager.StartCoroutineUpdateMatchInfo();
            glowMeneger.BosDead();
            Destroy(this.gameObject);
        }
       
    }
    public void SpawnParticalEffect()
    {
        this.effect.startColor = colors[r.Next(0, colors.GetLength(0))];
        Vector3 Brickpos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(Brickpos.x, Brickpos.y, 1);
        GameObject effectSpawn = Instantiate(effect.gameObject, spawnPosition, Quaternion.identity);
        effectSpawn.gameObject.GetComponent<ParticleSystem>().startSpeed = 20f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<MainTrictangle>().isJumping)
            {
                this.hp -= 30;
                hpBar.gameObject.GetComponent<Image>().fillAmount = (float)((float)this.hp / (float)1000);
            }
        }
        if (other.gameObject.tag == "BlueAmmo")
        {
            this.hp -= other.gameObject.GetComponent<CircleAttacScript>().damage;
            hpBar.gameObject.GetComponent<Image>().fillAmount = (float)((float)this.hp / (float)1000);
        }
    }
}
