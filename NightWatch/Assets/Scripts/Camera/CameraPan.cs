using UnityEngine;

public class CameraPan : MonoBehaviour {

    public float panSpeed = 20f;
    public Vector2 Panlimit = new Vector2(15, 15);
    private Vector3 deltaPosition = new Vector3();
    private Vector3 position = new Vector3();
    private float xAxis;
    private float zAxis;

    void Start () {
        transform.position = deltaPosition;
    }

	void Update () {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        float modifiedPanSpeed = panSpeed / Time.timeScale;
        
        deltaPosition.x = xAxis * modifiedPanSpeed;
        deltaPosition.z = zAxis * modifiedPanSpeed;
        Vector3 movement = new Vector3(deltaPosition.x, 0, deltaPosition.z);
        Vector3.ClampMagnitude(movement, modifiedPanSpeed);

        movement *= Time.deltaTime;
        transform.Translate(movement);
       
    }

    public float XAxis
    { get { return xAxis; } set { xAxis = value; } }

    public float ZAxis
    { get { return zAxis; } set { zAxis = value; } }
}
