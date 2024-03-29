using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;

    public float moveTime, waitTime;

    private float moveCount, waitCount;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D enemyRB;

    public SpriteRenderer enemySR;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);

                enemySR.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;

                }
            }
            else
            {
                enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);

                enemySR.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;

                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }
            anim.SetBool("isMoving", true);
        } else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            enemyRB.velocity = new Vector2(0f, enemyRB.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }
            anim.SetBool("isMoving", false);
        }
    }
}
