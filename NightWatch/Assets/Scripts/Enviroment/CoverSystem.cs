using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSystem : MonoBehaviour
{

    public int coverEffectiveness = 4;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            int chance = Random.Range(1, 10);
            if (chance <= coverEffectiveness && other.gameObject.GetComponent<BulletCollision>().GetDistanceFromOwner() > 1.5f)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
