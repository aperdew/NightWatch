﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    List<GameObject> targets = new List<GameObject>();
    GameObject currentTarget;
    GameObject weapon;
    UnityEngine.AI.NavMeshAgent nav;
    // Use this for initialization
    void Start()
    {
        weapon = transform.Find("Hand").gameObject;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Go to the nearest piece of cover.  Do a raycast all. Order the results based upon distance.  Make sure there is cover between the enemy and the player.
        //multiHitInfo = (Physics.RaycastAll(transform.parent.position, transform.parent.forward, Mathf.Infinity));
        //System.Array.Sort(multiHitInfo, (x, y) => x.distance.CompareTo(y.distance));
        //foreach (var rh in multiHitInfo)

            if (currentTarget == null)
        {
            if (targets.Count > 0)
            {
                RemoveNullTargets();
                if (targets.Count > 0)
                {
                    currentTarget = targets[0];
                    nav.SetDestination(currentTarget.transform.position);
                }
            }
            nav.isStopped = false;
            weapon.GetComponentInChildren<FireGun>().enabled = false;
        }
        else if (currentTarget != null && weapon != null)
        {
            //if(currentTarget != null)
            //{
            //    float currentTargetDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
            //    foreach(GameObject target in targets)
            //    {
            //        float targetDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
            //        if ( targetDistance< currentTargetDistance )
            //        {
            //            currentTarget = target;
            //        }
            //    }
            //}

            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= weapon.GetComponentInChildren<RifleInfo>().Range)
            {
                nav.isStopped = true;
                transform.LookAt(currentTarget.transform);
                transform.Find("Hand").transform.LookAt(currentTarget.transform);
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.Find("Hand").transform.position, transform.Find("Hand").transform.TransformDirection(Vector3.forward), out hit, weapon.GetComponentInChildren<RifleInfo>().Range))
                {
                    if (hit.collider.transform.parent != null && hit.collider.transform.parent.tag == "Player")
                    {
                        weapon.GetComponentInChildren<FireGun>().enabled = true;
                    }
                    else
                    {
                        weapon.GetComponentInChildren<FireGun>().enabled = false;
                    }
                }
            }
            else
            {
                weapon.GetComponentInChildren<FireGun>().enabled = false;
                nav.isStopped = false;
                nav.SetDestination(currentTarget.transform.position);
            }
        }
        else
        {
            weapon = transform.Find("Hand").gameObject;
        }

    }

    public void StopAttacking()
    {
        if (weapon != null)
        {
            weapon.GetComponentInChildren<FireGun>().enabled = false;
        }
    }

    private void RemoveNullTargets()
    {
        targets.RemoveAll(item => item == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.Remove(other.gameObject);
            StopAttacking();
        }
    }
}
