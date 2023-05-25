using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    //Visual elements in cell
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countText;

    public Item ItemInCell 
    { 
        get => _itemInCell;
        set
        {
            try
            {
                ItemInCell.OnLocalItemUpdated -= ConfigureCell;
            }
            catch
            {
                // ignored
            }
            
            _itemInCell = value;
            
            if(_itemInCell)
                ItemInCell.OnLocalItemUpdated += ConfigureCell;
            
            ConfigureCell();
        }
    }
    private Item _itemInCell;

    private void Awake()
    {
        InventorySystem.Instance.OnSelectedCellChanged += InventorySystem.Instance.CellClickHandler;
    }

    public void OnClickEvent()
    {
        InventorySystem.Instance.OnSelectedCellChanged.Invoke(this);
    }

    //Настраивает значения в ячейке
    private void ConfigureCell()
    {
        if (ItemInCell)
        {
            image.sprite = ItemInCell.sprite;
            countText.text = ItemInCell.isStackable ? ItemInCell.CurrentCount.ToString() : "";
        }
        else
        {
            image.sprite = null;
            countText.text = "";
        }
    }
    
    
    //Выбрасывая предмет меняется его колличество на 0
    //очищается ячейка
    public void ClearCell()
    {
        _itemInCell.CurrentCount = 0;
        _itemInCell = null;
    }
}
