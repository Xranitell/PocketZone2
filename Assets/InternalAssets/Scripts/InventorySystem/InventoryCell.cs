using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public event VoidMethod OnCellClick;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countText;
    private Item _itemInCell;
    public Item ItemInCell 
    { 
        get => _itemInCell;
        set
        {
            _itemInCell = value;
            ConfigureCell(_itemInCell);
        }
    }

    private void Start()
    {
        OnCellClick += InventorySystem.Instance.CellClickHandler;
        if(_itemInCell != null)
            _itemInCell.OnItemDataUpdate += InventorySystem.Instance.LoadInventory;
    }

    public void OnClickEvent()
    {
        OnCellClick.Invoke(this);
    }
    
    private void ConfigureCell(Item item)
    {
        image.sprite = item.sprite;
        countText.text = item.isStackable ? item.CurrentCount.ToString() : "";
    }

    public void ClearCell()
    {
        _itemInCell.CurrentCount = 0;
        _itemInCell = null;
        image.sprite = null;
        countText.text = "";
    }
}
