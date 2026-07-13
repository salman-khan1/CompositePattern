using CompositePattern;

public class Folder : IFileSystem
{
    private string _name;

    private List<IFileSystem> _items = new List<IFileSystem>();

    public Folder(string name)
    {
        _name = name;
    }

    public void Add(IFileSystem item)
    {
        _items.Add(item);
    }

    public void Remove(IFileSystem item)
    {
        _items.Remove(item);
    }

    public void Display()
    {
        Console.WriteLine("Folder : " + _name);

        foreach (var item in _items)
        {
            item.Display();
        }
    }
}