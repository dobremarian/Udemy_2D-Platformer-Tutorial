using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10f;

    public float pressKeyForce = 0.5f;

    private bool levelLoading;

    public LSManager theManager;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, currentPoint.transform.position) == 0f && !levelLoading)
        {


            if (Input.GetAxisRaw("Horizontal") > pressKeyForce)
            {
                if (currentPoint.right != null)
                {
                    if (Vector3.Distance(transform.position, currentPoint.transform.position) == 0f)
                    {
                        SetNextPoint(currentPoint.right);
                    }

                }
            }

            if (Input.GetAxisRaw("Horizontal") < -pressKeyForce)
            {
                if (currentPoint.left != null)
                {

                    if (Vector3.Distance(transform.position, currentPoint.transform.position) == 0f)
                    {
                        SetNextPoint(currentPoint.left);
                    }

                }
            }

            if (Input.GetAxisRaw("Vertical") > pressKeyForce)
            {
                if (currentPoint.up != null)
                {
                    if (Vector3.Distance(transform.position, currentPoint.transform.position) == 0f)
                    {
                        SetNextPoint(currentPoint.up);
                    }

                }
            }

            if (Input.GetAxisRaw("Vertical") < -pressKeyForce)
            {
                if (currentPoint.down != null)
                {
                    if (Vector3.Distance(transform.position, currentPoint.transform.position) == 0f)
                    {
                        SetNextPoint(currentPoint.down);
                    }

                }
            }

            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIController.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;

                    theManager.LoadLevel();
                }
            }
        }


    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;

        LSUIController.instance.HideInfo();

        AudioManager.instance.PlaySFX(5);
    }
}

