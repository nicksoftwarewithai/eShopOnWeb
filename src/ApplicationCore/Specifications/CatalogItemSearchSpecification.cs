using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CatalogItemSearchSpecification : Specification<CatalogItem>
{
    public CatalogItemSearchSpecification(string? searchTerm, int? brandId, int? typeId)
    {
        Query.Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId) &&
            (string.IsNullOrEmpty(searchTerm) || 
             i.Name.Contains(searchTerm) || 
             i.Description.Contains(searchTerm)));
    }
}