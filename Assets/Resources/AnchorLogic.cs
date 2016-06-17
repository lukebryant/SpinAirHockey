using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;


public class AnchorLogic : NetworkBehaviour {
    [SyncVar]
	public int id;
	public CueBallLogic cueBallLogic;
    private GameLogicManager gameLogicManager;
    private PlayerController playerController;

    // Use this for initialization
    void Start() {
        if(GetComponentInParent<Canvas>() == null)      //Spawned objects aren't put on the canvas, so this will happen on clients
        {
            this.transform.SetParent(GameObject.Find("Canvas").transform);
        }
        if (!isServer) print("id = " + id);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {
		playerController.CmdSetCurrentAnchor(id, playerController.getPlayerId());
	}

    public void givePlayerControllerReference(PlayerController newPlayerController)
    {
        playerController = newPlayerController;
    }
}
