using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float sideMovementSpeed;


    Rigidbody rb;
    Vector3 playerVelocity;

    int xAxisPosition = 1;
    public bool leftMove, rightMove;
    public float playerTiming;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerVelocity = Vector3.forward * movementSpeed;
    }


    private void Update()
    {

        if (GameManager.Instance.IsGameOver())
        {
            return;
        }

        // the player automoves forward
        rb.velocity = playerVelocity;
        

        if (!leftMove && !rightMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (xAxisPosition > 0)
                {
                    xAxisPosition--;
                    leftMove = true;
                }

                playerTiming = Time.time;
                float timingDiff = Mathf.Abs(playerTiming - GameManager.Instance.enemyTiming);
                Debug.Log("TIMING IS:::" + timingDiff);
                if (timingDiff < 0.3f)
                {
                    StartCoroutine(ScenesManager.Instance.DisplayTimingScene());
                    Debug.Log("GOOD TIMING!");
                    ScoreManager.Instance.IncreaseScore(3);
                }
            }


            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (xAxisPosition < GameManager.Instance.xAxisMovements.Count-1)
                {
                    xAxisPosition++;
                    rightMove = true;
                }

                playerTiming = Time.time;
                float timingDiff = Mathf.Abs(playerTiming - GameManager.Instance.enemyTiming);
                Debug.Log("TIMING IS:::" + timingDiff);
                if (timingDiff < 0.3f)
                {
                    StartCoroutine(ScenesManager.Instance.DisplayTimingScene());
                    Debug.Log("GOOD TIMING!");
                    ScoreManager.Instance.IncreaseScore(3);
                }

            }


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerTiming = Time.time;
                float timingDiff = Mathf.Abs(playerTiming - GameManager.Instance.enemyTiming);
                Debug.Log("TIMING IS:::" + timingDiff);
                if (timingDiff < 0.3f)
                {
                    StartCoroutine(ScenesManager.Instance.DisplayTimingScene());
                    Debug.Log("GOOD TIMING!");
                    ScoreManager.Instance.IncreaseScore(3);
                }
            }
        }

        if (leftMove)
        {
            if (transform.position.x > GameManager.Instance.xAxisMovements[xAxisPosition])
            {
                transform.Translate(Vector3.left * sideMovementSpeed / 10);
                //rb.velocity = new Vector3(-sideMovementSpeed, 0, rb.velocity.z);
            }
            
            else if (transform.position.x <= GameManager.Instance.xAxisMovements[xAxisPosition])
            {
                transform.position = new Vector3(GameManager.Instance.xAxisMovements[xAxisPosition], 0, transform.position.z);
                rb.velocity = playerVelocity;
                leftMove = false;
            }
        }

        if (rightMove)
        {
            if (transform.position.x < GameManager.Instance.xAxisMovements[xAxisPosition])
            {
                transform.Translate(Vector3.right * sideMovementSpeed / 10);
                //rb.velocity = new Vector3(sideMovementSpeed, 0, rb.velocity.z);
            }
            
            else if (transform.position.x >= GameManager.Instance.xAxisMovements[xAxisPosition])
            {
                transform.position = new Vector3(GameManager.Instance.xAxisMovements[xAxisPosition], 0, transform.position.z);
                rb.velocity = playerVelocity;
                rightMove = false;
            }
        }
    }
}
