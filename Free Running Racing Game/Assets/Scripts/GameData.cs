using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{

    private static GameData gameData = null;
    public static GameData get() {
        if (gameData == null) {
            gameData = new GameData();
        }
        return gameData;
    }

    private List<int> victories = new List<int>();
    private List<float> finishTimes = new List<float>();

    public int getPlayerVictoryCount(int playerID) {
        int count = 0;
        foreach (int winner in victories) {
            if (winner == playerID) count++;
        }
        return count;
    }

    public int getNumberOfGamesPlayed() {
        return victories.Count;
    }

    public List<float> getRecentFinishTimes() {
        return finishTimes;
    }

    public void registerVictory(int playerID, float finishTime) {
        victories.Add(playerID);
        finishTimes.Add(finishTime);
    }

    public List<int> getVictories() {
        return victories;
    }
    
}
