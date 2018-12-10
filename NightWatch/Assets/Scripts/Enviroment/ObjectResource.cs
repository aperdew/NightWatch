﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResource : MonoBehaviour {
    public string resourceName;
    public float resourceWeight;
	// Use this for initialization
	void Start () {
        Resource = new Item(resourceName, resourceWeight);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Item Resource { get; private set; }
}
