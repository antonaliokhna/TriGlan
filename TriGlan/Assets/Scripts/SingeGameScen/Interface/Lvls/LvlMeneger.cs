using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlMeneger : MonoBehaviour
{
    public GameObject TextLvl;
    private Text ObjTextLvl;

    public GameObject RoomPlacerMeneger;
    private RoomsPlacer RoomPlacer;

    public GameObject mainTrictangle;
    private MainTrictangle mainTrictangleScript;

    string[] lvls;
    int lvlNow = 1;

    public int LvlNow
    {
        get => lvlNow;
        set
        {
            lvlNow = value;
            ObjTextLvl.text = lvls[lvlNow - 1];
            mainTrictangleScript.InterfaceManager.StartCoroutineUpdateMatchInfo();

            RoomPlacer.DestroyRooms();
            if (lvlNow < 10)
            {
                RoomPlacer.DestroyBossRoom();
                RoomPlacer.StartSpawnRooms();
            }
            else
                RoomPlacer.SpawnBossRoom();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RoomPlacer = RoomPlacerMeneger.GetComponent<RoomsPlacer>();
        ObjTextLvl = TextLvl.GetComponent<Text>();
        mainTrictangle = GameObject.FindGameObjectWithTag("Player");
        mainTrictangleScript = mainTrictangle.GetComponent<MainTrictangle>();
        lvls = new string[] { "Very easy", "Easy", "Medium", "Hard", "Very hard", "Ultra hard", "Impossible", "Cheater!", "WTF ?", "Boss" };
    }
}
