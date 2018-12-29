using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectBoundingBox : MonoBehaviour
{


    public Material ghostPrefabMaterial;
    private Material intersectingGhostPrefabMaterial;

    // Use this for initialization
    void Start()
    {
        intersectingGhostPrefabMaterial = new Material(ghostPrefabMaterial);
        intersectingGhostPrefabMaterial.color = new Color(1, 0, 0, ghostPrefabMaterial.color.a);
        IsIntersecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsIntersecting)
        {
            gameObject.GetComponent<MeshRenderer>().material = intersectingGhostPrefabMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = ghostPrefabMaterial;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.tag != "GhostPrefab" && collider.name !="Ground")
        {
            IsIntersecting = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag != "GhostPrefab" && collider.name != "Ground")
        {
            IsIntersecting = false;
        }
    }

    public bool IsIntersecting { get; set; }
}