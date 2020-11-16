using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    public void RestartButton ()
    {
        Debug.Log("RESTART PRESSED...");
        GameManager.Instance.ResumeGame();
        if (GameManager.Instance.IsGameOver()) StartCoroutine(ScenesManager.Instance.UnloadScene("Gameover Scene"));
        StartCoroutine(ScenesManager.Instance.RestartGame());
    }
}
