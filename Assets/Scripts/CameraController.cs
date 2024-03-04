using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;

    //private float lastXPos;
    private Vector2 lastXYPosition;

    public bool stopFollow;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //lastXPos = transform.position.x;
        lastXYPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);*/
        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);


            //float amountToMoveX = transform.position.x - lastXPos;
            Vector2 amountToMoveXY = new Vector2(transform.position.x, transform.position.y) - lastXYPosition;

            farBackground.position = farBackground.position + new Vector3(/*amountToMoveX*/amountToMoveXY[0], amountToMoveXY[1], 0f);
            middleBackground.position += new Vector3(/*amountToMoveX*/amountToMoveXY[0], amountToMoveXY[1], 0f) * 0.5f;

            //lastXPos = transform.position.x;
            lastXYPosition = new Vector2(transform.position.x, transform.position.y);
        }

    }
}
