using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CueBallLogic : MonoBehaviour {
	public List<GameObject> anchors = new List<GameObject> ();
	private Image circleImage;
	private Transform anchorTransform;
    private Rigidbody2D rigidBody2d;
	private Vector2 scaleVector = new Vector2(100f,100f);
	private Vector2 perpVector = new Vector2(1f,1f);
	private Vector2 differenceVector;
    private double currentAnchorDistance;
    private bool clockwise;
    private bool newAnchor = false;
	public int id;
    public bool anchored = false;

	// Use this for initialization
	void Start () {
        if (GetComponentInParent<Canvas>() == null)      //Spawned objects aren't put on the canvas, so this will happen on clients
        {
            this.transform.SetParent(GameObject.Find("Canvas").transform);
        }
        rigidBody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	} 

    void FixedUpdate() { 
        float time = Time.fixedDeltaTime;
        if (anchorTransform == null || !anchored) return;
        if (newAnchor)
        {
            differenceVector = anchorTransform.position - this.transform.position;
            perpVector = new Vector2(-differenceVector.y, differenceVector.x);
            perpVector.Normalize();
            perpVector.Scale(scaleVector);
            if (!clockwise)
                perpVector *= -1;
            rigidBody2d.velocity = (Vector3)perpVector;
            newAnchor = false;
            currentAnchorDistance = Vector3.Magnitude(differenceVector);
        }
        else
        {
            double circumference = currentAnchorDistance * 2 * Mathf.PI;
            double orbitTime = circumference / Vector3.Magnitude(rigidBody2d.velocity);
            double angleRotation = (clockwise ? -1 : 1) * 360 * (Time.fixedDeltaTime / orbitTime);
            rigidBody2d.velocity = Quaternion.Euler(0, 0, (float)angleRotation) * rigidBody2d.velocity;
        }
    }

	public void setCurrentAnchor(Transform newAnchorTransform){
        if (anchorTransform != null)
        {
            differenceVector = anchorTransform.position - this.transform.position;
            perpVector = new Vector2(-differenceVector.y, differenceVector.x);
            perpVector.Normalize();
            perpVector.Scale(scaleVector);
            if (!clockwise)
                perpVector *= -1;
        }
        anchorTransform = newAnchorTransform;
		differenceVector = newAnchorTransform.position - this.transform.position;
		float angle = Vector2.Angle (perpVector, differenceVector);
		Vector3 cross = Vector3.Cross (perpVector, differenceVector);
		if (cross.z > 0)
			angle = 0 - angle;
		clockwise = angle > 0;
        newAnchor = true;
        print(clockwise ? "clockwise" : "anticlockwise");
	}
}
