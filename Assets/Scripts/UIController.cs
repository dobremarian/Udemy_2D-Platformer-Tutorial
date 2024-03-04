using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Image heart_1, heart_2, heart_3;

    public Sprite heartFull, heartHalf, heartEmpty;

    public Text gemScore;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    

    public GameObject levelCompleteText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
        FadeFromBlack();
        
        fadeScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
        
    }

    public void UpdateHeathDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 6:
                heart_1.sprite = heart_2.sprite = heart_3.sprite = heartFull;

                break;
            case 5:
                heart_1.sprite = heart_2.sprite = heartFull;
                heart_3.sprite = heartHalf;

                break;

            case 4:
                heart_1.sprite = heart_2.sprite = heartFull;
                heart_3.sprite = heartEmpty;

                break;
            case 3:
                heart_1.sprite = heartFull;
                heart_2.sprite = heartHalf;
                heart_3.sprite = heartEmpty;

                break;

            case 2:
                heart_1.sprite = heartFull;
                heart_2.sprite = heart_3.sprite = heartEmpty;

                break;
            case 1:
                heart_1.sprite = heartHalf;
                heart_2.sprite = heart_3.sprite = heartEmpty;

                break;

            case 0:
                heart_1.sprite = heart_2.sprite = heart_3.sprite = heartEmpty;

                break;
            default:
                heart_1.sprite = heart_2.sprite = heart_3.sprite = heartEmpty;

                break;
        }
    }

    public void UpdateGemCount()
    {
        gemScore.text = LevelManager.instance.gemsCollected.ToString();
    }

    public void FadeToBlack()
    {
        fadeScreen.enabled = true;
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
       
    }
}
