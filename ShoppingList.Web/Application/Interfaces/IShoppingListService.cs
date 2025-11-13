using ShoppingList.Domain.Models;

namespace ShoppingList.Application.Interfaces;

public interface IShoppingListService
{
    IReadOnlyList<ShoppingItem> GetAll();
    ShoppingItem? GetById(string id);
    ShoppingItem? Add(string name, int quantity, string? notes);
    ShoppingItem? Update(string id, string name, int quantity, string? notes);
    bool Delete(string id);
    IReadOnlyList<ShoppingItem> Search(string query);
    int ClearPurchased();
    bool TogglePurchased(string id);
    bool Reorder(IReadOnlyList<string> orderedIds);
    ShoppingItem[] SortByQuantity(ShoppingItem[] items);
    ShoppingItem[] SortByAlphabet(ShoppingItem[] itemsB);
}