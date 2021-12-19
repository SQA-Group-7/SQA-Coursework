using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Enums for the current status of the gmae.
//Started: Countdown has finished and the players are controlling the charaters.
//Preparing: Counting down.
public enum GameStatus
{
    Started, Preparing
}
public class GameController : MonoBehaviour
{
    public GameStatus GameStatus { get; set; } = GameStatus.Preparing;

    //For storing the componenet/object references
    private TextMeshProUGUI _p1Rank;
    private TextMeshProUGUI _p2Rank;
    private GameObject _p1Object;
    private GameObject _p2Object;
    private GameObject _finishRegionObject;


    // Start is called before the first frame update
    void Start()
    {
        //Getting the text component of the rank text.
        GameObject p1RankObject = GameObject.Find("Player1Rank");
        GameObject p2RankObject = GameObject.Find("Player2Rank");

        if (p1RankObject == null || p2RankObject == null)
        {
            Debug.LogError("Rank text object is missing!");
        }
        else
        {
            _p1Rank = p1RankObject.GetComponent<TextMeshProUGUI>();
            _p2Rank = p2RankObject.GetComponent<TextMeshProUGUI>();
        }

        _p1Object = GameObject.Find("Player 1");
        _p2Object = GameObject.Find("Player 2");
        _finishRegionObject = GameObject.Find("MapData/FinishRegion");

        //Start countdown.
        StartCoroutine(PrepareGame());
    }

    // Update is called once per frame
    void Update()
    {
        //A good-enough estimation of who's closer to the goal.
        //This estimation is good enough because the map is mostly in a straight line.
        //A more precise estimation that could be implemented is
        //to check the distance to the next checkpoint instead.
        string p1RankText = "1st";
        string p2RankText = "2nd";
        float p1Distance = (_p1Object.transform.position - _finishRegionObject.transform.position).sqrMagnitude;
        float p2Distance = (_p2Object.transform.position - _finishRegionObject.transform.position).sqrMagnitude;
        if (p2Distance < p1Distance) {
            p2RankText = "1st";
            p1RankText = "2nd";
        }
        _p1Rank.text = p1RankText;
        _p2Rank.text = p2RankText;
    }

    public void FinishGame(int winningPlayer)
    {

        GameObject timer = GameObject.Find("Canvas/Timer");
        float finishTime = 0;
        if (timer != null)
        {
            timer.GetComponent<Timer>().PauseTimer();
            finishTime = timer.GetComponent<Timer>().GetTime();
            timer.GetComponent<Timer>().PauseTimer();
        }
        else
        {
            Debug.LogError("Timer object is missing!");
        }
    }

    //Counting down
    IEnumerator PrepareGame()
    {

        TextMeshProUGUI p1Text = GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI p2Text = GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>();

        //3-2-1
        int count = 3;
        while (count > 0)
        {
            p1Text.text = count.ToString();
            p2Text.text = count.ToString();

            p1Text.enabled = true;
            p2Text.enabled = true;

            yield return new WaitForSeconds(1);
            count--;
        }

        p1Text.text = "GO!";
        p2Text.text = "GO!";

        //Start the timer
        GameObject timer = GameObject.Find("Canvas/Timer");
        if (timer != null)
        {
            timer.GetComponent<Timer>().StartTimer();
        }
        else
        {
            Debug.LogError("Timer object is missing!");
        }

        //Change the status, players will gain controls over the charaters
        GameStatus = GameStatus.Started;

        //Hide the text after "GO" is shown for a while
        yield return new WaitForSeconds(2);

        p1Text.enabled = false;
        p2Text.enabled = false;

    }


}
