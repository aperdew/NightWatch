using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {
    public int bulletDamage = 10;
    int delay;
    bool enableBulletCollision;
	// Use this for initialization
	void Start () {
        enableBulletCollision = false;
        delay = 2;
	}
	
	// Update is called once per frame
	void Update () {
        delay -= 1;
        if(delay== 0 )
        {
            enableBulletCollision = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (enableBulletCollision && other.transform.tag != "Player" && other.transform.tag !="Enemy"&& other.transform.parent != null)
        {
            GameObject parentGameObject = other.gameObject.transform.parent.gameObject;
            if (parentGameObject.tag == "Enemy" || parentGameObject.tag == "Player")
            {
                parentGameObject.GetComponent<Health>().takeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
