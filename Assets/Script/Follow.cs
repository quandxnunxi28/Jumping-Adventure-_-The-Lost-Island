﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject followObject;    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //thay đổi vị trí cảu camera theo game object sau 1 frame
        transform.position = followObject.transform.position + new Vector3(0, 0, -10);
            }
}
