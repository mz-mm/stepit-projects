namespace Monefy.Models;

public class Category
{
    public string Name { get; set; }
    public Icon Icon { get; set; }

    public Category(string name, Icon icon)
    {
        Name = name;
        Icon = icon;
    }
}
