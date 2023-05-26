
using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel: MonoBehaviour
{
    [SerializeField] private TMP_Text infoName;
    [SerializeField] private Image infoImage;
    [SerializeField] private TMP_Text infoDescription;
    [SerializeField] private TMP_Text infoCount;

    private Item _lastShowedItem;
    
    private void Awake()
    {
        InventorySystem.Instance.OnSelectedCellChanged += GetInfoAboutItem;
    }

    private void GetInfoAboutItem(InventoryCell cell)
    {
        if (cell == null || cell.ItemInCell == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            _lastShowedItem= cell.ItemInCell;
            gameObject.SetActive(true);
            infoName.text = _lastShowedItem.itemName;
            infoImage.sprite = _lastShowedItem.sprite;
            infoDescription.text = _lastShowedItem.description;
            infoCount.text = _lastShowedItem.CurrentCount + "/" + _lastShowedItem.maxCount;
        }
    }

    public void DeleteItem()
    {
        _lastShowedItem.CurrentCount = 0;
        GetInfoAboutItem(null);
    }
}
