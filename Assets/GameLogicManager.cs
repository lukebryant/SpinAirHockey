using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameLogicManager : MonoBehaviour {
    [SerializeField]
    private int numPlayers;
    [SerializeField]
    private GameObject cueBallPrefab;
    [SerializeField]
    private const int numAnchors = 10;
    [SerializeField]
    private float anchorEdgeFactor = 0.6f; //
    private List<GameObject> anchors = new List<GameObject>();
    private List<GameObject> cueBalls = new List<GameObject>();
    private List<CueBallLogic> cueBallLogics = new List<CueBallLogic>();
    private int selectedPlayerId;
    private Canvas canvas;


    // Use this for initialization
    void Start () {
        canvas = GetComponentInParent<Canvas>();
        for (int i = 0; i < numPlayers; i++)
        {
            GameObject cueBall = (GameObject)Instantiate((Object)cueBallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cueBall.transform.SetParent(canvas.transform);
            Vector2 spawnPosition = new Vector2(0,0);
            cueBall.transform.localPosition = spawnPosition;
            CueBallLogic cueBallLogic = cueBall.GetComponent<CueBallLogic>();
            cueBallLogic.id = i;
            cueBalls.Add(cueBall);
            cueBallLogics.Add(cueBallLogic);
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
        cueBallLogics[selectedPlayerId].setCurrentAnchor(anchors[id].transform);
        cueBallLogics[selectedPlayerId].anchored = true;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("0")) setActivePlayer(0);
        if (Input.GetKeyDown("1")) setActivePlayer(1);
        if (Input.GetKeyDown("2")) setActivePlayer(2);
        if (Input.GetKeyDown("3")) setActivePlayer(3);
        if (Input.GetMouseButtonDown(1)) cueBallLogics[selectedPlayerId].anchored = false;
    }

    void setActivePlayer(int id)
    {
        selectedPlayerId = id;
        for (int i = 0; i < cueBalls.Count; i++)
        {
            cueBallLogics[i].setActive(true ? i == id : false);
        }
    }
}
