using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Expandable] public List<Enemy> EnemiesList = new List<Enemy>();

    private void Awake()
    {
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Enemy enemyPrefab = GetRandomByPriority();

            if (Enemy.EnemyPull.Count > 0)
            {
                var entity = Enemy.EnemyPull.GetFromPull(enemyPrefab);
            }
            else
            {
                Instantiate(enemyPrefab);
            }

            yield return new WaitForSeconds(5f);
        }
    }
    
    public Enemy GetRandomByPriority()
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
