using Service.API;

namespace PresentationTests;

internal class FakeProductDTO : IProductDTO
{
    public FakeProductDTO(int id, string name, double price)
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

}
