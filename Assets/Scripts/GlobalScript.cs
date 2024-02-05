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

    Color[] typeColor = {Color.red, Color.yellow, Color.green};
    float attackTimer;
    float UFOSpawnTimer;
    float moveTimer;
    float movingDirec = -1.0f;
    int movingStep = 0;
    

    int row = 5;
    int col = 11;

    
    public float attackDelta;
    public float UFOSpawnDelta;
    public float moveDelta;
    public int movingBound = 3;

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

        Material m_Material;

        enemySeq = new GameObject[row, col];
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                Vector3 spawnPos = new Vector3(j*1.5f, 0.0f, i*1.5f);
                GameObject enemy = Instantiate(enemyPrefab) as GameObject;
                enemy.transform.position = spawnPos + spawnOrigin;
                enemy.GetComponent<EnemyScript>().type = spawnTypes[i, j];
                m_Material = enemy.GetComponent<Renderer>().material;
                m_Material.color = typeColor[spawnTypes[i, j]];
                enemySeq[i, j] = enemy;
                //ProjectileScript p = obj.GetComponent<ProjectileScript>();
            }
        }
          
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        UFOSpawnTimer += Time.deltaTime;
        moveTimer += Time.deltaTime;

        if (attackTimer > attackDelta) {
            spawnEnemyAttack();
            attackTimer = 0.0f;
        }

        if (UFOSpawnTimer > UFOSpawnDelta) {
            spawnUFO();
            UFOSpawnTimer = 0.0f;
        }

        if (moveTimer > moveDelta) {
            moveEnemies();
            moveTimer = 0.0f;
        }
    }

    void moveEnemies() 
    {
        int count = 0;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                if (enemySeq[i, j] != null) {
                    Vector3 newPos = enemySeq[i, j].transform.position;
                    newPos.x += movingDirec;
                    enemySeq[i, j].transform.position = newPos;
                    count++;
                }
            }
        }
        if (count <= 0) {
            gameState = 1;
            return;
        }

        movingStep++;
        if (movingStep > movingBound) {
            movingStep = 0;
            movingDirec *= -1.0f;
        }
    }

    void spawnEnemyAttack()
    {
            Vector3 spawnPos = new Vector3();
            int attackingCol = (int) Random.Range(0.0f, 11.0f);
            for (int i = 0; i < row; i++) {
                if (enemySeq[i, attackingCol] != null) {
                    spawnPos = enemySeq[i, attackingCol].transform.position;
                    break;
                }
            }
            spawnPos.z -= 1.5f;
            // instantiate the Bullet
            GameObject obj = Instantiate(projectilPrefab) as GameObject;
            obj.transform.position = spawnPos;
            ProjectileScript p = obj.GetComponent<ProjectileScript>();
            p.thrust.y = -400.0f;


    }

    void spawnUFO()
    {
            Debug.Log("UFO Appears!");
            Vector3 spawnPos = spawnOrigin;

            spawnPos.x -= 5.0f;
            spawnPos.z += 7.5f;
            // instantiate the Bullet
            GameObject obj = Instantiate(UFOPrefab) as GameObject;
            obj.transform.position = spawnPos;
            //UFOScript u = obj.GetComponent<UFOScript>();
            //u.thrust.z = -400.0f;
    }
}
