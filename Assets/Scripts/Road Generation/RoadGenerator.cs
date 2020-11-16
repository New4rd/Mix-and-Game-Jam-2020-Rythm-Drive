using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadGenerator : MonoBehaviour
{
    static public RoadGenerator Instance;

    [SerializeField] GameObject generableRoad;

    GameObject globalGeneratedRoadObject;
    List<GameObject> generatedRoad;
    int generationIteration = -1;


    private void Awake()
    {
        Instance = this;
        globalGeneratedRoadObject = new GameObject("Global Generated Road Object");
        SceneManager.MoveGameObjectToScene(globalGeneratedRoadObject, SceneManager.GetSceneByName("Game Scene"));
    }


    private void Start()
    {
        generatedRoad = new List<GameObject>();
        InitialGeneration();
    }


    private void InitialGeneration ()
    {
        GenerateRoad();
        GenerateRoad();
        GenerateRoad();
        GenerateRoad();
    }


    public void GenerateRoad ()
    {
        GameObject inst = Instantiate(generableRoad, Vector3.forward * generationIteration * 10, Quaternion.identity);
        inst.transform.parent = globalGeneratedRoadObject.transform;
        generatedRoad.Add(inst);
        generationIteration++;
    }


    public void DestroyLastRoad ()
    {
        Destroy(generatedRoad[0]);
        generatedRoad.RemoveAt(0);
    }
}
