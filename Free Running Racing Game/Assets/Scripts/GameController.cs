using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public GameStatus gameStatus { get; set; } = GameStatus.PREPARING;

    public enum GameStatus {
        STARTED, PREPARING
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrepareGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrepareGame() {

        TextMeshProUGUI p1Text = GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI p2Text = GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>();

        int count = 3;
        while (count > 0) {
            p1Text.text = count.ToString();
            p2Text.text = count.ToString();

            p1Text.enabled = true;
            p2Text.enabled = true;

            yield return new WaitForSeconds(1);
            count--;
        }

        p1Text.text = "GO!";
        p2Text.text = "GO!";

        gameStatus = GameStatus.STARTED;
        
        yield return new WaitForSeconds(2);

        p1Text.enabled = false;
        p2Text.enabled = false;

    }


}
