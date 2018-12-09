using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {
    public int bulletDamage = 10;
    int delay;
    bool enableBulletCollision;
    public GameObject owner;
    GameObject bulletHitEffect;
	// Use this for initialization
	void Start () {
        enableBulletCollision = false;
        bulletHitEffect = transform.Find("BulletHit").gameObject;
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
                DestroyProjectile();
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

    public void DestroyProjectile()
    {
        Debug.Log("playing");
        //bulletHitEffect.transform.parent = null;
        bulletHitEffect.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 2f);
    }
}
