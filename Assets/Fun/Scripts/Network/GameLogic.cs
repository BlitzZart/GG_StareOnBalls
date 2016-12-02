using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameLogic : NetworkBehaviour {
    private static GameLogic instance;
    public static GameLogic Instance
    {
        get { return instance; }
    }
    [SyncVar]
    public int score1, score2;
    public Text score1Text, score2Text, gameOverText;

    #region unity callbacks
    void Awake() {
        instance = this;
    }

    void Start() {
        CheckPointCollider.EventMadePoint += OnMadePoint;
        //BallServer.EventGameStarted += OnGameStarted;
    }

    void OnDestroy() {
        CheckPointCollider.EventMadePoint -= OnMadePoint;
        //BallServer.EventGameStarted -= OnGameStarted;
    }
    #endregion

    #region private

    private void CheckVictory() {
        if (score1 >= 3 || score2 >= 3) {
            gameOverText.enabled = true;

            if (score1 > score2)
                gameOverText.text = "Player 1 Wins";
            else
                gameOverText.text = "Player 2 Wins";
        }
    }
    #endregion

    #region public
    public void UpdateUI() {
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
        CheckVictory();
    }

    public void OnMadePoint(int playerNumber) {
        if (!isServer)
            return;
        if (playerNumber == 1) {
            score1++;
        }
        else if (playerNumber == 2) {
            score2++;
        }
        UpdateUI();
        // tell client
        Communicator.Player.RpcUpdateScore(score1, score2);
    }
    #endregion
}