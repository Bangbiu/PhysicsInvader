using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{   
    public int life;
    public AudioClip blockAudio;

    Color[] statusColor = {Color.red, Color.yellow, Color.green, Color.cyan};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage() {
        life--;
        AudioSource.PlayClipAtPoint(blockAudio, gameObject.transform.position);
        if (life < 0) {
            Destroy(gameObject);
            return;
        }
        Material m_Material = gameObject.GetComponent<Renderer>().material;
        m_Material.color = statusColor[life];
        //m_Material = gameObject.GetComponent<Mesh>().GetComponent<Renderer>().material;
        //m_Material.color = statusColor[life];
    }
}
