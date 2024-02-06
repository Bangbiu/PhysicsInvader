using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject owner;
    public GameObject explodeFXPrefab;
    public Vector3 thrust;
    public Quaternion heading;
    public float lifeTime;
    private float timeElapse;

    public AudioClip deathKnell;

    private bool disabled = false;

    // Use this for initialization
    void Start() {
        // travel straight in the X-axis
        //thrust.y = 400.0f;
        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
        // set the direction it will travel in
        //GetComponent<Rigidbody>().MoveRotation(heading);
        // apply thrust once, no need to apply it again since
        // it will not decelerate
        GetComponent<Rigidbody>().AddRelativeForce(thrust);

        timeElapse = 0.0f;
    }
    // Update is called once per frame
    void Update()
    { 
        timeElapse += Time.deltaTime;
        if (timeElapse > lifeTime) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (disabled) {
            return;
        } else {
            disabled = true;
        }
        // the Collision contains a lot of info, but it’s the colliding
        // object we’re most interested in.
        Collider collider = collision.collider;
        if (collider.CompareTag("Enemy"))
        {
            EnemyScript enemy = collider.gameObject.GetComponent<EnemyScript>();
            enemy.Die();
            playHitSound();
            Die();
        }
        else if (collider.CompareTag("Player")) {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            player.takeDamage();
            playHitSound();
            Die();
        }
        else if (collider.CompareTag("UFO")) {
            UFOScript ufo = collider.gameObject.GetComponent<UFOScript>();
            ufo.Die();
            playHitSound();
            Die();
        }
        else if (collider.CompareTag("Shield")) {
            ShieldScript shield = collider.gameObject.GetComponent<ShieldScript>();
            shield.takeDamage();
            Die();
        }
        else if (collider.CompareTag("Projectile")) {
            ProjectileScript proj = collider.gameObject.GetComponent<ProjectileScript>();
            proj.Die();
            Die();
        }
        else {
            // if we collided with something else, print to console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }

    public void playHitSound()
    {
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
    }

    public void Die()
    {
        //Destroy(gameObject);
        Vector3 spawnPos = gameObject.transform.position;
        GameObject obj = Instantiate(explodeFXPrefab) as GameObject;
        obj.transform.position = spawnPos;

        //GetComponent<Rigidbody>().useGravity = true;
    }
}
