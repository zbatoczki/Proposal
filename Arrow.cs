using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float MoveSpeed = 5;
    Vector3 direction;
    GameObject player;

    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = GetDirection();
    }

    private Vector3 GetDirection()
    {
        //get direction for arrow
        return player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (direction.normalized * MoveSpeed * Time.deltaTime);
        
    }


}
