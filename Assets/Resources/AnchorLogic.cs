using UnityEngine;
using System.Collections;

public class AnchorLogic : MonoBehaviour {
	public int id;
	public CueBallLogic cueBallLogic;
    private GameLogicManager gameLogicManager;

    // Use this for initialization
	void Start () {
        gameLogicManager = GameObject.Find("GameManager").GetComponent<GameLogicManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {
		gameLogicManager.setCurrentAnchor (id);
	}
}
