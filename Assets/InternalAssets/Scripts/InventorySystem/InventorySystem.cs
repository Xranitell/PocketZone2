using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void VoidMethod(InventoryCell cell);

[Serializable]
public class InventorySystem : MonoBehaviour
{
    public List<InventoryCell> cells = new List<InventoryCell>();
    public List<Item> allItemsList = new List<Item>();

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text name;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text count;

    public InventoryCell selectedCell;
    public static InventorySystem Instance;
    private void Awake()
    {
        Instance = this;
        allItemsList = Resources.LoadAll<Item>("Items").ToList();
        LoadInventory();
    }

    public void LoadInventory()
    {
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

    public void CellClickHandler(InventoryCell cell)
    {
        if (cell.ItemInCell != null)
        {
            infoPanel.SetActive(true);
            selectedCell = cell;
            GetInfoAboutItem(selectedCell.ItemInCell);
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }

    private void GetInfoAboutItem(Item item)
    {
        name.text = item.name;
        image.sprite = item.sprite;
        description.text = item.description;
        count.text = item.CurrentCount + "/" + item.maxCount;
    }
    
    public void ClearSelectedCell()
    {
        infoPanel.SetActive(false);
        selectedCell.ClearCell();
        LoadInventory();
    }
}
