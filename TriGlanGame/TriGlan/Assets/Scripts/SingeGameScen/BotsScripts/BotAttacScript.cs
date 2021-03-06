using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BotAttacScript : MonoBehaviour
{
    public float speed;
    public ParticleSystem DestroyEffect;

    public GameObject mainTrictangle;
    private Vector3 mousePos;
    private Vector2 second = new Vector2();
    static System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        mousePos = mainTrictangle.transform.position;
        if (transform.position.x - mousePos.x >= 0)
            second.x = (mousePos.x - 1000);
        else
            second.x = (mousePos.x + 1000);
        second.y = (second.x - transform.position.x) * (mousePos.y - transform.position.y) /
             (mousePos.x - transform.position.x) + transform.position.y;

        if (Math.Sqrt((Math.Pow(transform.position.x - mousePos.x, 2d) + (Math.Pow(transform.position.y - mousePos.y, 2d)))) > 20f)
        {
            second.x += random.Next(-130, 130);
            second.y += random.Next(-130, 130);
        }
        else
        {
            second.x += random.Next(-100, 100);
            second.y += random.Next(-100, 100);
        }
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, second, Time.deltaTime * speed);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Player" || other.gameObject.tag == "Chest" || other.gameObject.tag == "Box" || other.gameObject.tag == "ArmorBoss")
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
