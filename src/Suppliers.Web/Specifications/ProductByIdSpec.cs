﻿using Ardalis.Specification;
using Suppliers.Web.Entities;

namespace Suppliers.Web.Specifications;

public sealed partial class ProductByIdSpec : Specification<Supply>
{
    public ProductByIdSpec(int id)
    {
        Query.Where(p => p.Status != DeliveryStatus.Delivered);
    }
}