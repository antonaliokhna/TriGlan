using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstructChest : MonoBehaviour
{
    protected ServerSingleGame server;
    protected AudioSource CasheThisAudioSource;

    protected string ObjectName;
    protected int price;
    protected int WeaponID;
    protected int BoostID;
    protected (int, int) MinMaxGenerateWeaponValue;
    protected (int, int) MinMaxGenerateBoostValue = (1,5);

    public GameObject weaponObject;
    public GameObject BoostHpObject;
    public AudioClip ErrorByChest;

    protected CameraAnimation cameraAnimation;

    public static System.Random r = new System.Random();


    public void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.tag == "Player")
            {
                MainTrictangle mainTrictangleScript = other.gameObject.GetComponent<MainTrictangle>();
                if (mainTrictangleScript.CountCoin >= this.price)
                {
                    mainTrictangleScript.CountCoin -= this.price;
                    if (this.ObjectName == "Weapon") { 
                        var weapon = Instantiate(this.weaponObject, new Vector3(this.transform.position.x + (float)r.Next(-5, 6), this.transform.position.y + (float)r.Next(-5, 6)), this.transform.rotation *= Quaternion.Euler(0f, 0f, (float)r.Next(1, 361)));
                        var fuckingProgramm =  server.WeaponsDictionaty[this.WeaponID - 1];
                        Debug.Log(string.Join(" ",fuckingProgramm));
                        weapon.gameObject.GetComponent<WeaponScript>().SpawnWeaponObject(server.WeaponsDictionaty[this.WeaponID-1]);
                    }
                    else {
                        var boostHp = Instantiate(this.BoostHpObject, new Vector3(this.transform.position.x + (float)r.Next(-5, 6), this.transform.position.y + (float)r.Next(-5, 6)), this.transform.rotation *= Quaternion.Euler(0f, 0f, (float)r.Next(1, 361)));
                        boostHp.gameObject.GetComponent<BoostsScript>().SpawnBootsObject(server.BoostsDictionaty[this.BoostID - 1]);
                    }
                    mainTrictangleScript.glowMeneger.OpenChest();
                    Destroy(this.gameObject);
                }
                else 
                { 
                    ClipAudio(ErrorByChest);
                    cameraAnimation.Shake();
                }
            }
        }
    }

    public void GenerateObject()
    {
        if (r.Next(0, 100) > 75)
        {
            this.BoostID = r.Next(this.MinMaxGenerateBoostValue.Item1, this.MinMaxGenerateBoostValue.Item2);
            this.ObjectName = "Boost";
        }
        else
        {
            this.WeaponID = r.Next(this.MinMaxGenerateWeaponValue.Item1, this.MinMaxGenerateWeaponValue.Item2);
            this.ObjectName = "Weapon";
        }
    }

    private void ClipAudio(AudioClip clip)
    {
        this.CasheThisAudioSource.clip = clip;
        this.CasheThisAudioSource.Play();
    }
}
