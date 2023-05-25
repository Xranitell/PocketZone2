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
    public static ObjectPull<Enemy> enemyPull = new ObjectPull<Enemy>();
    
    [SerializeField ]private List<Enemy> EnemiesList = new List<Enemy>();

    [BoxGroup("Spawn params")] [SerializeField] private AnimationCurve delayBetweenWaves;
    [BoxGroup("Spawn params")] [SerializeField] private AnimationCurve countInWave;
    [BoxGroup("Spawn params")] [SerializeField] private float additiveOffsetRadius = 0.1f;
    [BoxGroup("Spawn params")][SerializeField] private int maxEnemyCount = 10;
    
    [SerializeField] private Transform enemyContainer;
    
    private void Awake()
    {
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            
            int countInWave = Mathf.RoundToInt(this.countInWave.Evaluate(Time.time));
            /////////////////////////////////////
            Vector3 randomPoint = Vector3.zero;
            ////////////////////////////////////
            
            
            for (int i = 0; i < countInWave; i++)
            {
                if (Enemy.ActiveMonsters.Count >= maxEnemyCount) continue;
                Enemy enemyPrefab = GetRandomByPriority();
                SpawnEnemy(enemyPrefab,randomPoint);
            }

            yield return new WaitForSeconds(delayBetweenWaves.Evaluate(Time.time));
        }
    }

    private void SpawnEnemy(Enemy enemyType, Vector3 position)
    {
        Enemy enemy;
        if (enemyPull.Count > 0)
        {
            enemy = enemyPull.GetFromPull(enemyType);
        }
        else
        {
            enemy = Instantiate(enemyType, enemyContainer);
        }

        Vector3 additiveOffset = new Vector3
        (
            Random.Range(-additiveOffsetRadius, additiveOffsetRadius),
            Random.Range(-additiveOffsetRadius, additiveOffsetRadius)
        );
        Enemy.ActiveMonsters.Add(enemy);
        enemy.transform.position = position + additiveOffset;
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
