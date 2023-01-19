﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Vector3 = UnityEngine.Vector3;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 50; i >= -50; i -= 10) {
        //    CreateRayCast(transform.position, transform.forward * 100 + transform.right * i, Color.red);
        //}
    }
    void CreateRayCast(Vector3 pos, Vector3 direction, Color color)
    {
        Ray ray = new Ray(pos, direction);

        Debug.DrawLine(pos, pos + direction, color);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            print(hit.point);
            print(hit.transform.position);
            print(hit.collider.gameObject);
        }
    }
    public void Move(Vector3 move_pos)
    {
        transform.position = transform.position + move_pos;
    }

}
