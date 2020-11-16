using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class to countain most of the global game parameters and functions. This has
/// to be used as a Singleton!
/// </summary>


public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    [Header("Global parameters")]
    [SerializeField] int FPSLimit;

    /// <summary>
    /// Variable used to modify the game speed at will.
    /// </summary>
    [SerializeField] float gameSpeed;

    [Header("X positions for the player")]

    /// <summary>
    /// A list of float countaining the different positions along the X-axis for the
    /// player and for the enemies to spawn.
    /// </summary>
    public List<float> xAxisMovements;

    [Header("Enemy parameters")]

    /// <summary>
    /// Z-coordinate to spawn the enemies in the local player space.
    /// </summary>
    public float ZAxisEnemySpawn;

    /// <summary>
    /// Z-coordinate to make the enemies stop in the local player space.
    /// </summary>
    public float ZAxisEnemyMiddleStop;

    /// <summary>
    /// Z-coordinate to unspawn the enemies in the local player space.
    /// </summary>
    public float ZAxisEnemyUnspawn;

    /// <summary>
    /// Variable to indicate how long the enemies should stop in the middle of the screen.
    /// </summary>
    [SerializeField] float phaseTime;
    public bool debugSpawn;

    [Header("Other properties")]
    [SerializeField] GameObject fadeCanvas;
    public float enemyTiming = 0;

    bool notFirstPhase = false;
    bool onPositionEnemies = false;
    bool destroyedEnemies = true;
    bool gameoverPhase = false;

    private void Awake()
    {
        Instance = this;
        ModifyGameSpeed(gameSpeed);
        Application.targetFrameRate = FPSLimit;
    }


    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(fadeCanvas);
    }


    /// <summary>
    /// Function to launch the game by starting to spawn enemies and putting the
    /// right music.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartGame ()
    {
        AudioManager.Instance.LoadAudio("Between the Buttons gameplay", AudioManager.Source.Music);
        AudioManager.Instance.PlayAudio(AudioManager.Source.Music);

        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(EnemyPhase());
    }


    /// <summary>
    /// Function to spawn and unspawn the enemies in the player's local space.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemyPhase ()
    {
        // Spawning the enemies on the Z-coordinate. If it's debug mode, all of the
        // enemy waves will be the same.
        SpawnEnemies(debug:debugSpawn);

        // Making the enemy move from the spawn to the middle spot
        StartCoroutine(EnemiesSpawnToMiddlePhase());
        if (notFirstPhase) StartCoroutine(EnemiesMiddleToDestroyPhase());

        // We wait until the enemies are on the right middle-position or have been destroyed
        yield return new WaitUntil(() => (onPositionEnemies && destroyedEnemies) || gameoverPhase );

        enemyTiming = Time.time;

        if (gameoverPhase) { Debug.Log("GAME OVER. BREAKING!"); yield break; }

        yield return new WaitForSecondsRealtime(phaseTime);

        if (!notFirstPhase) notFirstPhase = !notFirstPhase;

        // At the end, we call back the function to spawn another enemy wave
        StartCoroutine(EnemyPhase());
    }


    /// <summary>
    /// Function to spawn a wave of enemies. The enemies to spawn are randomly selected
    /// along the 1, 2, 3 or 4 X-coordinate of the list.
    /// </summary>
    /// <param name="debug"></param>
    private void SpawnEnemies(bool debug=false)
    {
        if (debug) { EnemiesGenerator.Instance.GenerateEnemies(new List<int>() { 0, 2, 3 }); return; }

        List<int> spawnable = new List<int>() { 0, 1, 2, 3 };
        spawnable.RemoveAt(UnityEngine.Random.Range(0, 4));
        spawnable.RemoveAt(UnityEngine.Random.Range(0, 3));
        EnemiesGenerator.Instance.GenerateEnemies(spawnable);

        
    }


    /// <summary>
    /// Function to make the enemies move from the spawn to the middle zone.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemiesSpawnToMiddlePhase ()
    {
        onPositionEnemies = false;
        // All the enemies move
        EnemiesGenerator.Instance.LaunchAllEnemies();
        // We wait until they reach the Z-coordinate
        yield return new WaitUntil(() => EnemiesGenerator.Instance.EnemiesZLocalPosition(Convert.ToInt16(notFirstPhase)) < ZAxisEnemyMiddleStop);
        // we make those enemies stop
        EnemiesGenerator.Instance.StopEnemies(Convert.ToInt16(notFirstPhase));
        onPositionEnemies = true;
    }


    /// <summary>
    /// Function the make the enemies move from the middle zone the the unspawn zone.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemiesMiddleToDestroyPhase ()
    {
        destroyedEnemies = false;
        //EnemiesGenerator.Instance.ModifyEnemiesSpeed(40, 0);
        EnemiesGenerator.Instance.LaunchAllEnemies();
        yield return new WaitUntil(() => EnemiesGenerator.Instance.EnemiesZLocalPosition(0) < ZAxisEnemyUnspawn);
        //yield return new WaitForSecondsRealtime(.3f);
        yield return new WaitUntil(() => onPositionEnemies);
        EnemiesGenerator.Instance.DestroyLastEnemies();
        ScoreManager.Instance.IncreaseScore(5);
        destroyedEnemies = true;
    }


    public float GameSpeed ()
    {
        return gameSpeed;
    }


    public void ModifyGameSpeed (float gameSpeed)
    {
        Time.timeScale = gameSpeed;
    }


    public void PauseGame ()
    {
        ModifyGameSpeed(0);
    }


    public void ResumeGame ()
    {
        ModifyGameSpeed(1);
    }


    /// <summary>
    /// Launching the game-over phase: the music stops, the game stops, and we
    /// display the "Game-over" scene.
    /// </summary>
    public void GameoverPhase ()
    {
        AudioManager.Instance.PauseAudio(AudioManager.Source.Music);
        PauseGame();
        StartCoroutine(ScenesManager.Instance.LoadScene("Gameover Scene"));
        gameoverPhase = true;
    }


    public bool IsGameOver ()
    {
        return gameoverPhase;
    }
}
