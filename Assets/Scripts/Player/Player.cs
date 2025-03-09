using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Player's Rigidbody
    public Rigidbody2D playerRb;

    // Screen limits in absolute values
    public float xLimit;
    public float yLimit;

    // Player's weapon
    public Weapon weapon;

    // Boolean to verify if the player is invincible
    public bool invincible;

    // Shield animation to show invincibility
    public GameObject prefabFakeShield;
    public GameObject fakeShield;

    // Player's score
    public int score;
    public TextMeshProUGUI scoreUI;

    // Game over panel
    public GameObject gameOverPanel;
    public bool gameOver;

    // Sounds of game over, item collection, and enemy destruction
    // Weapon sound is handled in class Weapon
    AudioSource gameOverSound;
    AudioSource itemCollectionSound;
    AudioSource enemyDestructionSound;

    // Start is called before the first frame update
    void Start()
    {
        // Establish the boundaries to prevent the player from moving off the screen
        Vector3 upperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        xLimit = upperRightCorner.x;
        yLimit = upperRightCorner.y;

        // Instantiate the weapon
        weapon = new Weapon((int) Constants.weaponTypes.SINGLESHOT);

        // Instantiate the fake shield
        fakeShield = Instantiate(prefabFakeShield);

        // Player starts the game without shield
        invincible = false;
        fakeShield.SetActive(false);

        // Player starts with 0 score
        score = 0;

        // This variable is used to restart the game when it ends
        gameOver = false;

        // Prepare some audio effects
        // Game over sound
        GameObject prefabGameOverSound = GameObject.Instantiate(Resources.Load("Audio/GameOverSound", typeof(GameObject))) as GameObject;
        gameOverSound = prefabGameOverSound.GetComponent<AudioSource>();

        // Collection sound
        GameObject prefabItemCollectionSound = GameObject.Instantiate(Resources.Load("Audio/ItemCollectionSound", typeof(GameObject))) as GameObject;
        itemCollectionSound = prefabItemCollectionSound.GetComponent<AudioSource>();

        // Enemy destruction
        GameObject prefabEnemyDestructionSound = GameObject.Instantiate(Resources.Load("Audio/EnemyDestructionSound", typeof(GameObject))) as GameObject;
        enemyDestructionSound = prefabEnemyDestructionSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            movementControl();
            preventPlayerMovingOffScreen();
            weaponActivation();
            verifyInvincibility();    
        }
        else
            restartControl();
        
    }

    // Control the movement of player according to WASD or the arrows keys
    public void movementControl()
    {
        float velX = Input.GetAxis("Horizontal");
        float velY = Input.GetAxis("Vertical");
        float speed = Constants.defaultPlayerSpeed;

        playerRb.linearVelocity = new Vector2(speed * velX, speed * velY);
    }

    // Prevent player from going out of the screen
    public void preventPlayerMovingOffScreen()
    {
        if (transform.position.x > xLimit)
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        else if (transform.position.x < -xLimit)
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);

        if (transform.position.y > yLimit)
            transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
        else if (transform.position.y < -yLimit)
            transform.position = new Vector3(transform.position.x, -yLimit, transform.position.z);
    }

    // Handle the fire when the player press spacebar
    public void weaponActivation()
    {
        if (Input.GetKey("space"))
            weapon.activation(transform.position);
    }

    // Update the position of the player'shield if the player is invincible
    public void verifyInvincibility()
    {
        if (invincible == true)
            fakeShield.transform.position = transform.position;
    }

    // This function activates the shield when the player collide with an item
    public void activateShield()
    {
        // If the player already has a shield, ignores the last invocation of the function "desactivateShield"
        if (invincible == true)
            CancelInvoke("desactivateShield");

        invincible = true;
        fakeShield.SetActive(true);

        Invoke("desactivateShield", Constants.shieldLifetime);
    }

    // This function is called when the player loses the shield
    public void desactivateShield()
    {
        invincible = false;
        fakeShield.SetActive(false);
    }

    // Increase player's score
    public void increaseScore(int points)
    {
        score += points;
        scoreUI.text = score.ToString();
    }

    // Called when the player collide with a enemy
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
            handleCollisionWithPlayer();
    }

    // Handle the collision of the player with an enemy
    public void handleCollisionWithPlayer()
    {
        // If player is not invincible the game is over
        if (invincible == false)
        {
            gameOver = true;

            // Activate the Game Over panel
            gameOverPanel.SetActive(true);

            // Show score
            GameObject.Find("Overlay").GetComponent<TextMeshProUGUI>().text = "Game Over\n\nTotal score: " + score.ToString() + "\n\nPress space to restart";

            // Game over sound
            GameObject prefabGameOverSound = GameObject.Instantiate(Resources.Load("Audio/GameOverSound", typeof(GameObject))) as GameObject;
            prefabGameOverSound.GetComponent<AudioSource>().Play();

            // Stop the game
            Time.timeScale = 0;
        }
        else
        {
            // What must happen if the player is invincible?
        }
    }

    // Verify if user wants to restart the game when it is over
    public void restartControl()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }

    // Play sound when hits a enemy
    public void playEnemyDestructionSound()
    {
        enemyDestructionSound.Play();
    }

    // Play sound when hits an item
    public void playItemCollectionSound()
    {
        itemCollectionSound.Play();
    }
}
