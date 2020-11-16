using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = string.Format("You made it to\n{0} beats!", ScoreManager.Instance.GetScore());
    }
}
