using UnityEngine;
using System.Collections;

public class GoalAreaLogic : MonoBehaviour {
    [SerializeField]
    private bool rightGoal;
    [SerializeField]
    private GameLogicManager gameLogicManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoalBall"))
        {
            print("goal");
            gameLogicManager.incrementScore(rightGoal);
        }
    }
}
