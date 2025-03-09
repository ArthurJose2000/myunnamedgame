using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // Default speed of the player
    public static float defaultPlayerSpeed = 9f;

    // Shield expiration time in seconds
    public static float shieldLifetime = 5f;

    // Points earned by destroying a enemy
    public static int hitScore = 100;

    // Points earned by collecting a coin
    public static int coinScore = 50;

    // Projectile rate in seconds/projectile
    public static float defaultProjectileRate = 0.25f;

    // Projectile velocity according to the type
    public static float defaultProjectileSpeed = 12f;
    public static Vector2 upperProjectileVelocity = new Vector2((float) System.Math.Sqrt(3) / 2f * defaultProjectileSpeed, 1f / 2 * defaultProjectileSpeed);
    public static Vector2 defaultProjectileVelocity = new Vector2(defaultProjectileSpeed, 0f);
    public static Vector2 lowerProjectileVelocity = new Vector2((float) System.Math.Sqrt(3) / 2f * defaultProjectileSpeed, - 1f / 2 * defaultProjectileSpeed);

    // Type of weapons
    public enum weaponTypes
    {
        SINGLESHOT,  // 0
        SPLITSHOT,   // 1
        TRIPLESHOT   // 2
    }

    // Type of items
    public enum itemTypes
    {
        COIN,    // 0
        POWER,   // 1
        SHIELD   // 2
    }

    // Type of enemies
    public enum enemyTypes
    {
        YELLOWSQUARE,   // 0
        REDSQUARE,      // 1
        ASTEROID,       // 2
        UPPERASTEROID,  // 3
        LOWERASTEROID   // 4
    }

    // Default speed of an enemy
    public static float defaultEnemySpeed = 4f; 

    // Default size of an enemy
    public static float defaultEnemySize = 1f;
    
    // Maximum time in seconds to an enemy spawns
    public static float maxSpawnRate = 5f;

    // Minimum time in seconds to an enemy spawns
    public static float minSpawnRate = 0.2f;
}
