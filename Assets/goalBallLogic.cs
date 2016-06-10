using UnityEngine;
using System.Collections;

public class goalBallLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GetComponentInParent<Canvas>() == null)      //Spawned objects aren't put on the canvas, so this will happen on clients
        {
            this.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
