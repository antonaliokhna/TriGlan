using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BoostsScript : MonoBehaviour
{
    public GameObject Coin;

    public int BoostID;
    public string BoostName;
    public float value;


    public static System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 25f);
    }

    public void SpawnBootsObject(string[] BoostInfo)
    {
        this.BoostID = Convert.ToInt32(BoostInfo[0]);
        this.BoostName = BoostInfo[1];
        this.value = (float)Convert.ToDouble(BoostInfo[2], CultureInfo.InvariantCulture);
        Sprite sprite = Resources.Load($"Sprite/Boosts/{this.BoostName}", typeof(Sprite)) as Sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

}
