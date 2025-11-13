using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using Xunit;
using Xunit.Sdk;

namespace ShoppingList.Tests;

/// <summary>
/// Unit tests for ShoppingListService.
///
/// IMPORTANT: Write your tests here using Test Driven Development (TDD)
///
/// TDD Workflow:
/// 1. Write a test for a specific behavior (RED - test fails)
/// 2. Implement minimal code to make the test pass (GREEN - test passes)
/// 3. Refactor the code if needed (REFACTOR - improve without changing behavior)
/// 4. Repeat for the next behavior
///
/// Test Examples:
/// - See ShoppingItemTests.cs for examples of well-structured unit tests
/// - Follow the Arrange-Act-Assert pattern
/// - Use descriptive test names: Method_Scenario_ExpectedBehavior
///
/// What to Test:
/// - Happy path scenarios (normal, expected usage)
/// - Input validation (null/empty IDs, invalid parameters)
/// - Edge cases (empty array, array expansion, last item, etc.)
/// - Array management (shifting after delete, compacting, reordering)
/// - Search functionality (case-insensitive, matching in name/notes)
///
/// Recommended Test Categories:
///
/// GetAll() tests:
/// - GetAll_WhenEmpty_ShouldReturnEmptyList
/// - GetAll_WithItems_ShouldReturnAllItems
/// - GetAll_ShouldNotReturnMoreThanActualItemCount
///
/// GetById() tests:
/// - GetById_WithValidId_ShouldReturnItem
/// - GetById_WithInvalidId_ShouldReturnNull
/// - GetById_WithNullId_ShouldReturnNull
/// - GetById_WithEmptyId_ShouldReturnNull
///
/// Add() tests:
/// - Add_WithValidInput_ShouldReturnItem
/// - Add_ShouldGenerateUniqueId
/// - Add_ShouldIncrementItemCount
/// - Add_WhenArrayFull_ShouldExpandArray
/// - Add_AfterArrayExpansion_ShouldContinueWorking
/// - Add_ShouldSetIsPurchasedToFalse
///
/// Update() tests:
/// - Update_WithValidId_ShouldUpdateAndReturnItem
/// - Update_WithInvalidId_ShouldReturnNull
/// - Update_ShouldNotChangeId
/// - Update_ShouldNotChangeIsPurchased
///
/// Delete() tests:
/// - Delete_WithValidId_ShouldReturnTrue
/// - Delete_WithInvalidId_ShouldReturnFalse
/// - Delete_ShouldRemoveItemFromList
/// - Delete_ShouldShiftRemainingItems
/// - Delete_ShouldDecrementItemCount
/// - Delete_LastItem_ShouldWork
/// - Delete_FirstItem_ShouldWork
/// - Delete_MiddleItem_ShouldWork
///
/// Search() tests:
/// - Search_WithEmptyQuery_ShouldReturnAllItems
/// - Search_WithNullQuery_ShouldReturnAllItems
/// - Search_MatchingName_ShouldReturnItem
/// - Search_MatchingNotes_ShouldReturnItem
/// - Search_ShouldBeCaseInsensitive
/// - Search_WithNoMatches_ShouldReturnEmpty
/// - Search_ShouldFindPartialMatches
///
/// ClearPurchased() tests:
/// - ClearPurchased_WithNoPurchasedItems_ShouldReturnZero
/// - ClearPurchased_ShouldRemoveOnlyPurchasedItems
/// - ClearPurchased_ShouldReturnCorrectCount
/// - ClearPurchased_ShouldShiftRemainingItems
///
/// TogglePurchased() tests:
/// - TogglePurchased_WithValidId_ShouldReturnTrue
/// - TogglePurchased_WithInvalidId_ShouldReturnFalse
/// - TogglePurchased_ShouldToggleFromFalseToTrue
/// - TogglePurchased_ShouldToggleFromTrueToFalse
///
/// Reorder() tests:
/// - Reorder_WithValidOrder_ShouldReturnTrue
/// - Reorder_WithInvalidId_ShouldReturnFalse
/// - Reorder_WithMissingIds_ShouldReturnFalse
/// - Reorder_WithDuplicateIds_ShouldReturnFalse
/// - Reorder_ShouldChangeItemOrder
/// - Reorder_WithEmptyList_ShouldReturnFalse
/// </summary>
public class ShoppingListServiceTests
{
    private readonly ShoppingListService _sut;
    public ShoppingListServiceTests()
    {
        _sut = new();
    }
    // TODO: Write your tests here following the TDD workflow

    // Example test structure:
    [Fact]
    public void Add_WithValidInput_ShouldReturnItem()
    {
        // Arrange


        // Act
        var item = _sut.Add("Milk", 2, "Lactose-free");

        // Assert
        Assert.NotNull(item);
        Assert.Equal("Milk", item!.Name);
        Assert.Equal(2, item.Quantity);
    }
    [Fact]
    public void Add_WithFullArray_ShouldResizeArrayAndContinueAddingItems()
    {
        // Arrange


        // Act
        var items = _sut.Add("Bread", 1, null);
        var iteam2 = _sut.Add("Eggs", 12, null);
        var iteam3 = _sut.Add("Butter", 1, null);
        var iteam4 = _sut.Add("chesse", 1, null);
        var iteam5 = _sut.Add("water", 4, null);
        var iteam6 = _sut.Add("soda", 2, null);


        // Assert
        Assert.NotNull(iteam2);
        Assert.NotNull(iteam3);
        Assert.NotNull(iteam4);
        Assert.NotNull(iteam5);
        Assert.NotNull(iteam6);

        //Assert.Throws(IndexOutOfRangeException , service.Add);

    }

    [Fact]
    public void GetAll_ShouldReturnAllItemsPreviouslyAdded()
    {
        //Arrange

        var item1 = _sut.Add("Milk", 2, "i love cereal");
        var item2 = _sut.Add("Eggs", 50, "I love eggs");
        //Act
        var items = _sut.GetAll();

        //Assert
        Assert.NotNull(items);
        Assert.Contains(item1, items);
        Assert.Contains(item2, items);
    }
    [Theory]
    [InlineData(2)]
    public void GetAll_ShouldReturnCorrectAmountOfItems(int expectedAmount)
    {
        //Arrange 
        for (int i = 0; i < expectedAmount; i++)
        {
            _sut.Add("Test", 1, "Some notes");
        }
        ;

        //Act
        var totalItems = _sut.GetAll();

        //Assert
        Assert.Equal(totalItems.Count, expectedAmount);

    }

    [Fact]
    public void GetIteamById_ShouldReturnIteamById()
    {
        //Arrange

        var expectedItem = _sut.Add("Egg", 50, "more egg");

        Assert.NotNull(expectedItem);

        //Act
        var returnedItem = _sut.GetById(expectedItem.Id);

        //Assert
        Assert.NotNull(returnedItem);
        Assert.Equal(expectedItem.Id, returnedItem.Id);

    }
    [Fact]
    public void Update_ShouldUpdateItemWithNewValues()
    {
        //arrange
        var item = _sut.Add("Test", 1, "test notes");
        var expectedName = "Egg";
        var expectedQuantity = 20;
        var expectedNote = "egg is healthy";

        //act
        Assert.NotNull(item);
        var updatedItem = _sut.Update(item.Id, "Egg", 20, "egg is healthy");

        //assert
        Assert.NotNull(updatedItem);
        Assert.Equal(expectedName, updatedItem.Name);
        Assert.Equal(expectedQuantity, updatedItem.Quantity);
        Assert.Equal(expectedNote, updatedItem.Notes);
    }
    [Fact]
    public void SortByQuantity_ShouldReturnSortedIteams()
    {
        //arrange
        var item = _sut.Add("TestIteam", 2, "test egg");
        var item1 = _sut.Add("TestIteam", 6, "test egg");
        var item2 = _sut.Add("TestIteam", 20, "test egg");
        var item3 = _sut.Add("TestIteam", 1, "test egg");
        var items = _sut.GetAll();
        //var expected =
        //act
        var sortedItems = _sut.SortByQuantity((ShoppingItem[])items!);

        //assert 
        Dictionary<int, int> quantityToIndex = new()
        {
           {0,1},
            {1,2 },
            {2,6 },
            {3,20 }
        };

        for (int i = 0; i < sortedItems.Length - 1; i++)
        {
            Assert.True(sortedItems[i].Quantity == quantityToIndex[i]);
        }
    }


}

