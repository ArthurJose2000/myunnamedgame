using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Prefab enemy
    public GameObject prefabEnemy;

    // The time at which the next enemy will spawn. Initially 0 seconds
    public float nextSpawn = 0f;

    // Auxiliary variable to decrease the maximum time to spawn an enemy
    // It increases the game difficulty
    public float deltaDifficulty = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Handle game difficulty
        increaseDifficultyOverTime();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy();
    }

    // Spawn enemy
    public void spawnEnemy()
    {
        if (Time.time > nextSpawn)
        {
            // Instantiate a prefab enemy
            // Spawn a random enemy between YellowSquare, RedSquare, and Asteroid
            int type = getEnemyType();
            GameObject enemy = Instantiate(prefabEnemy);
            enemy.GetComponent<Enemy>().type = type;
            EnemyType enemyType = new EnemyType(type);

            // Set the enemy position
            enemy.transform.position = getSpawnLocation();

            // Assign the properties to the enemy
            enemy.GetComponent<SpriteRenderer>().color = enemyType.color;
            enemy.GetComponent<Rigidbody2D>().linearVelocity = enemyType.velocity;
            enemy.transform.localScale = enemyType.dimensions;

            // Spawn the next enemy at 'nextSpawn' seconds
            // Random.range returns a float in this case
            float minSpawnRate = Constants.minSpawnRate;
            float maxSpawnRate = Constants.maxSpawnRate - deltaDifficulty;
            nextSpawn = Time.time + Random.Range(minSpawnRate, maxSpawnRate); 
        }
 
    }

    // Get a random enemy type
    public int getEnemyType()
    {
        int[] typesOfEnemies = {(int) Constants.enemyTypes.YELLOWSQUARE, (int) Constants.enemyTypes.REDSQUARE, (int) Constants.enemyTypes.ASTEROID};

        // Random.range returns a int in this case
        int randomIndex = Random.Range(0, typesOfEnemies.Length);
        int randomType = typesOfEnemies[randomIndex];

        return randomType;
    }

    // Get the spawn location of a new enemy
    public Vector3 getSpawnLocation()
    {
        // Get the dimensions of the screen windows in pixles
        int width = Screen.width;
        int height = Screen.height;

        // Outer x edge
        // Enemy spawns outside the screen
        float xEdge = 0.1f * width;

        // Border y gap
        float yGap = 0.1f * height;

        // x coordinate to generate the new enemy
        float x = width + xEdge;

        // y coordinate to generate the new enemy
        float y = Random.Range(yGap, height - yGap);

        // Spawn location of the new enemy
        Vector3 spawnLocation = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
        spawnLocation.z = 0f;
        
        return spawnLocation;
    }

    // Split asteroid into 2 small ones
    public void splitAsteroid(Vector3 asteroidPosition)
    {
        // Instantiate a prefab enemy
        // Spawn a UpperAsteroid and a LowerAsteroid
        int type;

        // Spawn the UpperAsteroid
        type = (int) Constants.enemyTypes.UPPERASTEROID;
        GameObject upperAsteroid = Instantiate(prefabEnemy);
        upperAsteroid.GetComponent<Enemy>().type = type;
        EnemyType upperAsteroidType = new EnemyType(type);


        float upperGap = Constants.defaultEnemySize / 2;
        upperAsteroid.transform.position = new Vector3(asteroidPosition.x - upperGap, asteroidPosition.y + upperGap, asteroidPosition.z);

        upperAsteroid.GetComponent<SpriteRenderer>().color = upperAsteroidType.color;
        upperAsteroid.GetComponent<Rigidbody2D>().linearVelocity = upperAsteroidType.velocity;
        upperAsteroid.transform.localScale = upperAsteroidType.dimensions;

        // Spawn the LowerAsteroid
        type = (int) Constants.enemyTypes.LOWERASTEROID;
        GameObject lowerAsteroid = Instantiate(prefabEnemy);
        lowerAsteroid.GetComponent<Enemy>().type = type;
        EnemyType lowerAsteroidType = new EnemyType(type);

        float lowerGap = Constants.defaultEnemySize / 2;
        lowerAsteroid.transform.position = new Vector3(asteroidPosition.x - lowerGap, asteroidPosition.y - lowerGap, asteroidPosition.z);
        
        lowerAsteroid.GetComponent<SpriteRenderer>().color = lowerAsteroidType.color;
        lowerAsteroid.GetComponent<Rigidbody2D>().linearVelocity = lowerAsteroidType.velocity;
        lowerAsteroid.transform.localScale = lowerAsteroidType.dimensions;
    }

    // Increase the difficulty of the game over time
    public void increaseDifficultyOverTime()
    {
        // Increase difficulty after 10 seconds
        Invoke("changeSpawnRates", 10);
    }

    // Change spawn rates
    public void changeSpawnRates()
    {
        // Decrease the maximum time to spawn an enemy in 'delta' seconds
        float delta = 0.3f;

        // 'deltaDifficulty' is used in function spawnEnemy()
        deltaDifficulty += delta;

        // New maximum time to spawn an enemy
        float maxSpawnRate = Constants.maxSpawnRate - deltaDifficulty;

        // Continue to decrease the difficulty every 10 seconds
        if (maxSpawnRate - Constants.minSpawnRate > delta)
        {
            Invoke("changeSpawnRates", 10);
        }
    }
}
