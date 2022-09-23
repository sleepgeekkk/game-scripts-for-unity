using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerasmooth : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isleft;
    private Transform player;
    private int lastX;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isleft);

    }

    

    public void FindPlayer(bool playerisleft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);

        if(playerisleft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if(player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) isleft = false; else if (currentX < lastX) isleft = true;

            Vector3 target;
            if (isleft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }
            Vector3 currentposition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);

            transform.position = currentposition;        }
    }
}
