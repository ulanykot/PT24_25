﻿using PresentationModel;

namespace PresentationModel.Implementation;

internal class CatalogModel : ICatalogModel
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public bool isBooked { get; set; }

    public CatalogModel(int id, int roomNumber, string roomType, bool isBooked)
    {
        Id = id;
        RoomNumber = roomNumber;
        RoomType = roomType;
        this.isBooked = isBooked;
    }
}
