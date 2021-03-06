using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLvlCube : MonoBehaviour
{
    public GameObject lvlMeneger;
    private CameraAnimation cameraAnimation;
    private GlowMeneger glowMeneger;

    // Start is called before the first frame update
    void Start()
    {
        glowMeneger = GameObject.FindGameObjectWithTag("GlowMeneger").GetComponent<GlowMeneger>();
        cameraAnimation = GameObject.FindGameObjectWithTag("CameraM").GetComponent<CameraAnimation>();
        lvlMeneger = GameObject.Find("LvlMeneger");
    }
    public void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            MainTrictangle mainTrictangle = other.gameObject.GetComponent<MainTrictangle>();
            lvlMeneger.GetComponent<LvlMeneger>().LvlNow++;
            other.gameObject.transform.position = new Vector3(0f, 0f, 1f);
            cameraAnimation.Shake();
            glowMeneger.TakeNextLvl();
        }
        if (other.gameObject.tag == "Chest")
            Destroy(other.gameObject);
    }
}
