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

    /// <summary>
    /// Transfers all of the contents of the inventory into another inventory
    /// </summary>
    /// <param name="transferTo">The inventory that the contents should be transfered to</param>
    /// <returns>Returns true if the transfer was successful.  Returns false if the transfer failed.</returns>
    public bool TransferAll(Inventory transferTo)
    {
        if (transferTo !=null && transferTo.DoesInventoryHaveEnoughRoom(inventoryList))
        {
            transferTo.Add(inventoryList);
            for (int i = 0; i < inventoryList.Count; i++)
            {
                Remove(inventoryList[i]);
            }
            return true;
        }
        return false;
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
        if(CurrentWeight + item.Weight < MaxWeight)
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
        if(bundle != null && CurrentWeight + bundle.Weight < MaxWeight)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the inventory has enough room for a list of bundles.
    /// </summary>
    /// <param name="bundles">The list of bundles to be added to the inventory</param>
    /// <returns>Returns true if there is enough room for the list of bundles.  Returns false if there isn't enough room.</returns>
    public bool DoesInventoryHaveEnoughRoom(List<Bundle> bundles)
    {
        if(bundles != null)
        {
            float totalWeight = 0;
            foreach(Bundle bundle in bundles)
            {
                totalWeight += bundle.Weight;
            }
            if(CurrentWeight+totalWeight < MaxWeight)
            {
                return true;
            }
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
