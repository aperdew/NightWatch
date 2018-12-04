using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleInfo : MonoBehaviour {
    public readonly int damage = 10;
    public readonly int rateOfFire = 20;
    public readonly float range = 10f;
    public readonly int bulletSpeed = 1000;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int BulletSpeed
    { get { return bulletSpeed; } }

    public int Damage
    { get { return damage; } }

    public int RateOfFire
    { get { return rateOfFire; } }

    public float Range
    { get { return range; } }
}
