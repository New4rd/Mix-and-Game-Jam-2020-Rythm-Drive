using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RoadGenerator.Instance.GenerateRoad();
            RoadGenerator.Instance.DestroyLastRoad();
        }
    }
}
