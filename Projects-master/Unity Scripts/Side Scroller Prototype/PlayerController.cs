using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public float jumpForce;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver = false;

    int jumpCount = 0;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;


    // Start is called before the first frame update
    void Start()
    {

        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        //allow gravity to be increased
        Physics.gravity *= gravityModifier;

        

    }

    // Update is called once per frame
    void Update()
    {
        
        //make player jump when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            jumpCount++;
        } //double jump
        else if(Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            jumpCount++;
        }

        //counting time since game start
        StopWatch();

    }

    private void OnCollisionEnter(Collision collision)
    {
        //detect if player is on ground or colliding with obstacle
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over man!");
            dirtParticle.Stop();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            
            
        }
        
    }


    void StopWatch()
    {
        if(!gameOver)
        {
            Debug.Log(Time.time);
        }
    }
}
