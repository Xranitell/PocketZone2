using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField ]private List<Enemy> EnemiesList = new List<Enemy>();

    [BoxGroup("Spawn params")] [SerializeField] private AnimationCurve delayBetweenWaves;
    [BoxGroup("Spawn params")] [SerializeField] private AnimationCurve countInWave;
    [BoxGroup("Spawn params")] [SerializeField] private int maxEnemyCount = 10;
    [BoxGroup("Spawn params")] [SerializeField] private float spawnOffsetMultiplier = 1f;
    [SerializeField] private Transform enemyContainer;
    private Camera _camera;
    
    
    private void Awake()
    {
        _camera = Camera.main;
        
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int enemyCountInWave = Mathf.RoundToInt(this.countInWave.Evaluate(Time.time));
            
            for (int i = 0; i < enemyCountInWave; i++)
            {
                if (DataHolder.ActiveEnemies.Count >= maxEnemyCount) continue;
                Enemy enemyPrefab = GetRandomByPriority();
                SpawnEnemy(enemyPrefab,GetSpawnPoint());
            }

            yield return new WaitForSeconds(delayBetweenWaves.Evaluate(Time.time));
        }
    }

    public Vector3 GetSpawnPoint()
    {
        Vector3 topLeft = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector3 bottomRight = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        
        Vector3 topRight = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 bottomLeft = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));

        int random = Random.Range(0, 4);

        Vector3 pos;
        
        switch (random)
        {
            case 0: pos = topLeft;
                break;
            case 1: pos = bottomRight;
                break;
            case 2: pos = topRight;
                break;
            default: pos = bottomLeft;
                break;
        }

        pos.z = 0;
        return pos * spawnOffsetMultiplier;
    }
    
    private void SpawnEnemy(Enemy enemyType, Vector3 position)
    {
        Enemy enemy;
        if (DataHolder.EnemyPull.Count > 0)
        {
            enemy = DataHolder.EnemyPull.GetFromPull(enemyType);
        }
        else
        {
            enemy = Instantiate(enemyType, enemyContainer);
        }

        DataHolder.ActiveEnemies.Add(enemy);
        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);
    }
    
    private Enemy GetRandomByPriority()
    {
        var totalSum = EnemiesList.Sum(x => x.spawnPriority);

        var randomValue = Random.Range(0, totalSum);

        float counter = 0;
        foreach (var enemyData in EnemiesList)
        {
            counter += enemyData.spawnPriority;
            if (counter >= randomValue)
            {
                return enemyData;
            }
        }
        return null;
    }

    
}
