using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlaceables : MonoBehaviour {

    public GameObject woodHousePrefab;
    private GameObject ghostPrefab;
    private GameObject currentPrefab;
    private GameObject newRoom;
    private GameObject floor;
    private bool isInSpawnMode;

	// Use this for initialization
	void Start () {
        isInSpawnMode = false;
        currentPrefab = woodHousePrefab;
        floor = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleIsInSpawnMode();
        }
        if (isInSpawnMode)
        {
            UpdateGhostPrefabPosition();
            if (Input.GetButtonDown("Fire1") && !ghostPrefab.GetComponentInChildren<CheckObjectBoundingBox>().IsIntersecting)
            {
                newRoom = ghostPrefab;
                newRoom.tag = currentPrefab.tag;
                newRoom.transform.Find("ObjectBoundingBox").gameObject.SetActive(false);
                newRoom.GetComponent<HouseOccupants>().enabled = true;
                ghostPrefab = null;
                isInSpawnMode = false;
            }
        }
    }

    public void ToggleIsInSpawnMode()
    {
        isInSpawnMode = !isInSpawnMode;
        GetComponent<ClickOrders>().ResetSelectedColonist();
        if (isInSpawnMode)
        {
            CreateGhostPrefab();
        }
        else
        {
            if (ghostPrefab != null)
            {
                DestroyGhostPrefab();
            }
        }
    }

    public void RotateGhostPrefab()
    {
        if (ghostPrefab != null)
        {
            ghostPrefab.transform.eulerAngles += new Vector3(0, 90, 0);
        }
    }

    GameObject CreatePrefabAtMousePosition()
    {
        GameObject prefab;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Ground")
            {
                prefab = Instantiate(currentPrefab, hit.point, Quaternion.identity);
                return prefab;
            }
        }
        prefab = Instantiate(currentPrefab, hit.point, Quaternion.identity);
        return prefab;
    }

    void UpdateGhostPrefabPosition()
    {
        if (ghostPrefab != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateGhostPrefab();
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                ghostPrefab.transform.position = new Vector3(hit.point.x, floor.transform.position.y, hit.point.z);
                //if (hit.transform.parent != null && (hit.transform.name == "Ground" || hit.transform.parent.gameObject.tag == "GhostPrefab"))
                //{
                //    ghostPrefab.transform.position = hit.point; //new Vector3(hit.point.x, floor.transform.position.y + 0.5f, hit.point.z);
                //}
            }
        }
    }

    void CreateGhostPrefab()
    {
        ghostPrefab = CreatePrefabAtMousePosition();
        ghostPrefab.tag = "GhostPrefab";
        ghostPrefab.transform.Find("ObjectBoundingBox").gameObject.SetActive(true);
    }

    void DestroyGhostPrefab()
    {
        Destroy(ghostPrefab);
        ghostPrefab = null;
    }
}
