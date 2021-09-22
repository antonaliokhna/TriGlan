using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public int weaponID;
    public string weapon;
    public int damage;
    public float sizeBoolet;
    public float speed;
    public float timeShot;
    public int countShot;

    public void CreateWeapon(int weaponID, string weapon, int damage, float sizeBoolet, float speed, float timeShot, int countShot)
    {
        this.weaponID = weaponID;
        this.weapon = weapon;
        this.damage = damage;
        this.sizeBoolet = sizeBoolet;
        this.speed = speed;
        this.timeShot = timeShot;
        this.countShot = countShot;
    }

    public void SpawnWeaponObject(string[] WeaponInfo)
    {
        this.weaponID = Convert.ToInt32(WeaponInfo[0]);
        this.weapon = WeaponInfo[1];
        Sprite sprite = Resources.Load($"Sprite/Weapons/{this.weapon}", typeof(Sprite)) as Sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite; 

        this.damage = Convert.ToInt32(WeaponInfo[2]); 
        this.sizeBoolet = (float)Convert.ToDouble(WeaponInfo[3], CultureInfo.InvariantCulture);
        this.speed = Convert.ToInt32(WeaponInfo[4]);
        this.timeShot = (float)Convert.ToDouble(WeaponInfo[5], CultureInfo.InvariantCulture);
        this.countShot = Convert.ToInt32(WeaponInfo[6]);

        this.gameObject.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 40f);
    }
}
