using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUIScript : MonoBehaviour
{
    PlayerScript playerObj;
    Text lifeText;
    // Use this for initialization
    void Start()
    {
        GameObject p = GameObject.Find("PlayerObject");
        playerObj = p.GetComponent<PlayerScript>();
        lifeText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        lifeText.text = "Life: " + playerObj.life.ToString();
    }
}
