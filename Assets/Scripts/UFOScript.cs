using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOScript : MonoBehaviour
{   
    public Vector3 thrust;
    public float lifeTime;
    private float timeElapse;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapse += Time.deltaTime;
        if (timeElapse > lifeTime) {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.score += 10;
        Destroy(gameObject);
    }
}
