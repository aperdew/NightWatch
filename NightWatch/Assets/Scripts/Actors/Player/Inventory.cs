using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<Bundle> inventoryList;
    readonly float maxWeight = 100;

    void Start()
    {
        inventoryList = new List<Bundle>();
    }

    public float CurrentWeight { get; private set; }
    public float MaxWeight { get { return maxWeight; } }

    /// <summary>
    /// Adds a bundle to the inventory provided that there is enough room. If the bundle already exists within the inventory, the bundles are merged.
    /// </summary>
    /// <param name="bundle">The bundle to be added to the inventory</param>
    public void Add(Bundle bundle)
    {
        if (DoesInventoryHaveEnoughRoom(bundle))
        {
            if (!inventoryList.Contains(bundle))
            {
                inventoryList.Add(bundle);
                CurrentWeight += bundle.Weight;
            }
            else
            {
                Bundle existingBundle = inventoryList.Find(x => x.Type == bundle.Type);
                if (existingBundle != null)
                {
                    existingBundle.Merge(bundle);
                    UpdateCurrentWeight();
                }
            }
        }
    }

    /// <summary>
    /// Adds an item to a bundle to the inventory provided that there is enough room and the bundle exists. If the bundle doesn't already exists within the inventory, 
    /// a new bundle is created.
    /// </summary>
    /// <param name="item">The item to be added to the inventory</param>
    public void Add(Item item)
    {
        if (DoesInventoryHaveEnoughRoom(item))
        {
            if (!inventoryList.Exists(x => x.Type == item.Name))
            {
                inventoryList.Add(new Bundle(item));
            }
            else
            {
                Bundle existingBundle = inventoryList.Find(x => x.Type == item.Name);
                if (existingBundle != null)
                {
                    existingBundle.Add(item);
                }
            }
            CurrentWeight += item.Weight;
        }
    }

    public void Remove(Bundle bundle)
    {
        if (inventoryList.Contains(bundle))
        {
            inventoryList.Remove(bundle);
        }
    }

    public void Remove(Item item, int i)
    {
        if (inventoryList.Exists(x => x.Type == item.Name && x.Count-i>=0))
        {
            Bundle existingBundle = inventoryList.Find(x => x.Type == item.Name);
            if (existingBundle != null)
            {
                existingBundle.Remove(item, i);
                if(existingBundle.Count ==0)
                {
                    inventoryList.Remove(existingBundle);
                }
            }
        }
    }

    /// <summary>
    /// Checks if the inventory is full or not.
    /// </summary>
    /// <returns>Returns true if the inventory is full. Returns false if there isn't room</returns>
    public bool IsInventoryFull()
    {
        if( CurrentWeight < MaxWeight)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the inventory has enough room for the item.
    /// </summary>
    /// <param name="item">The item to be added to the inventory</param>
    /// <returns>Returns true if there is enough room for the item.  Returns false if there isn't enough room.</returns>
    public bool DoesInventoryHaveEnoughRoom(Item item)
    {
        if(CurrentWeight + item.Weight <= MaxWeight)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the inventory has enough room for the bundle.
    /// </summary>
    /// <param name="bundle">The bundle to be added to the inventory</param>
    /// <returns>Returns true if there is enough room for the bundle.  Returns false if there isn't enough room.</returns>
    public bool DoesInventoryHaveEnoughRoom(Bundle bundle)
    {
        if(CurrentWeight + bundle.Weight <= MaxWeight)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Resets the current weight of the inventory and recalculates it.
    /// </summary>
    private void UpdateCurrentWeight()
    {
        CurrentWeight = 0;
        foreach(Bundle bundle in inventoryList)
        {
            CurrentWeight += bundle.Weight;
        }
    }
    
}
