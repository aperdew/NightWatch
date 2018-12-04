using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {
    public int bulletDamage = 10;
    int delay;
    bool enableBulletCollision;
    public GameObject owner;
	// Use this for initialization
	void Start () {
        enableBulletCollision = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!enableBulletCollision)
        {
            if (GetDistanceFromOwner() > 0.5f)
            {
                enableBulletCollision = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (enableBulletCollision &&  other.transform.parent != null)
        {
            GameObject parentGameObject = other.transform.parent.gameObject;
            if (parentGameObject.tag == "Enemy" || parentGameObject.tag == "Player")
            {
                parentGameObject.GetComponent<Health>().takeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }        
    }

    public float GetDistanceFromOwner()
    {
        if(owner == null)
        {
            return 0f;
        }
        return Vector3.Distance(gameObject.transform.position, owner.transform.position);
    }
}
