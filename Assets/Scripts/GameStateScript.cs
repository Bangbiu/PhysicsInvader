using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateScript : MonoBehaviour
{
    GlobalScript globalObj;
    Text stateText;
    // Use this for initialization
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<GlobalScript>();
        stateText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (globalObj.gameState == 0) {
            stateText.text = "";
        } else if (globalObj.gameState == 1) {
            stateText.text = "You Win!";
        } else if (globalObj.gameState == -1) {
            stateText.text = "Game Over";
        }

    }
}
