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
    private Canvas canvas;
    private GameObject arrow;
    private RectTransform arrowRectTransform;
    public int id;
    public bool anchored = false;

	// Use this for initialization
	void Start () {
        if (GetComponentInParent<Canvas>() == null)      //Spawned objects aren't put on the canvas, so this will happen on clients
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            this.transform.SetParent(canvas.transform);
        }
        else canvas = this.GetComponentInParent<Canvas>();
        rigidBody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if(anchored)DrawArrow(anchorTransform.localPosition, this.gameObject.transform.localPosition);
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
	}
    private void DrawArrow(Vector2 arrowStart, Vector2 canvasPosition)
    {
        if (arrow == null)
        {
            arrow = Instantiate(Resources.Load("Prefabs/ConnectorLine", typeof(GameObject))) as GameObject;
            arrow.transform.SetParent(canvas.transform);
            arrowRectTransform = (RectTransform)arrow.transform;
            arrowRectTransform.localScale = new Vector3(1, 1, 1);
        }
        float angle = Mathf.Atan2(canvasPosition.y - arrowStart.y, canvasPosition.x - arrowStart.x) * 180 / Mathf.PI;
        //arrow.transform.localScale = new Vector3((canvasPosition - arrowStart).magnitude/500, 1, 1);
        arrowRectTransform.offsetMin = new Vector2(0, 0);
        arrowRectTransform.offsetMax = new Vector2((canvasPosition - arrowStart).magnitude, 5);
        arrow.transform.localPosition = canvasPosition - ((canvasPosition - arrowStart) / 2);
        arrow.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
