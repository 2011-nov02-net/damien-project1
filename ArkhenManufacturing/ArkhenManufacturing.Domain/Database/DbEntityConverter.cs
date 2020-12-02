using System;
using System.Linq;

using ArkhenManufacturing.DataAccess;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Extensions;

namespace ArkhenManufacturing.Domain.Database
{
    public static class DbEntityConverter
    {
        #region To Db Entity

        public static DbAddress ToDbAddress(Guid id, AddressData data) {
            data.NullCheck(nameof(data));

            return new DbAddress
            {
                Id = id,
                Line1 = data.Line1,
                Line2 = data.Line2,
                City = data.City,
                State = data.State,
                Country = data.Country,
                ZipCode = data.ZipCode
            };
        }

        public static DbAdmin ToDbAdmin(Admin item) {
            item.NullCheck(nameof(item));

            var data = item.GetData() as AdminData;

            return new DbAdmin
            {
                Id = item.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                UserName = data.Username,
                Password = data.Password,
                Email = data.Email
            };
        }

        public static DbCustomer ToDbCustomer(Customer item) {
            item.NullCheck(nameof(item));

            var data = item.GetData() as CustomerData;

            return new DbCustomer
            {
                Id = item.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                BirthDate = data.BirthDate,
                AddressId = data.AddressId,
                DefaultLocationId = data.DefaultLocationId.Value
            };
        }

        public static DbInventoryEntry ToDbInventoryEntry(InventoryEntry item) {
            item.NullCheck(nameof(item));

            var data = item.GetData() as InventoryEntryData;

            return new DbInventoryEntry
            {
                Id = item.Id,
                ProductId = data.ProductId,
                LocationId = data.LocationId,
                Price = data.Price,
                Discount = data.Discount
            };
        }

        public static DbLocation ToDbLocation(Location item) {
            item.NullCheck(nameof(item));

            var data = item.GetData() as LocationData;

            return new DbLocation
            {
                Id = item.Id,
                AddressId = data.AddressId
            };
        }

        public static DbOrder ToDbOrder(Order item) {
            item.NullCheck(nameof(item));

            var data = item.GetData() as OrderData;

            return new DbOrder
            { 
                Id = item.Id,
                CustomerId = data.CustomerId,
                AdminId = data.AdminId,
                LocationId = data.LocationId,
                PlacementDate = data.PlacementDate
            };
        }

        public static DbOrderLine ToDbOrderLine(OrderLine item) {
            item.NullCheck(nameof(item));

            var data = item.GetData() as OrderLineData;

            return new DbOrderLine
            { 
                Id = item.Id,
                OrderId = data.OrderId,
                ProductId = data.ProductId,
                Count = data.Count,
                PricePerUnit = data.PricePerUnit,
                Discount = data.Discount
            };
        }

        public static DbProduct ToDbProduct(Product item) {
            item.NullCheck(nameof(item));

            return new DbProduct
            { 
                Id = item.Id,
                Name = item.GetName()
            };
        }

        #endregion

        #region To Library Entity

        public static Address ToAddress(DbAddress item) {
            item.NullCheck(nameof(item));
            var data = new AddressData(item.Line1, item.Line2,
                item.City, item.State, item.Country, item.ZipCode);
            return new Address(item.Id, data);
        }

        public static Admin ToAdmin(DbAdmin item) {
            item.NullCheck(nameof(item));

            var data = new AdminData(
                item.FirstName, item.LastName, item.UserName, 
                item.Password, item.Email, item.LocationId);

            return new Admin(item.Id, data);
        }

        public static Customer ToCustomer(DbCustomer item) {
            item.NullCheck(nameof(item));

            var data = new CustomerData(
                item.FirstName, item.LastName, item.UserName, 
                item.Password, item.Email, item.PhoneNumber, item.AddressId, 
                item.BirthDate, item.SignUpDate, item.DefaultLocationId);

            return new Customer(item.Id, data);
        }

        public static InventoryEntry ToInventoryEntry(DbInventoryEntry item) {
            item.NullCheck(nameof(item));

            var data = new InventoryEntryData(
                item.ProductId, item.LocationId, item.Price, 
                item.Discount, item.Count, item.Threshold);

            return new InventoryEntry(item.Id, data);
        }

        public static Location ToLocation(DbLocation item) {
            item.NullCheck(nameof(item));

            var orderIds = item.Orders
                .Select(o => o.Id)
                .ToList();

            var adminIds = item.Admins
                .Select(a => a.Id)
                .ToList();

            var inventoryEntryIds = item.InventoryEntries
                .Select(ie => ie.Id)
                .ToList();

            var data = new LocationData(
                item.Name, item.AddressId, orderIds, 
                adminIds, inventoryEntryIds);

            return new Location(item.Id, data);
        }

        public static Order ToOrder(DbOrder item) {
            item.NullCheck(nameof(item));
            var orderLineIds = item.OrderLines
                .Select(ol => ol.Id)
                .ToList();

            var data = new OrderData(
                item.CustomerId, item.AdminId, item.LocationId, 
                item.PlacementDate, orderLineIds);

            return new Order(item.Id, data);
        }

        public static OrderLine ToOrderLine(DbOrderLine item) {
            item.NullCheck(nameof(item));

            var data = new OrderLineData(
                item.OrderId, item.ProductId, item.Count, 
                item.PricePerUnit, item.Discount);

            return new OrderLine(item.Id, data);
        }

        public static Product ToProduct(DbProduct item) {
            item.NullCheck(nameof(item));

            var data = new ProductData(item.Name);

            return new Product(item.Id, data);
        }

        #endregion
    }
}
