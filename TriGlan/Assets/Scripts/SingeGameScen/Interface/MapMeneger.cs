using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapMeneger : MonoBehaviour
{
    private GameObject player;
    private InterfaceManager interfaceManager;
    public GameObject[] mapsObjects;
    public GameObject BoosRoom;
    private bool isBossLvl = false;
    // Start is called before the first frame update
    void Start()
    {
        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //for boss
        if (interfaceManager.lvlMeneger.LvlNow != 10)
        {
            transform.localPosition = new Vector2((player.transform.position.x * 1.161f) + 500f, (player.transform.position.y * 1.161f) - 250f);
            if (isBossLvl)
            {
                isBossLvl = false;
                BoosRoom.SetActive(false);
            }
        }
        else //for normalLvl
        {
            if (isBossLvl)
                transform.localPosition = new Vector2((player.transform.position.x * 6.09f) + 375f, (player.transform.position.y * 6.13f) - 610f);
            else
            {
                isBossLvl = true;
                BoosRoom.SetActive(true);
                for (int i = 0; i <= mapsObjects.GetLength(0); i++)
                    mapsObjects[i].SetActive(false);
            }
        }
    }
}
