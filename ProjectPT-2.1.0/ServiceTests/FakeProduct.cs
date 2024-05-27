﻿using Data.API;

namespace ServiceTests;

internal class FakeProduct : ICatalog
{
    public FakeProduct(int id, string name, double price)
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }
}
