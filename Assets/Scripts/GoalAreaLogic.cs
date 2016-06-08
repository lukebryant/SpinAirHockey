using UnityEngine;
using System.Collections;

public class GoalAreaLogic : MonoBehaviour {
    [SerializeField]
    private bool rightGoal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print("GOOAALLALLALDLA");
    }
}
