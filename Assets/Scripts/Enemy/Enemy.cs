using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Enemy's Rigidbody
    public Rigidbody2D enemyRb;

    // Enemy's type
    public int type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy object if it is not visible
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Called when a projectile collide with the enemy
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Projectile")
            handleCollisionWithProjectile(col.gameObject);

        // Collision with player is handled in class Player
    }

    // Handle the collision of the enemy with a projectile
    public void handleCollisionWithProjectile(GameObject projectile)
    {
        if (type == (int) Constants.enemyTypes.ASTEROID)
        {
            // Split asteroid
            GameObject.FindWithTag("EnemyGenerator").GetComponent<EnemyGenerator>().splitAsteroid(transform.position);

            Destroy(gameObject);
            Destroy(projectile);
        }
        else
        {
            // Player earns points
            GameObject.FindWithTag("Player").GetComponent<Player>().increaseScore(Constants.hitScore);

            // There is a 20% chance for a item to drop when the enemy gets destroyed
            dropItem(transform.position);

            Destroy(gameObject);
            Destroy(projectile);
        }

        // Destruction sound
        GameObject.FindWithTag("Player").GetComponent<Player>().playEnemyDestructionSound();
    }

    // Drop item if the enemy gets destroyed
    public void dropItem(Vector3 enemyPosition)
    {
        // Since Random.Range generates uniformly distruted results, if it generates 0
        // between the possibilities {0, 1, 2, 3, 4}, the following condition has a
        // 20% chance to be true
        if (0 == Random.Range((int) 0, (int) 5))
        {
            // Get a random item type
            int itemType = getItemType();

            if(itemType == (int) Constants.itemTypes.COIN)
            {
                GameObject coin = GameObject.Instantiate(Resources.Load("Prefabs/Items/Coin/Coin", typeof(GameObject))) as GameObject;
                coin.transform.position = enemyPosition;
                coin.GetComponent<Item>().type = itemType;
            }
            else if (itemType == (int) Constants.itemTypes.POWER)
            {
                GameObject power = GameObject.Instantiate(Resources.Load("Prefabs/Items/Power/Power", typeof(GameObject))) as GameObject;
                power.transform.position = enemyPosition;
                power.GetComponent<Item>().type = itemType;
            }
            else if (itemType == (int) Constants.itemTypes.SHIELD)
            {
                GameObject shield = GameObject.Instantiate(Resources.Load("Prefabs/Items/Shield/Shield", typeof(GameObject))) as GameObject;
                shield.transform.position = enemyPosition;
                shield.GetComponent<Item>().type = itemType;
            }
        }

    }

    // Get a random item type
    public int getItemType()
    {
        int[] typesOfItems = {(int) Constants.itemTypes.COIN, (int) Constants.itemTypes.POWER, (int) Constants.itemTypes.SHIELD};

        // Random.range returns a int in this case
        int randomIndex = Random.Range(0, typesOfItems.Length);
        int randomType = typesOfItems[randomIndex];

        return randomType;
    }
}
