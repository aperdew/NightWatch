using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<Bundle> inventoryList;
    public float maxWeight;

    void Start()
    {
        inventoryList = new List<Bundle>();
        CurrentWeight = 0;
        MaxWeight = maxWeight;
    }

    public float CurrentWeight { get; private set; }
    public float MaxWeight { get; private set; }

    /// <summary>
    /// Adds a bundle to the inventory provided that there is enough room. If the bundle already exists within the inventory, the bundles are merged.
    /// </summary>
    /// <param name="bundle">The bundle to be added to the inventory</param>
    public void Add(Bundle bundle)
    {
        if (DoesInventoryHaveEnoughRoom(bundle))
        {
            Bundle existingBundle = inventoryList.Find(x => x.Type == bundle.Type);
            if (existingBundle != null)
            {
                existingBundle.Merge(bundle);
                UpdateCurrentWeight();
            }
            else
            {
                inventoryList.Add(bundle);
                CurrentWeight += bundle.Weight;
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
            Bundle existingBundle = inventoryList.Find(x => x.Type == item.Name);
            if (existingBundle != null)
            {
                existingBundle.Add(item);
            }
            else
            {
                inventoryList.Add(new Bundle(item));
            }
            CurrentWeight += item.Weight;
        }
    }

    public void Add(List<Bundle> bundles)
    {
        if (bundles != null)
        {
            foreach (Bundle bundle in bundles)
            {
                Add(bundle);
            }
        }
    }

    public Bundle Transfer(Bundle bundle)
    {
        if (bundle != null && inventoryList.Exists(x => x.Type == bundle.Type))
        {
            Remove(bundle);
            return bundle;
        }
        return null;
    }

    public Bundle Transfer(Item item, int i)
    {
        if (inventoryList.Exists(x => x.Type == item.Name && x.Count - i >= 0))
        {
            Bundle existingBundle = inventoryList.Find(x => x.Type == item.Name);
            if (existingBundle != null)
            {
                Bundle temp = existingBundle;
                existingBundle.Remove(item, i);
                if (existingBundle.Count == 0)
                {
                    Remove(existingBundle);
                }
                return temp;
            }
        }
        return null;
    }

    public List<Bundle> TransferAll()
    {
        Bundle[] temp = new Bundle[inventoryList.Count];
        inventoryList.CopyTo(temp);
        for (int i = 0; i < inventoryList.Count; i++)
        {
            Remove(inventoryList[i]);
        }
        return new List<Bundle>(temp);
    }

    public void Remove(Bundle bundle)
    {
        if (inventoryList.Exists(x => x.Type == bundle.Type))
        {
            inventoryList.Remove(bundle);
            CurrentWeight -= bundle.Weight;
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
                    CurrentWeight -= i * item.Weight;
                }
            }
        }
    }

    public List<Bundle> GetInventory()
    {
        return inventoryList;
    }

    public Bundle GetBundle(Item item)
    {
        return inventoryList.Find(x => x.Type == item.Name);
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
        if(bundle != null && CurrentWeight + bundle.Weight <= MaxWeight)
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
