using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    // Type of the weapon (SINGLESHOT, SPLITSHOT, or TRIPLESHOT)
    public int weaponType;

    // Last time when the player used the weapon
    public float lastTriggerActivation = -1f;

    // Sounds of the weapon
    public AudioSource singleShotSound;
    public AudioSource splitShotSound;
    public AudioSource tripleShotSound;

    // Constructor. 'type' is chosen according to class Player
    public Weapon(int type)
    {
        weaponType = type;

        GameObject prefabSingleShotSound = GameObject.Instantiate(Resources.Load("Audio/SingleShotSound", typeof(GameObject))) as GameObject;
        singleShotSound = prefabSingleShotSound.GetComponent<AudioSource>();

        GameObject prefabSplitShotSound = GameObject.Instantiate(Resources.Load("Audio/SplitShotSound", typeof(GameObject))) as GameObject;
        splitShotSound = prefabSplitShotSound.GetComponent<AudioSource>();

        GameObject prefabTripleShotSound = GameObject.Instantiate(Resources.Load("Audio/TripleShotSound", typeof(GameObject))) as GameObject;
        tripleShotSound = prefabTripleShotSound.GetComponent<AudioSource>();
    }

    // Trigger the fire
    public void activation(Vector3 playerPosition)
    {
        if (lastTriggerActivation + Constants.defaultProjectileRate <= Time.time)
            triggerProjectiles(playerPosition);
    }

    // Enhance the weapon by changing the type
    public void enhanceWeapon()
    {
        if (weaponType == (int) Constants.weaponTypes.SINGLESHOT)
            weaponType = (int) Constants.weaponTypes.SPLITSHOT;
        else if (weaponType == (int) Constants.weaponTypes.SPLITSHOT)
            weaponType = (int) Constants.weaponTypes.TRIPLESHOT;
        else
            return;
    }

    // Instatiate and trigger projectiles according to the weapon's type
    public void triggerProjectiles(Vector3 playerPosition)
    {

        // Gap between the center of the player and the center of the projectile
        float playerDiameter = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float gap = playerDiameter;

        if (weaponType == (int) Constants.weaponTypes.SINGLESHOT)
        {
            GameObject projectile = GameObject.Instantiate(Resources.Load("Prefabs/Projectile", typeof(GameObject))) as GameObject;
            projectile.transform.position = new Vector3(playerPosition.x + gap, playerPosition.y, playerPosition.z);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = Constants.defaultProjectileVelocity;

            singleShotSound.Play();
        }
        else if (weaponType == (int) Constants.weaponTypes.SPLITSHOT)
        {
            GameObject upperProjectile = GameObject.Instantiate(Resources.Load("Prefabs/Projectile", typeof(GameObject))) as GameObject;
            upperProjectile.transform.position = new Vector3(playerPosition.x + gap, playerPosition.y + gap / 2, playerPosition.z);
            upperProjectile.transform.eulerAngles = new Vector3(0f, 0f, -60f);
            upperProjectile.GetComponent<Rigidbody2D>().linearVelocity = Constants.upperProjectileVelocity;

            GameObject lowerProjectile = GameObject.Instantiate(Resources.Load("Prefabs/Projectile", typeof(GameObject))) as GameObject;
            lowerProjectile.transform.position = new Vector3(playerPosition.x + gap, playerPosition.y - gap / 2, playerPosition.z);
            lowerProjectile.transform.eulerAngles = new Vector3(0f, 0f, 60f);
            lowerProjectile.GetComponent<Rigidbody2D>().linearVelocity = Constants.lowerProjectileVelocity;

            splitShotSound.Play();
        }
        else if (weaponType == (int) Constants.weaponTypes.TRIPLESHOT)
        {
            GameObject upperProjectile = GameObject.Instantiate(Resources.Load("Prefabs/Projectile", typeof(GameObject))) as GameObject;
            upperProjectile.transform.position = new Vector3(playerPosition.x + gap, playerPosition.y + gap / 2, playerPosition.z);
            upperProjectile.transform.eulerAngles = new Vector3(0f, 0f, -60f);
            upperProjectile.GetComponent<Rigidbody2D>().linearVelocity = Constants.upperProjectileVelocity;

            GameObject middleProjectile = GameObject.Instantiate(Resources.Load("Prefabs/Projectile", typeof(GameObject))) as GameObject;
            middleProjectile.transform.position = new Vector3(playerPosition.x + gap, playerPosition.y, playerPosition.z);
            middleProjectile.GetComponent<Rigidbody2D>().linearVelocity = Constants.defaultProjectileVelocity;

            GameObject lowerProjectile = GameObject.Instantiate(Resources.Load("Prefabs/Projectile", typeof(GameObject))) as GameObject;
            lowerProjectile.transform.position = new Vector3(playerPosition.x + gap, playerPosition.y - gap / 2, playerPosition.z);
            lowerProjectile.transform.eulerAngles = new Vector3(0f, 0f, 60f);
            lowerProjectile.GetComponent<Rigidbody2D>().linearVelocity = Constants.lowerProjectileVelocity;

            tripleShotSound.Play();
        }

        lastTriggerActivation = Time.time;
    }
}