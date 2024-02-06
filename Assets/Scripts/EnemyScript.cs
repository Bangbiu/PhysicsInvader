using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Color[] typeColor = {Color.red, Color.yellow, Color.green};
    public int type
    {
        get => _type;
        set
        {
            if ((value >= 0) && (value < 3))
            {
                _type = value;
                gameObject.transform.GetChild(_type).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }
    public int life = 1;

    private int _type = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        life = 0;

        GameObject obj = GameObject.Find("GlobalObject");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.score += 1;
        GetComponent<Rigidbody>().mass = 1;
        GetComponent<Rigidbody>().useGravity = true;

        Vector3 thrust = new Vector3(0.0f ,0.0f, -200.0f);
        GetComponent<Rigidbody>().AddRelativeForce(thrust);

        gameObject.transform.GetChild(_type).gameObject
        .transform.GetChild(0).gameObject.GetComponent<Renderer>()
        .material.color = Color.grey;
        //GetComponent<Rigidbody>().detectCollisions  = true;
        //Destroy(gameObject);
    }
}
