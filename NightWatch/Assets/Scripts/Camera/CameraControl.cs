using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Contoller for the in-game camera
public class CameraControl : MonoBehaviour {
    
    public float scrollSpeed = 20f;
    private Vector3 angledPosition;
    private Vector3 topDownPosition;
    private bool isTopDown = false;
    private float cameraAngle = Mathf.Deg2Rad*60;
    public float minScrollHeight = 1f;
    public float maxScrollHeight = 60f;

    // Use this for initialization
    void Start () {
        angledPosition.y = Mathf.Floor((minScrollHeight + maxScrollHeight) / 2);
        angledPosition.z = -(angledPosition.y / Mathf.Tan(cameraAngle));
        transform.localPosition = angledPosition;
	}
	
	void LateUpdate () {
        float modifiedScrollSpeed = scrollSpeed / Time.timeScale;

        if (Input.GetKeyDown("t") && isTopDown == false)
        {
            topDownPosition = new Vector3(0, transform.localPosition.y, 0);
            transform.localPosition = topDownPosition;
            transform.Rotate(30, 0, 0);
            isTopDown = true;
        }
        else if (Input.GetKeyDown("t") && isTopDown == true)
        {
            angledPosition.y = topDownPosition.y;
            angledPosition.z = -(angledPosition.y / Mathf.Tan(cameraAngle));
            transform.localPosition = angledPosition;
            transform.Rotate(-30, 0, 0);
            isTopDown = false;
        }

        float scrollDistance = Input.GetAxis("Mouse ScrollWheel") * modifiedScrollSpeed * 100f * Time.deltaTime;

        if (isTopDown)
        {
            if (transform.localPosition.y - scrollDistance > maxScrollHeight)
            {
                topDownPosition.y = maxScrollHeight;
                transform.localPosition = topDownPosition;
            }
            else if (transform.localPosition.y - scrollDistance < minScrollHeight)
            {
                topDownPosition.y = minScrollHeight;
                transform.localPosition = topDownPosition;
            }
            else
            {
                transform.Translate(0, 0, scrollDistance);
                topDownPosition = transform.localPosition;
            }
        }
        else
        {
            scrollDistance = scrollDistance * Mathf.Sin(cameraAngle);

            if (transform.localPosition.y - scrollDistance > Mathf.Abs(maxScrollHeight))
            {
                angledPosition.y = maxScrollHeight;
                angledPosition.z = -(angledPosition.y / Mathf.Tan(cameraAngle));
                transform.localPosition = angledPosition;
            }
            else if (transform.localPosition.y - scrollDistance < Mathf.Abs(minScrollHeight))
            {
                angledPosition.y = minScrollHeight;
                angledPosition.z = -(angledPosition.y / Mathf.Tan(cameraAngle));
                transform.localPosition = angledPosition;
            }
            else
            {
                transform.Translate(0, 0, scrollDistance);
                angledPosition = transform.localPosition;
            }
        }
	}
}
