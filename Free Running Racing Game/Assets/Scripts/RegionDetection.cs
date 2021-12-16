using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegionDetection : MonoBehaviour
{

    //To store the current checkpoint of the player
    //0 means no checkpoints visited yet.
    //If the player visited checkpoint 1, he will respawn on respawnpoint 1.
    string _currentCheckpoint = "0";

    //To distinguish between player 1 or player 2.
    string _playerID = "1";

    //Start is called before the first frame update
    void Start()
    {
        //Check whether this player is p1 or p2.
        if (gameObject.name.EndsWith("2"))
        {
            _playerID = "2";
        }

        //TEMP
        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Temp magic value
        if (transform.position.y < 20)
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        //Teleports the player to the respawnpoint object.
        transform.position = GameObject.Find("MapData/RespawnPoints/" + _currentCheckpoint + "/p" + _playerID).transform.position;
    }

    //When the player collides with another object.
    void OnTriggerEnter(Collider other)
    {
        //If the player touches the goal region.
        if (other.gameObject.tag == "Finish")
        {
            //Display win/lose text
            TextMeshProUGUI p1Text = GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI p2Text = GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>();
            if (gameObject.name == "Player 1")
            {
                p1Text.text = "You WIN";
                p2Text.text = "You LOSE";
            }
            else if (gameObject.name == "Player 2")
            {
                p1Text.text = "You LOSE";
                p2Text.text = "You WIN";
            }
            p1Text.enabled = true;
            p2Text.enabled = true;

            //Hide the text after 5 seconds.
            Invoke("HideResultText", 5f);
        }
        //If the player touches the checkpoint region.
        else if (other.gameObject.tag == "Checkpoint")
        {
            //The object name of the checkpoint object is the checkpoint id of the player.
            _currentCheckpoint = other.gameObject.name;
        }
    }

    void HideResultText()
    {
        GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
