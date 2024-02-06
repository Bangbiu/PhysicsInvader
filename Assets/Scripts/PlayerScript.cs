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

    public Vector3 moveVector;
    void FixedUpdate() {
        //GetComponent<Rigidbody>().AddRelativeForce(Input.GetAxisRaw("Horizontal") * moveVector);
    }

    public GameObject projectileClass;
    public AudioClip projLanuchSound;
    // Update is called once per frame
    void Update()
    {
        
        Vector3 updatedPosition = gameObject.transform.position; 
        updatedPosition.y = 0.0f;
        updatedPosition.z = 0.0f;
        updatedPosition.x += .01f * Input.GetAxisRaw("Horizontal"); 
        gameObject.transform.position = updatedPosition;
        

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.y += 1.5f;
            // instantiate the Bullet
            GameObject obj = Instantiate(projectileClass) as GameObject;
            obj.transform.position = spawnPos;
            ProjectileScript p = obj.GetComponent<ProjectileScript>();

            AudioSource.PlayClipAtPoint(projLanuchSound, gameObject.transform.position);
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
