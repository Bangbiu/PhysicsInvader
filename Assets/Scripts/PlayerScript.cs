using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int life;
    public GameObject camToShake;
    void Start()
    {

    }

    public GameObject projectileClass;
    // Update is called once per frame
    void Update()
    {
        Vector3 updatedPosition = gameObject.transform.position; 
        updatedPosition.x += .05f * Input.GetAxisRaw("Horizontal"); 
        gameObject.transform.position = updatedPosition;

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 1.5f;
            // instantiate the Bullet
            GameObject obj = Instantiate(projectileClass) as GameObject;
            obj.transform.position = spawnPos;
            ProjectileScript p = obj.GetComponent<ProjectileScript>();
        }
    }

    public void takeDamage() {
        life--;
        //Play Camera Shake
        CameraShakeScript shake = camToShake.GetComponent<CameraShakeScript>();
        shake.shakeDuration = 0.3f;
        if (life <= 0) {
            GameObject g = GameObject.Find("GlobalObject");
            GlobalScript globalObj = g.GetComponent<GlobalScript>();
            globalObj.gameState = -1;
            Destroy(gameObject);
        }
    }
}
