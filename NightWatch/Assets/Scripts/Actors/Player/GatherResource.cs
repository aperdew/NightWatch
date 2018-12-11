using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResource : MonoBehaviour, Job {
    private GameObject jobLocation;
    private GameObject stockpile;
    private ResourceManager resourceManager;
    private bool isAtJobLocation;
    private bool isDroppingOffSupplies;
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
        timerTime = 0;
        stockpile = GameObject.FindGameObjectWithTag("Stockpile");
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAtJobLocation)
        {
            var remainingDistance = Vector3.Distance(jobLocation.transform.position, transform.position);
            if (remainingDistance <= 2f)
            {                
                isAtJobLocation = true;
                nav.isStopped = true;
            }
        }
        else if(!isDroppingOffSupplies && isAtJobLocation)
        {
            if (inventory.DoesInventoryHaveEnoughRoom(resource))
            {
                timerTime += Time.deltaTime;
                if (timerTime >= resource.CollectionTime)
                {
                    Debug.Log("Got Resource");
                    timerTime = 0;
                    inventory.Add(resource);
                }
            }
            else
            {
                nav.isStopped = false;
                nav.SetDestination(stockpile.transform.position);
                isDroppingOffSupplies = true;
                Debug.Log("Inventory full");
            }
        }
        else
        {
            var remainingDistance = Vector3.Distance(stockpile.transform.position, transform.position);
            if (remainingDistance <= 3f)
            {
                Inventory stockpileInventory = stockpile.GetComponent<Inventory>();
                bool wasTransferSuccessful = inventory.TransferAll(stockpileInventory);
                if (wasTransferSuccessful)
                {
                    resourceManager.UpdateResourceTotals();
                    isAtJobLocation = false;
                    isDroppingOffSupplies = false;
                    SetTargetDestination();
                }
                else
                {
                    Debug.Log("Transfer failed.  Idle state active.");
                    gameObject.GetComponent<ColonistInfo>().ResetCurrentJob();
                }
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
