using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutomove : MonoBehaviour
{
    private bool _move;
    public bool move
    {
        get { return _move; }
        set { if (!value) rb.velocity = Vector3.zero; _move = value; }
    }


    [SerializeField] float moveSpeed;

    Vector3 enemyVelocity;
    Rigidbody rb;


    private void Start()
    {
        enemyVelocity = Vector3.back * moveSpeed;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (_move)
        {
            rb.velocity = enemyVelocity;
        }
    }


    public void StopEnemy ()
    {
        move = false;
    }


    public void LaunchEnemy ()
    {
        move = true;
    }


    public void ModifySpeed(float speed)
    {
        enemyVelocity = Vector3.back * speed;
    }
}
