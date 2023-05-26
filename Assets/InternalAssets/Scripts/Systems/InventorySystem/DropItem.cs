using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item itemData;

    [SerializeField] SpriteRenderer spriteRenderer;

    private static Transform _parentForSpawn;
    private void Awake()
    {
        if (!_parentForSpawn) _parentForSpawn = GameObject.Find("DropItemsContainer").transform;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            itemData.CurrentCount += itemData.dropCount;
            gameObject.SetActive(false);
            DataHolder.DropPull.PushToPull(this);
        }
    }

    private void OnEnable() => DataHolder.ActiveDrop.Add(this);
    private void OnDisable() => DataHolder.ActiveDrop.Remove(this);

    public void Configure()
    {
        transform.SetParent(_parentForSpawn);
        gameObject.SetActive(true);
        spriteRenderer.sprite = itemData.sprite;
    }
}
