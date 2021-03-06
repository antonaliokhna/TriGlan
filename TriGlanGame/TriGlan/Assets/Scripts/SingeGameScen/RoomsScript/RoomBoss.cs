using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class RoomBoss : MonoBehaviour
{
    public GameObject[] PrefabsTrictangle;

    public ParticleSystem SpawnEffect;
    public List<GameObject> spawnedTrictangles;
    public List<GameObject> spawnedTrictanglesEffect;

    public float globalSpawnBots = 16f;
    private GameObject boss;

    public float TimeSpawnBot = 0f;
    public float TimeSpawnOtherBot = 0f;
    public float floatSpawnBos = 0;
    public GameObject ArmorRorate;
    private float rotateArmor = 0;
    private static System.Random r = new System.Random();
    private bool IsDestroy = false;

    private GlowMeneger glowMeneger;
    private CameraAnimation cameraAnimation;

    public void DestroyRoomObjects()
    {
        for (int i = 0; i < spawnedTrictangles.Count; i++)
        {
            if (spawnedTrictangles?[i] != null)
                Destroy(spawnedTrictangles[i].gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        spawnedTrictangles = new List<GameObject>();
        spawnedTrictanglesEffect = new List<GameObject>();
        ArmorRorate.SetActive(false);
        boss = this.gameObject.transform.Find("Boss").gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if (boss == null && !IsDestroy)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            this.gameObject.GetComponent<AudioSource>().Play();
            this.gameObject.GetComponent<AudioSource>().Play();
            IsDestroy = true;
        }
        RotateAmmo();

        if (globalSpawnBots > 0)
        {
            globalSpawnBots -= Time.deltaTime;

            if (TimeSpawnOtherBot < 0)
            {
                float x = r.Next(-60, 60);
                float y = r.Next(20, 70);
                this.TimeSpawnOtherBot = 2f; //3

                this.spawnedTrictangles.Add(Instantiate(PrefabsTrictangle[(r.Next(0, PrefabsTrictangle.GetLength(0)))], new Vector3(x, y, 1), this.transform.rotation));
                this.spawnedTrictangles.Last().gameObject.SetActive(false);

                floatSpawnBos = 1.5f;
                GameObject effect = Instantiate(SpawnEffect.gameObject, new Vector3(x, y, 1), Quaternion.identity);
                effect.transform.GetComponent<ParticleSystem>().startColor = this.spawnedTrictangles.Last().gameObject.GetComponent<SpriteRenderer>().color;
                Destroy(effect, 1.5f);
            }
            else
                TimeSpawnOtherBot -= Time.deltaTime;
        }

        if (this.spawnedTrictangles.Count > 0)
        {
            ArmorRorate.gameObject.SetActive(true);
            if (floatSpawnBos < 0)
            {
                if (this.spawnedTrictangles.Last() != null)
                {
                    this.spawnedTrictangles.Last().gameObject.SetActive(true);
                    floatSpawnBos = 1.5f;
                }
            }
            else
                floatSpawnBos -= Time.deltaTime;

            if (spawnedTrictangles.Count >= 5)
            {
                int count = 0;
                for (int i = 0; i < spawnedTrictangles.Count; i++)
                {
                    if (spawnedTrictangles[i] == null)
                        count++;
                    else
                        break;
                }
                ResetSpawn(count);
            }
        }

    }

    private void ResetSpawn(int count)
    {
        if (count == spawnedTrictangles.Count)
        {
            spawnedTrictangles.Clear();
            globalSpawnBots = 35f;
            TimeSpawnOtherBot = 6.5f;
            ArmorRorate.gameObject.SetActive(false);
            glowMeneger.DoorClose();
            cameraAnimation.Shake();
        }
    }

    private void RotateAmmo()
    {
        if (rotateArmor < 0.3f)
            ArmorRorate.gameObject.transform.Rotate(0, 0, 1.2f);
        else
            rotateArmor -= Time.deltaTime;
    }
}
