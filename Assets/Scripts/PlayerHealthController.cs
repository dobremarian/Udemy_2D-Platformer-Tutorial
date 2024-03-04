using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHeath;

    public float invincibleLength;

    private float invincibleCounter;

    private SpriteRenderer playerSR;

    public GameObject deathEffect;

    private void Awake()
    {
        //Awake happens before Start()
        instance = this; //this = the script itself. = > makes the script static
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHeath;
        playerSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if(invincibleCounter <= 0)
            {
                playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if(invincibleCounter <= 0 )
        {

        currentHealth--;

            if(currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false);

                Instantiate(deathEffect, transform.position, transform.rotation);


                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 0.5f);

                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9);
            }

        UIController.instance.UpdateHeathDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHeath)
        {
            currentHealth = maxHeath;
        }

        UIController.instance.UpdateHeathDisplay();
    }
}
