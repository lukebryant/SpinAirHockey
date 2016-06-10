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
	private bool clockwise;
	public int id;
    public bool anchored = false;
	private bool active = false;

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
        if (anchorTransform ==null || !anchored) return;
        differenceVector = anchorTransform.position - this.transform.position;
		perpVector = new Vector2 (-differenceVector.y, differenceVector.x);
		perpVector.Normalize ();
		perpVector.Scale (scaleVector);
		if (!clockwise)
			perpVector *= -1;
        rigidBody2d.velocity = (Vector3)perpVector;	
	} 

	public void setCurrentAnchor(Transform newAnchorTransform){
		if (active) {
			anchorTransform = newAnchorTransform;
			differenceVector = newAnchorTransform.position - this.transform.position;
			float angle = Vector2.Angle (perpVector, differenceVector);
			Vector3 cross = Vector3.Cross (perpVector, differenceVector);
			if (cross.z > 0)
				angle = 0 - angle;
			clockwise = true ? angle > 0 : false;
    
		}
	}

    public void setActive(bool newActive)
    {
        active = newActive;
        if(active)print(id + " active");
    }
}
