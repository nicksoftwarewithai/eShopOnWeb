using System.Collections.Generic;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;

public class CatalogItemSearchPaginatedSpecification
{
    [Theory]
    [InlineData(0, 2, "NET", null, null, 2)] // First 2 items matching "NET"
    [InlineData(2, 2, "NET", null, null, 1)] // Next 2 items (only 1 left) matching "NET"
    [InlineData(0, 10, "NET", null, null, 3)] // All items matching "NET"
    [InlineData(0, 1, null, null, null, 1)] // First item with no filters
    public void MatchesExpectedNumberOfItems(int skip, int take, string searchTerm, int? brandId, int? typeId, int expectedCount)
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogItemSearchPaginatedSpecification(skip, take, searchTerm, brandId, typeId);

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