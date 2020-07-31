using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float minSpeed = 12f;
    public float maxSpeed = 16f;

    public float maxTorque = 10f;

    public float xRange = 4f;
    public float ySpawnPos = -6f;

    public int pointValue = 0;

    private Rigidbody targetRb;
    public ParticleSystem explosionParticle;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomUpwardForce(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomUpwardForce()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        return Vector3.up * randomSpeed;
    }

    Vector3 RandomSpawnPos()
    {
        float randomXPos = Random.Range(-xRange, xRange);
        return new Vector3(randomXPos, ySpawnPos);
    }

    float RandomTorque()
    {
        float randomTorque = Random.Range(0, maxTorque);
        return randomTorque;
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad Object"))
        {
            gameManager.GameOver();
        }
        
    }


}
