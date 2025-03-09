using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType
{
    // Enemy's Properties
    public float speed;
    public Vector2 velocity;
    public Vector3 dimensions;
    public Color color;

    // Enemy's reference speed
    static float defaultSpeed = Constants.defaultEnemySpeed;
    static float defaultSize = Constants.defaultEnemySize;

    // Enemy YellowSquare's properties
    public struct YellowSquare
    {
        public static float speed = 2 * defaultSpeed;
        public static Vector2 velocity = new Vector2(-speed, 0);
        public static Vector3 dimensions = new Vector3(defaultSize, defaultSize, 1f);
        public static Color color = new Color(1f, 0.92f, 0.016f);
    } 

    // Enemy RedSquare's properties
    public struct RedSquare
    {
        public static float speed = defaultSpeed;
        public static Vector2 velocity = new Vector2(-speed, 0);
        public static Vector3 dimensions = new Vector3(defaultSize, defaultSize, 1f);
        public static Color color = new Color(1f, 0, 0);
    }

    // Enemy Asteroid's properties
    public struct Asteroid
    {
        public static float speed = defaultSpeed;
        public static Vector2 velocity = new Vector2(-speed, 0);
        public static Vector3 dimensions = new Vector3(defaultSize, defaultSize, 1f);
        public static Color color = new Color(0.35f, 0.22f, 0.15f);
    }

    // Enemy UpperAsteroid's properties
    public struct UpperAsteroid
    {
        public static float speed = defaultSpeed / (float) System.Math.Sqrt(2);
        public static Vector2 velocity = new Vector2(-speed, speed);
        public static Vector3 dimensions = new Vector3(defaultSize / (float) System.Math.Sqrt(2), defaultSize / (float) System.Math.Sqrt(2), 1f);
        public static Color color = new Color(0.35f, 0.22f, 0.15f);
    }

    // Enemy LowerAsteroid's properties
    public struct LowerAsteroid
    {
        public static float speed = defaultSpeed / (float) System.Math.Sqrt(2);
        public static Vector2 velocity = new Vector2(-speed, -speed);
        public static Vector3 dimensions = new Vector3(defaultSize / (float) System.Math.Sqrt(2), defaultSize / (float) System.Math.Sqrt(2), 1f);
        public static Color color = new Color(0.35f, 0.22f, 0.15f);
    }

    // Constructor. 'type' is chosen according to class EnemyGenerator
    public EnemyType(int type)
    {
        if (type == (int) Constants.enemyTypes.YELLOWSQUARE)
        {
            speed = YellowSquare.speed;
            velocity = YellowSquare.velocity;
            dimensions = YellowSquare.dimensions;
            color = YellowSquare.color;
        }
        else if (type == (int) Constants.enemyTypes.REDSQUARE)
        {
            speed = RedSquare.speed;
            velocity = RedSquare.velocity;
            dimensions = RedSquare.dimensions;
            color = RedSquare.color;
        }
        else if (type == (int) Constants.enemyTypes.ASTEROID)
        {
            speed = Asteroid.speed;
            velocity = Asteroid.velocity;
            dimensions = Asteroid.dimensions;
            color = Asteroid.color;
        }
        else if (type == (int) Constants.enemyTypes.UPPERASTEROID)
        {
            speed = UpperAsteroid.speed;
            velocity = UpperAsteroid.velocity;
            dimensions = UpperAsteroid.dimensions;
            color = UpperAsteroid.color;
        }
        else if (type == (int) Constants.enemyTypes.LOWERASTEROID)
        {
            speed = LowerAsteroid.speed;
            velocity = LowerAsteroid.velocity;
            dimensions = LowerAsteroid.dimensions;
            color = LowerAsteroid.color;
        }
    }
}