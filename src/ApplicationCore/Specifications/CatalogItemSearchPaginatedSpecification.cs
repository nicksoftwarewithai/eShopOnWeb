using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CatalogItemSearchPaginatedSpecification : Specification<CatalogItem>
{
    public CatalogItemSearchPaginatedSpecification(int skip, int take, string? searchTerm, int? brandId, int? typeId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId) &&
            (string.IsNullOrEmpty(searchTerm) || 
             i.Name.Contains(searchTerm) || 
             i.Description.Contains(searchTerm)))
            .Skip(skip).Take(take);
    }
}