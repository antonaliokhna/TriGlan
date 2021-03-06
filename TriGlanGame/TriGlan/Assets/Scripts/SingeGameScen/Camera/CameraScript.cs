using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float dumping = 3.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    public GameObject player;
    private int lastX;
    private Camera cacheCamera;
 
    // Start is called before the first frame update
    void Start()
    {
        cacheCamera = Camera.main;
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        cacheCamera.orthographicSize = 1f;
        FindPlayer(isLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && cacheCamera.orthographicSize <32.5f)
        {
            cacheCamera.orthographicSize += 0.05f;
        }
        else if (cacheCamera.orthographicSize>29f)
        {
            cacheCamera.orthographicSize -= 0.05f;
        }
        if (cacheCamera.orthographicSize < 24f)
        {
            cacheCamera.orthographicSize +=0.2f;
            if (cacheCamera.orthographicSize == 23.8f)
            {
                cacheCamera.orthographicSize = 24f;
            }
        }

        if (player)
        {
            int currentX = Mathf.RoundToInt(player.transform.position.x);
            if (currentX > lastX)
            {
                isLeft = false;
            }
            else if (currentX < lastX)
            {
                isLeft = true;
            }
            lastX = Mathf.RoundToInt(player.transform.position.x);
            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.transform.position.x - offset.x, player.transform.position.y - offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
            }
            Vector3 curentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime*2.5f);
            transform.position = curentPosition;
        }
    }
    public void FindPlayer(bool playerLeft)
    {
        lastX = Mathf.RoundToInt(player.transform.position.x);
        if (playerLeft)
        {
            transform.position = new Vector3(player.transform.position.x - offset.x, player.transform.position.y - offset.y, transform.position.z);
        }
        if (playerLeft)
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
        }
    }
}
