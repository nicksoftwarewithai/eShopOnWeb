using System.Collections.Generic;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;

public class CatalogItemSearchSpecification
{
    [Theory]
    [InlineData(null, null, null, 5)] // No filters
    [InlineData("NET", null, null, 3)] // Search term "NET" should match 3 items
    [InlineData("Mug", null, null, 2)] // Search term "Mug" should match 2 items
    [InlineData("Test", null, null, 1)] // Search term "Test" should match 1 item
    [InlineData("NotFound", null, null, 0)] // Search term not found
    [InlineData("NET", 1, null, 2)] // Search "NET" with brand filter
    [InlineData("NET", null, 1, 1)] // Search "NET" with type filter
    [InlineData("NET", 1, 1, 1)] // Search "NET" with both filters
    public void MatchesExpectedNumberOfItems(string searchTerm, int? brandId, int? typeId, int expectedCount)
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogItemSearchSpecification(searchTerm, brandId, typeId);

        var result = spec.Evaluate(GetTestItemCollection()).ToList();

        Assert.Equal(expectedCount, result.Count());
    }

    public List<CatalogItem> GetTestItemCollection()
    {
        return new List<CatalogItem>()
            {
                new CatalogItem(1, 1, ".NET Bot Description", ".NET Bot Black Sweatshirt", 19.5M, "FakePath1"),
                new CatalogItem(1, 2, ".NET Bot Mug Description", ".NET Black & White Mug", 8.50M, "FakePath2"),
                new CatalogItem(2, 1, "Prism Description", "Prism White T-Shirt", 12M, "FakePath3"),
                new CatalogItem(2, 2, ".NET Foundation Description", ".NET Foundation Sweatshirt", 12M, "FakePath4"),
                new CatalogItem(3, 1, "Test product with mug description", "Test White Mug", 12M, "FakePath5"),
            };
    }
}