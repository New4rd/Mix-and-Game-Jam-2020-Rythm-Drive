using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    static public EnemiesGenerator Instance;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject playerSpace;

    public List<List<GameObject>> enemies;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        enemies = new List<List<GameObject>>();
    }


    public void GenerateEnemies (List<int> zAxisIndexs)
    {
        List<GameObject> _enemies = new List<GameObject>();
        foreach(int i in zAxisIndexs)
        {
            GameObject inst = Instantiate(enemyPrefab, playerSpace.transform) as GameObject;
            //inst.transform.parent = playerSpace.transform;
            inst.transform.localPosition = new Vector3(GameManager.Instance.xAxisMovements[i], 1, GameManager.Instance.ZAxisEnemySpawn);
            _enemies.Add(inst);
        }

        enemies.Add(_enemies);
    }


    public void DestroyLastEnemies ()
    {
        Debug.Log("DESTROYING");

        for (int i = 0; i < (2 + Convert.ToInt16(GameManager.Instance.debugSpawn)) ; i++)
        {
            Destroy(enemies[0][i]);
        }
        enemies.RemoveAt(0);
    }


    public float EnemiesZLocalPosition (int enemyListIndex)
    {
        return enemies[enemyListIndex][0].transform.localPosition.z;
    }


    public void StopEnemies (int enemyListIndex)
    {
        foreach(GameObject enemy in enemies[enemyListIndex])
        {
            enemy.GetComponent<EnemyAutomove>().StopEnemy();
        }
    }


    public void ModifyEnemiesSpeed (float speed, int enemyListIndex)
    {
        foreach (GameObject enemy in enemies[enemyListIndex])
        {
            enemy.GetComponent<EnemyAutomove>().ModifySpeed(speed);
        }
    }


    public void LaunchAllEnemies ()
    {
        foreach (List<GameObject> list in enemies)
        {
            foreach (GameObject enemy in list)
            {
                enemy.GetComponent<EnemyAutomove>().LaunchEnemy();
            }
        }
    }
}
