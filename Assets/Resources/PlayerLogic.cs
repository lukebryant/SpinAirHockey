﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour {
	public List<GameObject> anchors = new List<GameObject> ();
	//private GameObject currentAnchor;
	private Image circleImage;
	private Transform anchorTransform;
	private Vector2 scaleVector = new Vector2(2f,2f);
	private Vector2 perpVector = new Vector2(1f,1f);
	private Vector2 differenceVector;
	private bool clockwise;
	public int id;
	private bool active = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (anchorTransform ==null) return;
		//if (Input.GetKeyDown("0")) active = true ? id == 0 : false;
		// (Input.GetKeyDown("1")) active = true ? id == 1 : false;
        // (Input.GetKeyDown("2")) active = true ? id == 2 : false;
        // (Input.GetKeyDown("3")) active = true ? id == 3 : false;
        differenceVector = anchorTransform.position - this.transform.position;
		perpVector = new Vector2 (-differenceVector.y, differenceVector.x);
		perpVector.Normalize ();
		perpVector.Scale (scaleVector);
		if (!clockwise)
			perpVector *= -1;
		transform.position += (Vector3)perpVector;	
	} 

	public void setCurrentAnchor(Transform newAnchorTransform){
		if (active) {
			//currentAnchor = anchors [id];
			anchorTransform = newAnchorTransform;
			differenceVector = newAnchorTransform.position - this.transform.position;
			float angle = Vector2.Angle (perpVector, differenceVector);
			Vector3 cross = Vector3.Cross (perpVector, differenceVector);
			if (cross.z > 0)
				angle = 0 - angle;
			clockwise = true ? angle > 0 : false;
			print ("perpVector = " + perpVector + "differenceVector + " + differenceVector + "angle = " + angle);
		}
	}

    public void setActive(bool newActive)
    {
        active = newActive;
        if(active)print(id + " active");
    }
}
