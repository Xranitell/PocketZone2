using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public delegate void VoidMethod(InventoryCell cell);

[Serializable]
public class InventorySystem : MonoBehaviour
{
    
    public UnityAction OnGlobalItemUpdated;
    public UnityAction<InventoryCell> OnSelectedCellChanged;
    
    public List<InventoryCell> cells = new List<InventoryCell>();
    public List<Item> allItemsList = new List<Item>();

    [SerializeField] private InfoPanel panel;

    public InventoryCell selectedCell;
    
    public static InventorySystem Instance;
    private void Awake()
    {
        Instance = this;
        allItemsList = Resources.LoadAll<Item>("Items").ToList();
        
        OnGlobalItemUpdated += UpdateInventory;
    }

    public void UpdateInventory()
    {
        ClearAllCells();
        
        var counter = 0;
        foreach (var item in allItemsList)
        {
            if (item.EnableInInventory)
            {
                cells[counter].ItemInCell = item;
                counter++;
            }
        }
    }

    private void ClearAllCells()
    {
        foreach (var cell in cells)
        {
            cell.ItemInCell = null;
        }
    }

    public void CellClickHandler(InventoryCell cell)
    {
        selectedCell = cell;
    }

    public void ClearSelectedCell()
    {
        selectedCell.ClearCell();
        UpdateInventory();
    }
}
