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

        #region To DbEntity (id, data)

        public static DbAddress ToDbAddress(Guid id, AddressData data) {
            if(data is null) {
                return null;
            }

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
            if (data is null) {
                return null;
            }

            return new DbAdmin
            {
                Id = id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email
            };

        }

        public static DbCustomer ToDbCustomer(Guid id, CustomerData data) {
            if (data is null) {
                return null;
            }

            return new DbCustomer
            {
                Id = id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                BirthDate = data.BirthDate,
                AddressId = data.AddressId,
                DefaultLocationId = data.DefaultLocationId
            };
        }

        public static DbInventoryEntry ToDbInventoryEntry(Guid id, InventoryEntryData data) {
            if (data is null) {
                return null;
            }

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
            if (data is null) {
                return null;
            }

            return new DbLocation
            {
                Id = id,
                AddressId = data.AddressId
            };
        }

        public static DbOrder ToDbOrder(Guid id, OrderData data) {
            if (data is null) {
                return null;
            }

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
            if (data is null) {
                return null;
            }

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
            if (data is null) {
                return null;
            }

            return new DbProduct
            {
                Id = id,
                Name = data.Name
            };
        }

        #endregion

        #region To DbEntity (item)

        public static DbAddress ToDbAddress(Address item) {
            if (item is null) {
                return null;
            }

            return ToDbAddress(item.Id, item.GetData() as AddressData);
        }

        public static DbAdmin ToDbAdmin(Admin item) {
            if (item is null) {
                return null;
            }

            return ToDbAdmin(item.Id, item.GetData() as AdminData);
        }

        public static DbCustomer ToDbCustomer(Customer item) {
            if (item is null) {
                return null;
            }

            return ToDbCustomer(item.Id, item.GetData() as CustomerData);
        }

        public static DbInventoryEntry ToDbInventoryEntry(InventoryEntry item) {
            if (item is null) {
                return null;
            }

            return ToDbInventoryEntry(item.Id, item.GetData() as InventoryEntryData);
        }

        public static DbLocation ToDbLocation(Location item) {
            if (item is null) {
                return null;
            }

            return ToDbLocation(item.Id, item.GetData() as LocationData);
        }

        public static DbOrder ToDbOrder(Order item) {
            if (item is null) {
                return null;
            }

            return ToDbOrder(item.Id, item.GetData() as OrderData);
        }

        public static DbOrderLine ToDbOrderLine(OrderLine item) {
            if (item is null) {
                return null;
            }

            return ToDbOrderLine(item.Id, item.GetData() as OrderLineData);
        }

        public static DbProduct ToDbProduct(Product item) {
            if (item is null) {
                return null;
            }

            return ToDbProduct(item.Id, item.GetData() as ProductData);
        }

        #endregion

        #endregion

        #region To Library Entity

        public static Address ToAddress(DbAddress item) {
            if (item is null) {
                return null;
            }

            var data = new AddressData(item.Line1, item.Line2,
                item.City, item.State, item.Country, item.ZipCode);
            return new Address(item.Id, data);
        }

        public static Admin ToAdmin(DbAdmin item) {
            if (item is null) {
                return null;
            }

            var data = new AdminData(
                item.FirstName, item.LastName, 
                item.Email, item.LocationId);

            return new Admin(item.Id, data);
        }

        public static Customer ToCustomer(DbCustomer item) {
            if (item is null) {
                return null;
            }

            var data = new CustomerData(
                item.FirstName, item.LastName, item.Email, 
                item.PhoneNumber, item.AddressId, 
                item.BirthDate, item.SignUpDate, item.DefaultLocationId);

            return new Customer(item.Id, data);
        }

        public static InventoryEntry ToInventoryEntry(DbInventoryEntry item) {
            if (item is null) {
                return null;
            }

            var data = new InventoryEntryData(
                item.ProductId, item.LocationId, item.Price, 
                item.Discount, item.Count, item.Threshold);

            return new InventoryEntry(item.Id, data);
        }

        public static Location ToLocation(DbLocation item) {
            if (item is null) {
                return null;
            }

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
            if (item is null) {
                return null;
            }

            var orderLineIds = item.OrderLines
                .Select(ol => ol.Id)
                .ToList();

            var data = new OrderData(
                item.CustomerId, item.AdminId, item.LocationId, 
                item.PlacementDate, orderLineIds);

            return new Order(item.Id, data);
        }

        public static OrderLine ToOrderLine(DbOrderLine item) {
            if (item is null) {
                return null;
            }

            var data = new OrderLineData(
                item.OrderId, item.ProductId, item.Count, 
                item.PricePerUnit, item.Discount);

            return new OrderLine(item.Id, data);
        }

        public static Product ToProduct(DbProduct item) {
            if (item is null) {
                return null;
            }

            var data = new ProductData(item.Name);

            return new Product(item.Id, data);
        }

        #endregion
    }
}
