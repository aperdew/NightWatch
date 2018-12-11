using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOrders : MonoBehaviour {

    GameObject selectedColonist;
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
                    if(selectedColonist != null)
                    {
                        selectedColonist.transform.Find("Body").transform.Find("SelectionCircle").gameObject.SetActive(false);
                    }
                    selectedColonist = hit.transform.gameObject;
                    selectedColonist.transform.Find("Body").transform.Find("SelectionCircle").gameObject.SetActive(true);
                }
                else 
                {
                    ResetSelectedColonist();
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (( hit.transform.tag != "Player" || hit.transform.tag != "Cover") && selectedColonist != null)
                {
                    UnityEngine.AI.NavMeshAgent nav = selectedColonist.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    nav.isStopped = false;
                    nav.SetDestination(hit.point);
                    if(hit.transform.tag == "Enemy")
                    {
                        selectedColonist.GetComponent<PlayerAttack>().enabled = true;
                    }
                    else
                    {
                        selectedColonist.GetComponent<PlayerAttack>().StopAttacking();
                    }
                    selectedColonist.GetComponent<ColonistInfo>().DetermineJob(hit.transform);
                }
            }
        }
    }

    public void ResetSelectedColonist()
    {
        if (selectedColonist != null)
        {
            selectedColonist.transform.Find("Body").transform.Find("SelectionCircle").gameObject.SetActive(false);
            selectedColonist = null;
        }
    }
}
