using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            AudioManager.Instance.LoadAudio("crash", AudioManager.Source.Sound);
            AudioManager.Instance.PlayAudio(AudioManager.Source.Sound);
            GameManager.Instance.GameoverPhase();
        }
    }
}
