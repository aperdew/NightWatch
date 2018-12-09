public class Bundle{
    private Item item;

    public Bundle(Item item)
    {
        this.item = item;
        Count = 0;
        Weight = 0;
    }

    public int Count { get; private set; }

    public float Weight { get; private set; }

    public string Type { get { return item.Name; } }

    /// <summary>
    /// Adds an item to the bundle if they have the same type.
    /// </summary>
    /// <param name="item">The item to add to the bundle.</param>
    public void Add(Item item)
    {
        if(item.Name == Type)
        {
            Count += 1;
            Weight += item.Weight;
        }
    }

    /// <summary>
    /// Adds an item to the bundle if they have the same type.
    /// </summary>
    /// <param name="itemName">The name of the item to add to the bundle.</param>
    public void Add(string itemName)
    {
        if (itemName == Type)
        {
            Count += 1;
            Weight += item.Weight;
        }
    }

    /// <summary>
    /// Removes an item from the bundle if they have the same type and the bundle isn't empty.
    /// </summary>
    /// <param name="item">The item to remove from the bundle.</param>
    public void Remove(Item item)
    {
        if (item.Name == Type && Count !=0)
        {
            Count -= 1;
            Weight -= item.Weight;
        }
    }

    /// <summary>
    /// Removes a number of items from the bundle if they have the same type and the bundle isn't empty.
    /// </summary>
    /// <param name="item">Tthe item to remove from the bundle.</param>
    /// <param name="i">The number of items to remove</param>
    public void Remove(Item item, int i)
    {
        if (item.Name == Type && Count - i >= 0)
        {
            Count -= i;
            Weight -= i * item.Weight;
        }
    }

    /// <summary>
    /// Removes an item from the bundle if they have the same type and the bundle isn't empty.
    /// </summary>
    /// <param name="itemName">The name of the item to remove from the bundle.</param>
    public void Remove(string itemName)
    {
        if (itemName == Type && Count>= 0)
        {
            Count -= 1;
            Weight -= item.Weight;
        }
    }

    /// <summary>
    /// Removes a number of items from the bundle if they have the same type and the bundle isn't empty.
    /// </summary>
    /// <param name="itemName">The name of the item to remove from the bundle.</param>
    /// <param name="i">The number of items to remove</param>
    public void Remove(string itemName, int i)
    {
        if (itemName == Type && Count - i >= 0)
        {
            Count -= i;
            Weight -= i * item.Weight;
        }
    }

    /// <summary>
    /// Merges two bundles together if they have the same type.
    /// </summary>
    /// <param name="bundle">The bundle to merge into the current bundle</param>
    public void Merge(Bundle bundle)
    {
        if (bundle.Type == Type)
        {
            Count += bundle.Count;
            Weight += bundle.Weight;
        }
    }
}
