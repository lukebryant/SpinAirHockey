using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    GameLogicManager gameLogicManager;
    private int playerId;
    // Use this for initialization
    void Start() {
        if (isServer)
        {
            gameLogicManager = GameObject.Find("GameManager").GetComponent<GameLogicManager>();
            playerId = 0;
        }
        else playerId = 1;
        if (isLocalPlayer){
            GameObject[] spinAnchorList = GameObject.FindGameObjectsWithTag("SpinAnchor");
            foreach (GameObject spinAnchor in spinAnchorList)
            {
                spinAnchor.GetComponent<AnchorLogic>().givePlayerControllerReference(this);
                print("playerId = " + playerId);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown("0")) CmdSendInput(InputType.N0, playerId);
        if (Input.GetKeyDown("1")) CmdSendInput(InputType.N1, playerId);
        if (Input.GetKeyDown("2")) CmdSendInput(InputType.N2, playerId);
        if (Input.GetKeyDown("3")) CmdSendInput(InputType.N3, playerId);
        if (Input.GetMouseButtonDown(1)) CmdSendInput(InputType.M1, playerId);
    }

    [Command]
    void CmdSendInput(InputType inputType, int sentPlayerId)
    {
        gameLogicManager.Input(inputType, sentPlayerId);
    }

    [Command]
    public void CmdSetCurrentAnchor(int id, int sentPlayerId)
    {
        gameLogicManager.setCurrentAnchor(id, sentPlayerId);
        print("Setting anchor: " + "id" + " = " + id + ", " + "playerId" + "= " + sentPlayerId);
    }

    public int getPlayerId() { return playerId; }
}


public enum InputType
{
    N0,N1,N2,N3,M1
}
