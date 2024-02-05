using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        GameObject obj = GameObject.Find("GlobalObject");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.score += 1;
        Destroy(gameObject);
    }
}
