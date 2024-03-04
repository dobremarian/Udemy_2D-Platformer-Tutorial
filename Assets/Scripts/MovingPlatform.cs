using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int curentPoint;
    public Transform movingPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movingPlatform.position = Vector3.MoveTowards(movingPlatform.position, points[curentPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(movingPlatform.position, points[curentPoint].position) < 0.5f)
        {
            curentPoint++;

            if(curentPoint >= points.Length)
            {
                curentPoint = 0;
            }
        }
    }
}
