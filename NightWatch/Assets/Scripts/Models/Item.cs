public class Item {
    public Item(string name, float weight)
    {
        Name = name;
        Weight = weight;
    }
    public string Name { get; private set; }

    public float Weight { get; private set; }
}
