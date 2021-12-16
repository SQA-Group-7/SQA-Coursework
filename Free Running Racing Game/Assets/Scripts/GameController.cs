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

    // Start is called before the first frame update
    void Start()
    {
        //Start countdown.
        StartCoroutine(PrepareGame());
    }

    // Update is called once per frame
    void Update()
    {

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

        //Change the status, players will gain controls over the charaters
        GameStatus = GameStatus.Started;

        //Hide the text after "GO" is shown for a while
        yield return new WaitForSeconds(2);

        p1Text.enabled = false;
        p2Text.enabled = false;

    }


}
