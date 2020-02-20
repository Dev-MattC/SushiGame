using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoardScript : MonoBehaviour {

    public Text p1Score;
    public Text p2Score;
    public Text p3Score;
    public Text p4Score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    GameObject gameData = GameObject.Find ("GameData");
        if (gameData != null)
        {
            GameDataScript gameDataScript = gameData.GetComponent<GameDataScript>();
            p1Score.text = gameDataScript.player1Score.ToString();
            p2Score.text = gameDataScript.player2Score.ToString();
            p3Score.text = gameDataScript.player3Score.ToString();
            p4Score.text = gameDataScript.player4Score.ToString();
        }
	}
}
