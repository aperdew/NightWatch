using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour, Job {
    private GameObject jobLocation;
    private bool isAtJobLocation;
    private bool isDroppingOffSupplies;
    private float maxTimerTime;
    private float timerTime;
    private Inventory inventory;
    private Item wood;
    UnityEngine.AI.NavMeshAgent nav;

    // Use this for initialization
    void Awake () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        inventory = GetComponent<Inventory>();
        isAtJobLocation = false;
        isDroppingOffSupplies = false;
        maxTimerTime = 3;
        timerTime = 0;
        wood = new Item("Wood", 50);
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
            if (inventory.DoesInventoryHaveEnoughRoom(wood))
            {
                timerTime += Time.deltaTime;
                if (timerTime >= maxTimerTime)
                {
                    timerTime = 0;
                    inventory.Add(wood);
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
                houseInventory.Add(inventory.Transfer(inventory.GetBundle(wood)));
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
}
