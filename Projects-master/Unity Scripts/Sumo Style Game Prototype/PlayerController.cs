using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject[] powerupIndicators;

    public float speed = 10.0f;
    private float forwardInput;
    public float forcePowerupStrength = 10f;
    public float speedPowerupBoost = 10f;
    public float jumpForce = 5f;
    public float jumpSmashForce = 15f;
    

    public bool useMouse = false;
    public bool isOnGround = true;
    public bool hasPowerup = false;
    public bool hasForcePowerup = false;
    public bool hasSpeedPowerup = false;
    public bool hasJumpPowerup = false;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {

        //press 'C' to switch between mouse and keys
        if (Input.GetKeyDown(KeyCode.C))
        {
            useMouse = !useMouse;
        }

        if(useMouse)
        {
            forwardInput = Input.GetAxis("Mouse Y");
        }
        else
        {
            forwardInput = Input.GetAxis("Vertical");
        }

        //keep powerup indicators moving with player
        foreach (GameObject indicator in powerupIndicators)
        {
            indicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }

        //moves player in direction camera is pointed
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        if (hasPowerup == false)
        {
            hasForcePowerup = false;
            hasSpeedPowerup = false;
            speed = 5f;
            hasJumpPowerup = false;
        }

        //allow player to jump if on ground and has jump powerup
        if (hasJumpPowerup)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }

        if (transform.position.y < -5)
        {
            isGameOver = true;
            Debug.Log("Game Over Man!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Force Powerup"))
        {
            hasPowerup = true;
            hasForcePowerup = true;
            powerupIndicators[2].gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        else if(other.CompareTag("Speed Powerup"))
        {
            hasPowerup = true;
            hasSpeedPowerup = true;
            speed += speedPowerupBoost;
            powerupIndicators[0].gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        else if(other.CompareTag("Jump Powerup"))
        {
            hasPowerup = true;
            hasJumpPowerup = true;
            powerupIndicators[1].gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        foreach (GameObject indicator in powerupIndicators)
        {
            indicator.gameObject.SetActive(false);
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRb;

        Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Enemy") && hasForcePowerup)
        {
            enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(awayFromPlayer * forcePowerupStrength, ForceMode.Impulse);
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
        if (collision.gameObject.CompareTag("Ground") && hasJumpPowerup)
        {
            isOnGround = true;
            var enemies = FindObjectsOfType<Enemy>();
            foreach(Enemy enemy in enemies)
            {
                enemyRb = enemy.GetComponent<Rigidbody>();
                enemyRb.AddForce(awayFromPlayer * jumpSmashForce, ForceMode.Impulse);
            }
            
        }
    }
}
