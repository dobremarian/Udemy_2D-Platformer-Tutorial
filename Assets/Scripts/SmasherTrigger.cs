using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherTrigger : MonoBehaviour
{
    public GameObject smasher;
    public float waitForReturn, fallSpeed, returnSpeed;
    public float returnCounter;
    private Vector3 originalPosition;
    private bool hasFalen;


    // Start is called before the first frame update
    void Start()
    {
        returnCounter = 0f;
        originalPosition = smasher.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(smasher.transform.position, transform.position) <= 0.01f && returnCounter <= 0f)
        {
            hasFalen = false;
        }
        if(hasFalen)
        {
            smasher.transform.position = Vector3.MoveTowards(smasher.transform.position, transform.position, fallSpeed * Time.deltaTime);

            if (returnCounter > 0f)
            {
                returnCounter -= Time.deltaTime;
            }
        }
        else
        {
            if (returnCounter <= 0f)
            {
                smasher.transform.position = Vector3.MoveTowards(smasher.transform.position, originalPosition, returnSpeed * Time.deltaTime);
            }
        }
    }
  


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && (Vector3.Distance(smasher.transform.position, originalPosition) <= 0.01f))
        {
            
            returnCounter = waitForReturn;
            hasFalen = true;

        }
    }
}

