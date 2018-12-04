using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOrders : MonoBehaviour {

    GameObject selectedPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.transform.tag == "Player")
                {
                    selectedPlayer = hit.transform.gameObject;
                }
                else 
                {
                    selectedPlayer = null;
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if ( hit.transform.tag != "Player")
                {
                    UnityEngine.AI.NavMeshAgent nav = selectedPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    nav.isStopped = false;
                    nav.SetDestination(hit.point);
                    if(hit.transform.tag == "Enemy")
                    {
                        selectedPlayer.GetComponent<PlayerAttack>().enabled = true;
                    }
                    else
                    {
                        selectedPlayer.GetComponent<PlayerAttack>().StopAttacking();
                    }
                }
            }
        }
    }
}
