using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public static ObjectPull<DropItem> DropPull = new ObjectPull<DropItem>();
    public Item itemData;

    [SerializeField] SpriteRenderer renderer;
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            itemData.CurrentCount += itemData.dropCount;
            gameObject.SetActive(false);
            DropPull.PushToPull(this);
        }
    }

    public void Configure()
    {
        gameObject.SetActive(true);
        renderer.sprite = itemData.sprite;
    }
}
