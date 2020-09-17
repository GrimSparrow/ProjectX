using System;
using System.Collections.Generic;
using System.Linq;
using Lowscope.Saving;
using Lowscope.Saving.Components;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour, ISaveable
{
    [SerializeField] private int capacity;
    private List<ItemCell> cells;

    private static InventoryController instance;

    public static InventoryController Instance
    {
        get => instance;
        set => instance = value;
    }

    public List<ItemCell> Cells
    {
        get => cells;
        set => cells = value;
    }

    public int Capacity
    {
        get => capacity;
        set => capacity = value;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Duplicate save master found. " +
                             "Ensure that the save master has not been added anywhere in your scene.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        cells = new List<ItemCell>();
        InitInventory();
    }

    private void InitInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            cells.Add(new ItemCell());
        }
    }

    public void AddItem(Saveable item)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].TryAddItem(item))
            {
                return;
            }
        }
    }

    public ItemCell GetItem(PrefabSourceContainer container)
    {
        return cells.FirstOrDefault(c => c.container == container);
    }

    public bool UseItem(PrefabSourceContainer container)
    {
        var item =  cells.FirstOrDefault(c => c.container == container);

        if (item != null)
        {
            cells.Remove(item);
            cells.Add(new ItemCell());
            return true;
        }

        return false;
    }

    public string OnSave()
    {
        return null;
    }

    public void OnLoad(string data)
    {
        
    }

    public bool OnSaveCondition()
    {
        return true;
    }
}

[Serializable]
public class ItemCell
{
    public bool isActive;
    public string Name;
    public string Description;
    public Sprite Pic;
    public string prefabPath;
    public string guid;
    public PrefabSourceContainer container;
    public ItemCell()
    {
    }

    public void Clear()
    {
        prefabPath = null;
    }

    public bool TryAddItem(Saveable item)
    {
        prefabPath = item.container.PathSourceContainer;
        guid = item.SaveIdentification;
        container = item.container;

        Name = item.container.Name;
        Description = item.container.Description;
        Pic = item.container.picture;
        return true;
    }
}