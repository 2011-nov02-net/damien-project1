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

        public static DbAdmin ToDbAdmin(Guid id, AdminData data) {
            data.NullCheck(nameof(data));

            return new DbAdmin
            {
                Id = id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                UserName = data.UserName,
                Password = data.Password,
                Email = data.Email
            };
        }

        public static DbCustomer ToDbCustomer(Guid id, CustomerData data) {
            data.NullCheck(nameof(data));

            return new DbCustomer
            {
                Id = id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                BirthDate = data.BirthDate,
                AddressId = data.AddressId,
                DefaultLocationId = data.DefaultLocationId.Value
            };
        }

        public static DbInventoryEntry ToDbInventoryEntry(Guid id, InventoryEntryData data) {
            data.NullCheck(nameof(data));

            return new DbInventoryEntry
            {
                Id = id,
                ProductId = data.ProductId,
                LocationId = data.LocationId,
                Price = data.Price,
                Discount = data.Discount
            };
        }

        public static DbLocation ToDbLocation(Guid id, LocationData data) {
            data.NullCheck(nameof(data));

            return new DbLocation
            {
                Id = id,
                AddressId = data.AddressId
            };
        }

        public static DbOrder ToDbOrder(Guid id, OrderData data) {
            data.NullCheck(nameof(data));

            return new DbOrder
            { 
                Id = id,
                CustomerId = data.CustomerId,
                AdminId = data.AdminId,
                LocationId = data.LocationId,
                PlacementDate = data.PlacementDate
            };
        }

        public static DbOrderLine ToDbOrderLine(Guid id, OrderLineData data) {
            data.NullCheck(nameof(data));

            return new DbOrderLine
            { 
                Id = id,
                OrderId = data.OrderId,
                ProductId = data.ProductId,
                Count = data.Count,
                PricePerUnit = data.PricePerUnit,
                Discount = data.Discount
            };
        }

        public static DbProduct ToDbProduct(Guid id, ProductData data) {
            data.NullCheck(nameof(data));

            return new DbProduct
            { 
                Id = id,
                Name = data.Name
            };
        }

        public static DbLocationAdmin ToLocationAdmin(Guid locationId, LocationData locationData, Guid adminId, AdminData adminData) {
            locationData.NullCheck(nameof(locationData));
            adminData.NullCheck(nameof(adminData));

            return new DbLocationAdmin
            {
                LocationId = locationId,
                Location = ToDbLocation(locationId, locationData),

                AdminId = adminId,
                Admin = ToDbAdmin(adminId, adminData)
            };
        }

        #endregion

        #region To Lib Entity

        public static AddressData ToAddress(DbAddress item) {
            item.NullCheck(nameof(item));
            return new AddressData(item.Line1, item.Line2,
                item.City, item.State, item.Country, item.ZipCode);
        }

        public static AdminData ToAdmin(DbAdmin item) {
            item.NullCheck(nameof(item));
            var locationIds = item.LocationAdmins
                .Select(la => la.LocationId)
                .ToList();

            return new AdminData(item.FirstName, item.LastName, item.UserName, item.Password, item.Email, locationIds);
        }

        public static CustomerData ToCustomer(DbCustomer item) {
            item.NullCheck(nameof(item));
            return new CustomerData(item.FirstName, item.LastName, item.UserName, item.Password, item.Email, item.PhoneNumber, item.AddressId, item.BirthDate, item.SignUpDate, item.DefaultLocationId);
        }

        public static InventoryEntryData ToInventoryEntry(DbInventoryEntry item) {
            item.NullCheck(nameof(item));
            return new InventoryEntryData(item.ProductId, item.LocationId, item.Price, item.Discount);
        }

        public static LocationData ToLocation(DbLocation item) {
            item.NullCheck(nameof(item));
            var orderIds = item.Orders
                .Select(o => o.Id)
                .ToList();

            var adminIds = item.LocationAdmins
                .Select(la => la.AdminId)
                .ToList();

            var inventoryEntryIds = item.InventoryEntries
                .Select(ie => ie.Id)
                .ToList();

            return new LocationData(item.Name, item.AddressId, orderIds, adminIds, inventoryEntryIds);
        }

        public static OrderData ToOrder(DbOrder item) {
            item.NullCheck(nameof(item));
            var orderLineIds = item.OrderLines
                .Select(ol => ol.Id)
                .ToList();

            return new OrderData(item.CustomerId, item.AdminId, item.LocationId, item.PlacementDate, orderLineIds);
        }

        public static OrderLineData ToOrderLine(DbOrderLine item) {
            item.NullCheck(nameof(item));
            return new OrderLineData(item.OrderId, item.ProductId, item.Count, item.PricePerUnit, item.Discount);
        }

        public static ProductData ToProduct(DbProduct item) {
            item.NullCheck(nameof(item));
            return new ProductData(item.Name);
        }

        public static Tuple<LocationData, AdminData> ToLocationAndAdmin(DbLocationAdmin dbLocationAdmin) {
            dbLocationAdmin.NullCheck(nameof(dbLocationAdmin));

            var location = ToLocation(dbLocationAdmin.Location);
            var admin = ToAdmin(dbLocationAdmin.Admin);

            return new Tuple<LocationData, AdminData>(location, admin);
        }

        #endregion
    }
}
