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
    private enum State { GoingToJob, GatheringResources, HaulingResources}
    private State currentState;
    private float remainingDistance;
    private float collectionTime;
    private float collectionDistance;
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
        currentState = State.GoingToJob;
    }

    private void OnEnable()
    {
        currentState = State.GoingToJob;
    }

    // Update is called once per frame
    void Update() {
        switch (currentState)
        {
            case State.GoingToJob:
                if (!inventory.DoesInventoryHaveEnoughRoom(resource))
                {
                    nav.SetDestination(stockpile.transform.position);
                    currentState = State.HaulingResources;
                }
                else
                {
                    remainingDistance = Vector3.Distance(jobLocation.transform.position, transform.position);
                    if (remainingDistance <= collectionDistance)
                    {
                        isAtJobLocation = true;
                        nav.isStopped = true;
                        currentState = State.GatheringResources;
                    }
                }
                break;
            case State.GatheringResources:
                remainingDistance = Vector3.Distance(jobLocation.transform.position, transform.position);
                if (inventory.DoesInventoryHaveEnoughRoom(resource) && remainingDistance <= collectionDistance)
                {
                    timerTime += Time.deltaTime;
                    if (timerTime >= collectionTime)
                    {
                        Debug.Log("Got Resource");
                        timerTime = 0;
                        inventory.Add(resource);
                    }
                }
                else if(!inventory.DoesInventoryHaveEnoughRoom(resource))
                {
                    nav.isStopped = false;
                    nav.SetDestination(stockpile.transform.position);
                    isDroppingOffSupplies = true;
                    Debug.Log("Inventory full");
                    currentState = State.HaulingResources;
                }
                break;
            case State.HaulingResources:
                remainingDistance = Vector3.Distance(stockpile.transform.position, transform.position);
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
                        currentState = State.GoingToJob;
                    }
                    else
                    {
                        Debug.Log("Transfer failed.  Idle state active.");
                        gameObject.GetComponent<ColonistInfo>().ResetCurrentJob();
                    }
                }
                break;
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

    public void SetResource(ObjectResource item)
    {
        resource = item.Resource;
        collectionDistance = item.collectionDistance;
        collectionTime = item.collectionTime;
    }
}
