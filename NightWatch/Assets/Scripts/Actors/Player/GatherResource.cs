using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResource : MonoBehaviour, Job {
    private GameObject jobLocation;
    private bool isAtJobLocation;
    private bool isDroppingOffSupplies;
    private float maxTimerTime;
    private float timerTime;
    private Inventory inventory;
    private Item resource;
    UnityEngine.AI.NavMeshAgent nav;

    // Use this for initialization
    void Awake () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        inventory = GetComponent<Inventory>();
        isAtJobLocation = false;
        isDroppingOffSupplies = false;
        maxTimerTime = 3;
        timerTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAtJobLocation)
        {
            var remainingDistance = Vector3.Distance(jobLocation.transform.position, transform.position);
            if (remainingDistance <= 2f)
            {
                Debug.Log("At tree");
                isAtJobLocation = true;
                nav.isStopped = true;
            }
        }
        else if(!isDroppingOffSupplies && isAtJobLocation)
        {
            if (inventory.DoesInventoryHaveEnoughRoom(resource))
            {
                timerTime += Time.deltaTime;
                if (timerTime >= maxTimerTime)
                {
                    timerTime = 0;
                    inventory.Add(resource);
                }
            }
            else
            {
                nav.isStopped = false;
                nav.SetDestination(GetComponent<ColonistInfo>().House.transform.position);
                isDroppingOffSupplies = true;
                Debug.Log("Inventory full");
            }
        }
        else
        {
            var remainingDistance = Vector3.Distance(GetComponent<ColonistInfo>().House.transform.position, transform.position);
            if (remainingDistance <= 3f)
            {
                Debug.Log("At house");
                Inventory houseInventory = GetComponent<ColonistInfo>().House.GetComponent<Inventory>();
                houseInventory.Add(inventory.Transfer(inventory.GetBundle(resource)));
                isAtJobLocation = false;
                isDroppingOffSupplies = false;
                SetTargetDestination();
            }
        }
        

    }

    public void SetTargetDestination(GameObject target)
    {
        jobLocation = target;
        nav.SetDestination(target.transform.position);
    }

    public void SetTargetDestination()
    {
        nav.SetDestination(jobLocation.transform.position);
    }

    public void SetResource(Item item)
    {
        resource = item;
    }
}
