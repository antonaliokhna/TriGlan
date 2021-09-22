using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject player;

    public GameObject[] PrefabsTrictangle;
    public ParticleSystem SpawnEffect;
    public List<GameObject> spawnedTrictangles;
    public List<GameObject> spawnedTrictanglesEffect;

    public Queue<GameObject> spawnQuqueBots;

    public GameObject LovChest;
    public GameObject mediumChest;
    public GameObject hightChest;

    public GameObject useChest;
    public GameObject box;
    public GameObject lvlMeneger;
    private LvlMeneger lvlMenegerScript;

    public GameObject DoorT;
    public GameObject DoorR;
    public GameObject DoorB;
    public GameObject DoorL;

    public GameObject[] DoorTPass;
    public GameObject[] DoorRPass;
    public GameObject[] DoorBPass;
    public GameObject[] DoorLPass;

    public GameObject[] spawnedBox;

    private List<GameObject> DoorsOpen;

    private bool isFite = false;
    private bool isSpawnBots = false;
    private float timeSpawnBots = 0f;
    private float isSpawnBotsTrictangle = 0f;

    public AudioClip ClipCloseEffect;

    public GameObject RandomSpawnWallPrefub;
    public GameObject[] SpawnedWalls;

    private GlowMeneger glowMeneger;
    private CameraAnimation cameraAnimation;

    static System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        this.lvlMeneger = GameObject.Find("LvlMeneger");
        lvlMenegerScript = lvlMeneger.GetComponent<LvlMeneger>();
        if (this.gameObject.tag != "Room")
            this.gameObject.transform.Find("Canvas").gameObject.transform.Find("TextFloor").gameObject.GetComponent<Text>().text = $"Floor {lvlMeneger.GetComponent<LvlMeneger>().LvlNow}";

        this.spawnedTrictanglesEffect = new List<GameObject>();
        this.spawnedTrictangles = new List<GameObject>();
        this.DoorsOpen = new List<GameObject>();
        this.spawnQuqueBots = new Queue<GameObject>();

        player = GameObject.Find("TrictangleMain");
        this.useChest = new GameObject();


        if (this.gameObject.tag == "Room")
        {
            GameObject spawnChest = new GameObject();
            int chestCount = r.Next(0, 2);
            if (chestCount == 1)
            {
                spawnChest = hightChest;
                if (lvlMenegerScript.LvlNow >= 1 && lvlMenegerScript.LvlNow <= 2)
                {
                    spawnChest = LovChest;
                }
                else if (lvlMenegerScript.LvlNow >= 3 && lvlMenegerScript.LvlNow <= 5)
                {
                    spawnChest = mediumChest;
                }
                this.useChest = Instantiate(spawnChest, this.transform.position, this.transform.rotation);
            }
        }

        if (this.gameObject.tag == "Room")
        {
            int valCountWall = r.Next(1, 3);
            this.SpawnedWalls = new GameObject[valCountWall];

            for (int i = 0; i < valCountWall; i++)
            {
                int limit = 500;
                while (limit-- > 0)
                {
                    int RandomValX = r.Next(-40, 40);
                    int RandomValY = r.Next(-40, 40);
                    if (Math.Abs(RandomValX) > 20 && Math.Abs(RandomValY) > 20)
                    {
                        this.SpawnedWalls[i] = Instantiate(this.RandomSpawnWallPrefub.gameObject, new Vector3(this.transform.position.x + (float)RandomValX, this.transform.position.y + (float)RandomValY, 1), this.transform.rotation);
                        if (r.Next(0, 2) == 1)
                            this.SpawnedWalls[i].transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
                        break;
                    }
                }
            }
        }

        int valCountBox = r.Next(0, 3);
        this.spawnedBox = new GameObject[valCountBox];
        for (int i = 0; i < valCountBox; i++)
        {
            this.spawnedBox[i] = Instantiate(box, new Vector3(this.transform.position.x + (float)r.Next(-50, 50), this.transform.position.y + (float)r.Next(-50, 50), 1), this.transform.rotation);
        }
    }

    public void DestroyWalls()
    {
        for (int i = 0; i < SpawnedWalls.GetLength(0); i++)
        {
            if (SpawnedWalls[i] != null)
                Destroy(SpawnedWalls[i].gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && this.gameObject.tag == "Room" && !this.isFite)
        {
            this.isFite = true;

            if (this.DoorT.gameObject.active == false)
                this.DoorsOpen.Add(this.DoorT.gameObject);
            if (this.DoorB.gameObject.active == false)
                this.DoorsOpen.Add(this.DoorB.gameObject);
            if (this.DoorR.gameObject.active == false)
                this.DoorsOpen.Add(this.DoorR.gameObject);
            if (this.DoorL.gameObject.active == false)
                this.DoorsOpen.Add(this.DoorL.gameObject);

            for (int i = 0; i < this.DoorsOpen.Count; i++)
            {
                this.DoorsOpen[i].SetActive(true);
                ClipAudio(ClipCloseEffect);
            }

            if (lvlMenegerScript.LvlNow == 1)
            {
                SpawneTrictangles(2); //red orange
            }
            if (lvlMenegerScript.LvlNow >= 2 && lvlMenegerScript.LvlNow <= 3)
            {
                SpawneTrictangles(3); //yellow
            }
            if (lvlMenegerScript.LvlNow >= 4 && lvlMenegerScript.LvlNow <= 5)
            {
                SpawneTrictangles(4); //blue
            }
            if (lvlMenegerScript.LvlNow >= 6 && lvlMenegerScript.LvlNow <= 7)
            {
                SpawneTrictangles(5); //pink
            }
            if (lvlMenegerScript.LvlNow >= 8 && lvlMenegerScript.LvlNow <= 9)
            {
                SpawneTrictangles(6); //white
            }
            glowMeneger.DoorClose();
            cameraAnimation.Shake();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < this.DoorsOpen.Count; i++)
        {
            this.DoorsOpen[i].SetActive(false);
        }
    }
    public void DestroyTrictangles()
    {
        for (int i = 0; i < spawnedTrictangles.Count; i++)
        {
            if (spawnedTrictangles[i] != null)
            {
                Destroy(spawnedTrictangles[i].gameObject);
            }
        }
    }

    void Update()
    {
        if (this.isSpawnBots)
        {
            if (this.timeSpawnBots < 0)
            {
                if (this.spawnQuqueBots.Count > 0)
                {
                    float x = this.transform.position.x + r.Next(-50, 50);
                    float y = this.transform.position.y + r.Next(-50, 50);
                    this.timeSpawnBots = 2f;
                    this.spawnedTrictangles.Add(Instantiate(spawnQuqueBots.Dequeue(), new Vector3(x, y, 1), this.transform.rotation));
                    this.spawnedTrictangles.Last().gameObject.SetActive(false);
                    isSpawnBotsTrictangle = 1.5f;
                    GameObject effect = Instantiate(SpawnEffect.gameObject, new Vector3(x, y, 1), Quaternion.identity);
                    effect.transform.GetComponent<ParticleSystem>().startColor = this.spawnedTrictangles.Last().gameObject.GetComponent<SpriteRenderer>().color;
                    Destroy(effect, 1.5f);
                }
            }
            else
                this.timeSpawnBots -= Time.deltaTime;

            if (spawnedTrictangles.Count > 0)
            {
                if (isSpawnBotsTrictangle < 0)
                {
                    if (this.spawnedTrictangles.Last() != null)
                    {
                        this.spawnedTrictangles.Last().gameObject.SetActive(true);
                        isSpawnBotsTrictangle = 1.5f;
                    }
                }
                else
                    isSpawnBotsTrictangle -= Time.deltaTime;
            }



            int count = 0;
            if (spawnQuqueBots.Count == 0)
            {
                for (int i = 0; i < this.spawnedTrictangles.Count; i++)
                {
                    if (this.spawnedTrictangles?[i] == null)
                    {
                        count++;
                    }
                    if (count == this.spawnedTrictangles.Count)
                    {
                        this.isSpawnBots = false;
                        OpenDoors();
                    }
                }
            }
        }
    }

    public void SpawneTrictangles(int id)
    {
        for (int i = 0; i <lvlMenegerScript.LvlNow + 1; i++)
        {
            spawnQuqueBots.Enqueue(PrefabsTrictangle[r.Next(0, id)]);
        }
        this.timeSpawnBots = 3f;
        this.isSpawnBots = true;
    }

    private void ClipAudio(AudioClip clip)
    {
        this.gameObject.GetComponent<AudioSource>().clip = clip;
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}