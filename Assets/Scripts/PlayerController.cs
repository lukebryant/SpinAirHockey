using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    GameLogicManager gameLogicManager;
    // Use this for initialization
    void Start() {
        if(isServer) gameLogicManager = GameObject.Find("GameManager").GetComponent<GameLogicManager>();
        GameObject[] spinAnchorList = GameObject.FindGameObjectsWithTag("SpinAnchor");
        foreach(GameObject spinAnchor in spinAnchorList)
        {
            spinAnchor.GetComponent<AnchorLogic>().givePlayerControllerReference(this);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown("0")) CmdSendInput(InputType.N0);
        if (Input.GetKeyDown("1")) CmdSendInput(InputType.N1);
        if (Input.GetKeyDown("2")) CmdSendInput(InputType.N2);
        if (Input.GetKeyDown("3")) CmdSendInput(InputType.N3);
        if (Input.GetMouseButtonDown(1)) CmdSendInput(InputType.M1);
    }

    [Command]
    void CmdSendInput(InputType inputType)
    {
        gameLogicManager.Input(inputType);
    }

    [Command]
    public void CmdSetCurrentAnchor(int id)
    {
        gameLogicManager.setCurrentAnchor(id);
    }
}


public enum InputType
{
    N0,N1,N2,N3,M1
}
