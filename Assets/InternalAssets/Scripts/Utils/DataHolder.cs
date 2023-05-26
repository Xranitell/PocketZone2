using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static Camera Camera;
    public static ObjectPull<DropItem> DropPull = new ObjectPull<DropItem>();
    public static ObjectPull<Enemy> EnemyPull = new ObjectPull<Enemy>();
    public static List<DropItem> ActiveDrop = new List<DropItem>();
    public static List<Enemy> ActiveEnemies = new List<Enemy>();

    private void Awake()
    {
        Camera = Camera.main;

        DropPull.Clear();
        EnemyPull.Clear();
        ActiveDrop.Clear();
        ActiveEnemies.Clear();
    }
}
