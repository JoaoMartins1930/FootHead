using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_PE : MonoBehaviour {

    private GameController gameController;
    private Player player;
    private Ball ball;
    private bool goal = false;
    [SerializeField]
    private int scoreValue;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
        if (player == null)
        {
            Debug.Log("Cannot find 'Player' script");
        }

        GameObject ballObject = GameObject.FindWithTag("Ball");
        if (ballObject != null)
        {
            ball = ballObject.GetComponent<Ball>();
        }
        if (ball == null)
        {
            Debug.Log("Cannot find 'Player' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            //gameController.AddScorePE(scoreValue);
            goal = true;
            player.InitialPos(goal);
            ball.InitialPos(goal);
            print("GOLO DO AVES");
        }
        else
        {
            goal = false;
        }
    }
}
