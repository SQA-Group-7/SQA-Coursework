using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI recentMatches;

    public void StartGame() {
        SceneManager.LoadScene("EpicMap");
    }

    // Start is called before the first frame update
    void Start()
    {
        player1Score.text = GameData.get().getPlayerVictoryCount(1).ToString();
        player2Score.text = GameData.get().getPlayerVictoryCount(2).ToString();

        int lowerBound = 0;
        int gamesPlayed = GameData.get().getNumberOfGamesPlayed();
        if (gamesPlayed > 5) lowerBound = gamesPlayed - 5;

        string recentMatchesText = "";

        for (int i = gamesPlayed ; i > lowerBound ; i--) {

            string raceCount = (gamesPlayed - i + 1).ToString() + ". ";
            string winner = GameData.get().getVictories()[i-1].ToString() + "P ";

            float time = GameData.get().getRecentFinishTimes()[i-1];
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("00");
            string milliSeconds = (time % 60 % 1 * 1000).ToString("000");

            string finishTime = minutes + ":" + seconds + ":" + milliSeconds + "\n";

            recentMatchesText += raceCount + winner + finishTime;

        }

        recentMatches.text = recentMatchesText;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
