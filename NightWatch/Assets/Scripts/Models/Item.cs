public class Item {
    public Item(string name, float weight)//, float collectionTime)
    {
        Name = name;
        Weight = weight;
       // CollectionTime = collectionTime;
    }
    public string Name { get; private set; }

    public float Weight { get; private set; }

    //public float CollectionTime { get; private set; }
}
