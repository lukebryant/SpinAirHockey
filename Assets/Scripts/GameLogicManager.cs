using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameLogicManager : NetworkBehaviour
{ 
    [SerializeField]
    private int numCueBallsPerPlayer;
    [SerializeField]
    private GameObject cueBallPrefab;
    [SerializeField]
    private const int numAnchors = 10;
    [SerializeField]
    private float anchorEdgeFactor = 0.6f; //
    [SerializeField]
    private Text scoreText;
    private int leftScore = 0;
    private int rightScore = 0;
    private static int numPlayers = 2;
    private List<GameObject> anchors = new List<GameObject>();
    private List<GameObject>[] gameCueBalls = new List<GameObject>[numPlayers];  //Array of all the lists of cueBalls in the game where gameCueBalls[0] is a list of player 0's cueBalls
    private List<CueBallLogic>[] gameCueBallLogics = new List<CueBallLogic>[numPlayers];
    private int[] selectedCueBallIds = {0,0};     //Player n's selected cueBall
    private Canvas canvas;


    // Use this for initialization
    void Start () {
        Vector2 spawnPosition;
        if (!isServer) return;
        canvas = GetComponentInParent<Canvas>();
        for (int j = 0; j < numPlayers; j++)
        {
            gameCueBalls[j] = new List<GameObject>();
            gameCueBallLogics[j] = new List<CueBallLogic>();
            Color newColor = j == 0 ? Color.green : Color.yellow;
            for (int i = 0; i < numCueBallsPerPlayer; i++)
            {
                GameObject cueBall = (GameObject)Instantiate((Object)cueBallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                cueBall.transform.SetParent(canvas.transform);
                spawnPosition = new Vector2(0, 0);
                cueBall.transform.localPosition = spawnPosition;
                CueBallLogic cueBallLogic = cueBall.GetComponent<CueBallLogic>();
                cueBallLogic.id = i;
                cueBall.GetComponentInChildren<Image>().color = newColor;
                cueBall.GetComponentInChildren<Text>().text = i.ToString();
                gameCueBalls[j].Add(cueBall);
                gameCueBallLogics[j].Add(cueBallLogic);
                NetworkServer.Spawn(cueBall);
            }
        }
        for (int i = 0; i < numAnchors; i++)
        {
            GameObject anchor = Instantiate(Resources.Load("Prefabs/SpinAnchor")) as GameObject;
            AnchorLogic anchorLogic = anchor.GetComponent<AnchorLogic>();
            anchorLogic.id = i;
            anchor.transform.SetParent(canvas.transform);
            spawnPosition = new Vector2(Random.Range(-500 * anchorEdgeFactor, 500 * anchorEdgeFactor), Random.Range(-250 * anchorEdgeFactor, 250 * anchorEdgeFactor));
            anchor.transform.localPosition = spawnPosition;
            anchors.Add(anchor);
            NetworkServer.Spawn(anchor);
        }
        GameObject goalBall = Instantiate(Resources.Load("Prefabs/Ball")) as GameObject;
        goalBall.transform.SetParent(canvas.transform);
        spawnPosition = new Vector2(200, 0);
        goalBall.transform.localPosition = spawnPosition;
        NetworkServer.Spawn(goalBall);
    }

    public void setCurrentAnchor(int id, int player)
    {
        gameCueBallLogics[player][selectedCueBallIds[player]].setCurrentAnchor(anchors[id].transform);
        gameCueBallLogics[player][selectedCueBallIds[player]].anchored = true;
    }
    // Update is called once per frame
    void Update () {
        /*if (Input.GetKeyDown("0")) setActiveCueBall(0);
        if (Input.GetKeyDown("1")) setActiveCueBall(1);
        if (Input.GetKeyDown("2")) setActiveCueBall(2);
        if (Input.GetKeyDown("3")) setActiveCueBall(3);
        if (Input.GetMouseButtonDown(1)) cueBallLogics[selectedPlayerId].anchored = false;*/
    }

    void setActiveCueBall(int id, int player)
    {
        selectedCueBallIds[player] = id;
    }

    public void incrementScore(bool rightGoal)
    {
        if (rightGoal) leftScore++;
        else rightScore++;
        scoreText.text = leftScore + " - " + rightScore;
    }

    public void Input(InputType inputType, int player)
    {
        switch(inputType){
            case InputType.N0:
                setActiveCueBall(0, player);
                break;
            case InputType.N1:
                setActiveCueBall(1, player);
                break;
            case InputType.N2:
                setActiveCueBall(2, player);
                break;
            case InputType.N3:
                setActiveCueBall(3, player);
                break;
            case InputType.M1:
                gameCueBallLogics[player][selectedCueBallIds[player]].anchored = false;
                break;
        }
    }
}
