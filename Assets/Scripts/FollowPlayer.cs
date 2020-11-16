using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 shift;
    [SerializeField] bool followXAxis = false;

    private void Update()
    {
        transform.position = new Vector3(
            (player.transform.position.x + shift.x) * Convert.ToInt16(followXAxis),
            player.transform.position.y + shift.y -1,
            player.transform.position.z + shift.z);
    }
}
