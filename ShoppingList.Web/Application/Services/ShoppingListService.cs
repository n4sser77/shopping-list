using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Models;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items;
    private int _nextIndex;

    public ShoppingListService()
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        _items = InitializeArray();
        _nextIndex = 0;
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        return _items;
        // TODO: Students - Return all items from the array (up to _nextIndex)

    }

    public ShoppingItem? GetById(string id)
    {
        foreach (var item in _items)
        {
            if (item.Id.Equals(id))
                return item;
        }

        return null;
    }

    public ShoppingItem? Add(string name, int quantity, string? notes)
    {

        if (_nextIndex == _items.Length - 1)
        {
            var newSize = _items.Length * 2;
            ShoppingItem[] newArray = new ShoppingItem[newSize];
            // copies array elements to new array
            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }
            _items = newArray;

        }

        _items[_nextIndex] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Quantity = quantity,
            Notes = notes,
            IsPurchased = false
        };
        var item = _items[_nextIndex];
        _nextIndex++;
        return item;
    }

    public ShoppingItem? Update(string id, string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        // Return the updated item, or null if not found
        return null;
    }

    public bool Delete(string id)
    {
        // TODO: Students - Implement this method
        // Return true if deleted, false if not found
        return false;
    }

    public IReadOnlyList<ShoppingItem> Search(string query)
    {
        // TODO: Students - Implement this method
        // Return the filtered items
        return [];
    }

    public int ClearPurchased()
    {
        // TODO: Students - Implement this method
        // Return the count of removed items
        return 0;
    }

    public bool TogglePurchased(string id)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false if item not found
        return false;
    }

    public bool Reorder(IReadOnlyList<string> orderedIds)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false otherwise
        return false;
    }

    private ShoppingItem[] InitializeArray()
    {
        var items = new ShoppingItem[5];
        //items[0] = new ShoppingItem
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    Name = "Dishwasher tablets",
        //    Quantity = 1,
        //    Notes = "80st/pack - Rea",
        //    IsPurchased = false
        //};
        //items[1] = new ShoppingItem
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    Name = "Ground meat",
        //    Quantity = 1,
        //    Notes = "2kg - origin Sweden",
        //    IsPurchased = false
        //};
        //items[2] = new ShoppingItem
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    Name = "Apples",
        //    Quantity = 10,
        //    Notes = "Pink Lady",
        //    IsPurchased = false
        //};
        //items[3] = new ShoppingItem
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    Name = "Toothpaste",
        //    Quantity = 1,
        //    Notes = "Colgate",
        //    IsPurchased = false
        //};
        return items;
    }
}