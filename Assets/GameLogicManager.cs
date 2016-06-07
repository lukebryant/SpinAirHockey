using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameLogicManager : MonoBehaviour {
    [SerializeField]
    private int numPlayers;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private const int numAnchors = 10;
    [SerializeField]
    private GameObject currentAnchor;
    [SerializeField]
    private float anchorEdgeFactor = 0.6f; //
    private List<GameObject> anchors = new List<GameObject>();
    private List<GameObject> players = new List<GameObject>();
    private List<PlayerLogic> playerLogics = new List<PlayerLogic>();
    private int selectedPlayerId;
    private Canvas canvas;


    // Use this for initialization
    void Start () {
        canvas = GetComponentInParent<Canvas>();
        for (int i = 0; i < numPlayers; i++)
        {
            GameObject player = (GameObject)Instantiate((Object)playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            player.transform.SetParent(canvas.transform);
            Vector2 spawnPosition = new Vector2(0,0);
            player.transform.localPosition = spawnPosition;
            PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();
            playerLogic.id = i;
            players.Add(player);
            playerLogics.Add(playerLogic);
        }
        for (int i = 0; i < numAnchors; i++)
        {
            GameObject anchor = Instantiate(Resources.Load("Prefabs/SpinAnchor")) as GameObject;
            AnchorLogic anchorLogic = anchor.GetComponent<AnchorLogic>();
            anchorLogic.id = i;
            anchor.transform.SetParent(canvas.transform);
            Vector2 spawnPosition = new Vector2(Random.Range(-500 * anchorEdgeFactor, 500 * anchorEdgeFactor), Random.Range(-250 * anchorEdgeFactor, 250 * anchorEdgeFactor));
            anchor.transform.localPosition = spawnPosition;
            anchors.Add(anchor);
        }
    }

    public void setCurrentAnchor(int id)
    {
        playerLogics[selectedPlayerId].setCurrentAnchor(anchors[id].transform);
        playerLogics[selectedPlayerId].anchored = true;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("0")) setActivePlayer(0);
        if (Input.GetKeyDown("1")) setActivePlayer(1);
        if (Input.GetKeyDown("2")) setActivePlayer(2);
        if (Input.GetKeyDown("3")) setActivePlayer(3);
        if (Input.GetMouseButtonDown(1)) playerLogics[selectedPlayerId].anchored = false;
    }

    void setActivePlayer(int id)
    {
        selectedPlayerId = id;
        for (int i = 0; i < players.Count; i++)
        {
            playerLogics[i].setActive(true ? i == id : false);
        }
    }
}
