using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonistInfo : MonoBehaviour {


    UnityEngine.AI.NavMeshAgent nav;

    // Use this for initialization
    void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public string Name { get; private set; }
    
    public GameObject House { get; set; }

    public string CurrentJob { get; private set; }

    public void DetermineJob(Transform hit)
    {
        switch (hit.tag)
        {
            case "Stone":
                SetCurrentJob("Stone Miner", hit.gameObject);
                break;
            case "Tree":
                SetCurrentJob("Lumberjack", hit.gameObject);
                break;
        }
    }

    public void SetCurrentJob(string newJob, GameObject jobLocation)
    {
        ResetCurrentJob();
        Behaviour newJobScript = GetJobScriptByName(newJob);
        if (newJobScript != null)
        {
            CurrentJob = newJob;
            Job job = (Job)newJobScript;
            newJobScript.enabled = true;
            job.SetTargetDestination(jobLocation);
            job.SetResource(jobLocation.GetComponent<ObjectResource>().Resource);
            nav.isStopped = false;
        }

    }

    private void ResetCurrentJob()
    {
        if (CurrentJob != null)
        {
            Behaviour currentJobScript = GetJobScriptByName(CurrentJob);
            if (currentJobScript != null)
            {
                currentJobScript.enabled = false;
                nav.isStopped = true;
            }
        }
    }

    private Behaviour GetJobScriptByName(string jobName)
    {
        Behaviour jobScript = null;
        switch(jobName)
        {
            case "Lumberjack":
            case "Stone Miner":
                jobScript = GetComponent<GatherResource>();
                break;
        }
        return jobScript;
    }
}
