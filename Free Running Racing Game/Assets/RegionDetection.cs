using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegionDetection : MonoBehaviour
{
    // Start is called before the first frame update

    string currentCheckpoint = "0";
    string playerID = "1";
    
    void Start()
    {
        
        if (gameObject.name.EndsWith("2")) {
            playerID = "2";
        }

        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Temp magic value
        if (transform.position.y < 20) {
            RespawnPlayer();
        }
    }

    void RespawnPlayer() {
        //string path = "MapData/RespawnPoints/" + currentCheckpoint + "/p" + playerID;
        //Debug.Log(path);
        transform.position = GameObject.Find("MapData/RespawnPoints/" + currentCheckpoint + "/p" + playerID).transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        TextMeshProUGUI p1Text = GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI p2Text = GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>();
        if (other.gameObject.tag == "Finish")
        {
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
            Invoke("HideResultText", 5f);
        } else if (other.gameObject.tag == "Checkpoint") {
            currentCheckpoint = other.gameObject.name;
        }
    }

    void HideResultText()
    {
        GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
