using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private ServerSingleGame server;
    public GameObject Coin;
    public GameObject boost;

    private CameraAnimation cameraAnimation;


    private (int, int) MinMaxGenerateBoostValue = (1, 5);
    private int BoostID;
    private bool isBoost = false;

    private static System.Random r = new System.Random();

    public int BoostID1
    {
        get => BoostID;
        set
        {
            this.BoostID = value;
            isBoost = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerSingleGame>();
        cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        GenerateObject();
    }

    public void GenerateObject()
    {
        if (r.Next(0, 100) > 75)
            this.BoostID1 = r.Next(this.MinMaxGenerateBoostValue.Item1, this.MinMaxGenerateBoostValue.Item2);
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainTrictangle mainTrictangleScript = other.gameObject.GetComponent<MainTrictangle>();
            if (mainTrictangleScript.jumpAnimateTime > 0)
            {
                if (this.isBoost)
                {
                    var boost = Instantiate(this.boost, new Vector3(this.transform.position.x + (float)r.Next(-5, 6), this.transform.position.y + (float)r.Next(-5, 6)), this.transform.rotation *= Quaternion.Euler(0f, 0f, (float)r.Next(1, 361)));
                    boost.gameObject.GetComponent<BoostsScript>().SpawnBootsObject(server.BoostsDictionaty[BoostID1 - 1]);
                    Destroy(boost, 50f);
                }
                else
                    Instantiate(Coin, new Vector3(transform.position.x, transform.position.y), transform.rotation *= Quaternion.Euler(0f, 0f, (float)r.Next(1, 361)));
                cameraAnimation.Shake();
                mainTrictangleScript.glowMeneger.OpenBox();
                Destroy(this.gameObject);
            }
        }
    }
}
