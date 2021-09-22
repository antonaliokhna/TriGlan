using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using UnityEngine;

public class CircleAttacScript : MonoBehaviour 
{
    public int UserID;
    public float speed;
    public int damage;
    public AudioClip AudioShot; 
    public ParticleSystem DestroyEffect;
    public float TimeScale = 0;

    private Vector3 mousePos;
    private Vector2 secondPos = new Vector2();
    private static System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position.x - mousePos.x >= 0)
            secondPos.x = (mousePos.x - 1000);
        else
            secondPos.x = (mousePos.x + 1000);
        secondPos.y = (secondPos.x - transform.position.x) * (mousePos.y - transform.position.y) /
             (mousePos.x - transform.position.x) + transform.position.y;

        int randomNumber = Math.Abs(Convert.ToInt32(((TimeScale + TimeScale) * 165f) - 75f));

        secondPos.x += random.Next(-randomNumber, randomNumber);
        secondPos.y += random.Next(-randomNumber, randomNumber);

        if (Math.Sqrt((Math.Pow(mousePos.x - transform.position.x, 2d) + (Math.Pow(mousePos.y - transform.position.y, 2d)))) < 3.5f)
            Destroy(this.gameObject);

        Destroy(this.gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, secondPos, Time.deltaTime * speed);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "RedAmmo" && other.gameObject.tag != "Weapon" && other.gameObject.tag != "BlueAmmo" && other.gameObject.tag != "Player") 
        {
            SpawnParticalEffect();
            Destroy(this.gameObject);
        }
    }
    
    public void SpawnParticalEffect()
    {
        this.DestroyEffect.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        Vector3 Brickpos = transform.position;
        Vector3 spawnPosition = new Vector3(Brickpos.x, Brickpos.y, Brickpos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        Destroy(effect, 1f);
    }

}
