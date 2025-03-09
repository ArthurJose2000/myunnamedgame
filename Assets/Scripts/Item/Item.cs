using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Item's type
    public int type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called when the player or a projectile collide with the enemy
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject player = col.gameObject;

            if(type == (int) Constants.itemTypes.COIN)
                player.GetComponent<Player>().increaseScore(Constants.coinScore);
            else if (type == (int) Constants.itemTypes.POWER)
                player.GetComponent<Player>().weapon.enhanceWeapon();
            else if (type == (int) Constants.itemTypes.SHIELD)
                player.GetComponent<Player>().activateShield();

            // Collection sound
            col.gameObject.GetComponent<Player>().playItemCollectionSound();

            Destroy(gameObject);
        }
    }
}
