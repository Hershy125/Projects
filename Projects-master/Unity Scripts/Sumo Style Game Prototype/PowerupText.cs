using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupText : MonoBehaviour
{
    public Text powerupText;
    PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.hasJumpPowerup)
        {
            GetComponent<Text>().enabled = true;
            powerupText.text = "Jump Powerup! Hit Spacebar to Jump Smash!";
            StartCoroutine(PowerupTextCountdownRoutine());
        }
        if(playerController.hasForcePowerup)
        {
            GetComponent<Text>().enabled = true;
            powerupText.text = "\nForce Powerup! Extra Smashing Force!";
            StartCoroutine(PowerupTextCountdownRoutine());
        }
        if(playerController.hasSpeedPowerup)
        {
            GetComponent<Text>().enabled = true;
            powerupText.text = "\nSpeed Powerup! Extra Movement Speed!";
            StartCoroutine(PowerupTextCountdownRoutine());
        }
    }

    IEnumerator PowerupTextCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        GetComponent<Text>().enabled = false;

    }
}
