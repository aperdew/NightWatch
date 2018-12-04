using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{

    public GameObject projectile;
    GameObject fire;
    GameObject gameProjectile;
    RifleInfo rifleInfo;
    int timer;
    int delay;

    // Use this for initialization
    void Start()
    {
        rifleInfo = GetComponent<RifleInfo>();
        delay = rifleInfo.RateOfFire;
        timer = delay;
        fire = transform.Find("Fire").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer == delay)
        {
            timer = 0;
            Fire();
        }
        else
        {
            timer++;
        }
    }

    void Fire()
    {
        
                gameProjectile = Instantiate(projectile, fire.transform.position, fire.transform.rotation);
                gameProjectile.GetComponent<BulletCollision>().bulletDamage = rifleInfo.Damage;
                gameProjectile.GetComponent<Rigidbody>().AddRelativeForce(fire.transform.forward * rifleInfo.bulletSpeed);
                gameProjectile.transform.Rotate(Vector3.left * -90);
                Destroy(gameProjectile, 5f);
        
    }
}
