
using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel: MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text count;

    private Item lastShowedItem;
    
    private void Awake()
    {
        InventorySystem.Instance.OnSelectedCellChanged += GetInfoAboutItem;
    }

    private void GetInfoAboutItem(InventoryCell cell)
    {
        lastShowedItem= cell.ItemInCell;
        if (lastShowedItem == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            name.text = lastShowedItem.name;
            image.sprite = lastShowedItem.sprite;
            description.text = lastShowedItem.description;
            count.text = lastShowedItem.CurrentCount + "/" + lastShowedItem.maxCount;
        }
    }

    public void DeleteItem()
    {
        lastShowedItem.CurrentCount = 0;
    }
}
