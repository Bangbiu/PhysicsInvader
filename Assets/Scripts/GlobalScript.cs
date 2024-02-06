using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject UFOPrefab;
    public GameObject projectilPrefab;

    int[,] spawnTypes = { 
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
    };

    
    float attackTimer;
    float UFOSpawnTimer;
    float movingDirec = -1.0f;
    float movingStep = 0.0f;
    

    int row = 5;
    int col = 11;

    
    public float attackDelta;
    public float UFOSpawnDelta;
    public float moveDelta = 0.05f;
    public float movingBound = 10.0f;

    public Vector3 spawnOrigin;
    public GameObject[,] enemySeq;

    
    public int score;
    public int gameState;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        attackTimer = 0;
        UFOSpawnTimer = 0;
        // Create Enemy Array

        enemySeq = new GameObject[row, col];
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                GameObject enemy = Instantiate(enemyPrefab) as GameObject;
                enemy.GetComponent<EnemyScript>().type = spawnTypes[i, j];
                enemySeq[i, j] = enemy;
                /*
                Material m_Material;
                //enemy.GetComponent<Rigidbody>().detectCollisions = false;
                m_Material = enemy.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
                m_Material.color = typeColor[spawnTypes[i, j]];
                */
            }
        }
        moveEnemies();
          
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        UFOSpawnTimer += Time.deltaTime;

        if (attackTimer > attackDelta) {
            spawnEnemyAttack();
            attackTimer = 0.0f;
        }

        if (UFOSpawnTimer > UFOSpawnDelta) {
            spawnUFO();
            UFOSpawnTimer = 0.0f;
        }

        moveEnemies();

    }

    void moveEnemies() 
    {
        int count = 0;
        spawnOrigin.x += movingDirec * moveDelta;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                int life = enemySeq[i, j].GetComponent<EnemyScript>().life;
                if (life > 0) {
                    Vector3 spawnPos = new Vector3(j*1.5f, i*1.5f, 0.0f);
                    enemySeq[i, j].transform.position = spawnPos + spawnOrigin;
                    enemySeq[i, j].transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                    count++;
                }
            }
        }
        if (count <= 0) {
            gameState = 1;
            return;
        }

        movingStep+= moveDelta;
        if (movingStep > movingBound) {
            movingStep = 0.0f;
            movingDirec *= -1.0f;
        }
    }

    void spawnEnemyAttack()
    {
        if (gameState != 0) return;
        Vector3 spawnPos = new Vector3();
        bool sucess = false;
            
        while (!sucess) {
            int attackingCol = (int) Random.Range(0.0f, 11.0f);
            for (int i = 0; i < row; i++) {
                int life = enemySeq[i, attackingCol].GetComponent<EnemyScript>().life;
                if (life > 0) {
                    spawnPos = enemySeq[i, attackingCol].transform.position;
                    sucess = true;
                    Debug.Log("Attck!" + spawnPos);
                    break;
                }
            }
        }

        spawnPos.y -= 1.5f;
        // instantiate the Bullet
        GameObject obj = Instantiate(projectilPrefab) as GameObject;
        obj.transform.position = spawnPos;
        ProjectileScript p = obj.GetComponent<ProjectileScript>();
        p.thrust.y = -100.0f;


    }

    void spawnUFO()
    {
            Debug.Log("UFO Appears!");
            Vector3 spawnPos = spawnOrigin;

            spawnPos.x -= 5.0f;
            spawnPos.y += 7.5f;
            // instantiate the Bullet
            GameObject obj = Instantiate(UFOPrefab) as GameObject;
            obj.transform.position = spawnPos;
            //UFOScript u = obj.GetComponent<UFOScript>();
            //u.thrust.z = -400.0f;
    }
}
