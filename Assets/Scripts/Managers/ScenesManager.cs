using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Class to handle the various Loading/Unloading of the scenes. It's also used for
/// the initial launch of the game. This class has to be used as a Singleton!
/// </summary>


public class ScenesManager : MonoBehaviour
{
    static public ScenesManager Instance;

    public bool sceneOperationDone;


    private void Awake()
    {
        Instance = this;
    }


    /*
     * The Start function initialize the game: it loads the Game scene and the Title
     * scene, and configurates the audio. When the player presses the Space bar, it
     * launches the gameplay itself.
     */
    private IEnumerator Start()
    {
        StartCoroutine(LoadScene("Game Scene"));
        StartCoroutine(LoadScene("Title Scene"));
        yield return new WaitUntil(() => sceneOperationDone);

        AudioManager.Instance.LoadAudio("Between the Buttons Lowpass short", AudioManager.Source.Music);
        AudioManager.Instance.PlayAudio(AudioManager.Source.Music);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        StartCoroutine(UnloadScene("Title Scene"));
        yield return new WaitUntil(() => sceneOperationDone);
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(LoadScene("UI Scene"));
        StartCoroutine(GameManager.Instance.StartGame());
    }


    /// <summary>
    /// Function to load a scene by its name. It's loaded as an Additive scene. That function
    /// has to be used with the sceneOperationDone boolean to handle the loadings.
    /// <code>
    /// Example: StartCoroutine (LoadScene ("GameScene"); yield return new WaitUntil (() => sceneOperationDone);
    /// </code>
    /// </summary>
    /// <param name="sceneName">The scene name</param>
    /// <param name="setActive">Should the scene be set as active?</param>
    /// <returns></returns>
    public IEnumerator LoadScene (string sceneName, bool setActive = false)
    {
        sceneOperationDone = false;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => async.isDone);
        sceneOperationDone = true;
    }


    /// <summary>
    /// Function to unload a scene by its name. That function has to be used with the
    /// sceneOperationDone boolean to handle the loadings.
    /// <code>
    /// Example: StartCoroutine (UnloadScene ("GameScene"); yield return new WaitUntil (() => sceneOperationDone);
    /// </code>
    /// </summary>
    /// <param name="sceneName">The scene name</param>
    /// <returns></returns>
    public IEnumerator UnloadScene (string sceneName)
    {
        sceneOperationDone = false;
        AsyncOperation async = SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.None);
        yield return new WaitUntil(() => async.isDone);
        sceneOperationDone = true;
    }


    /// <summary>
    /// Function to simply restart the game. It unload then load the game and UI scenes.
    /// </summary>
    /// <returns></returns>
    public IEnumerator RestartGame()
    {
        StartCoroutine(UnloadScene("Game Scene"));
        yield return new WaitUntil(() => sceneOperationDone);

        StartCoroutine(UnloadScene("UI Scene"));
        //yield return new WaitUntil(() => sceneOperationDone);

        StartCoroutine(LoadScene("Game Scene"));
        yield return new WaitUntil(() => sceneOperationDone);

        yield return new WaitUntil(() => AudioManager.Instance != null);
        AudioManager.Instance.LoadAudio("Between the Buttons Lowpass short", AudioManager.Source.Music);
        AudioManager.Instance.PlayAudio(AudioManager.Source.Music);
        

        StartCoroutine(LoadScene("UI Scene"));
        //yield return new WaitUntil(() => sceneOperationDone);

        yield return new WaitForSecondsRealtime(2);

        StartCoroutine(GameManager.Instance.StartGame());
    }


    public IEnumerator DisplayTimingScene ()
    {
        StartCoroutine(LoadScene("UI Timing"));
        yield return new WaitUntil(() => sceneOperationDone);
        yield return new WaitForSecondsRealtime(.5f);
        StartCoroutine(UnloadScene("UI Timing"));
    }
}
