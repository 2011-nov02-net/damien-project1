using System;
using System.Collections.Generic;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Repository.InternalRepository;

using Xunit;

namespace ArkhenManufacturing.Library.Test.Library.Internal
{
    public class InternalRepository_CreateTests
    {
        #region MemberData

        /* Location:
         *  Name,
         *  AddressId,
         *  OrderIds,
         *  AdminIds,
         *  InventoryEntryIds
         */

        /* Customer:
         *  FirstName,
         *  LastName,
         *  UserName,
         *  Password,
         *  Email,
         *  
         *  PhoneNumber,
         *  AddressId,
         *  SignUpDate,
         *  BirthDate,
         *  DefaultLocationId
         */

        /* Admin:
         *  FirstName,
         *  LastName,
         *  UserName,
         *  Password,
         *  Email,
         *  
         *  LocationIdsAssignedTo
         */

        /* Order:
         *  CustomerId,
         *  AdminId,
         *  LocationId,
         *  PlacementDate,
         *  OrderLineIds
         */

        /* OrderLine:
         *  OrderId,
         *  ProductId,
         *  Count,
         *  PricePerUnit,
         *  Discount
         */

        /* InventoryEntry:
         *  ProductId,
         *  LocationId,
         *  Price,
         *  Discount
         */

        /* Product:
         *  Name
         */

        /* Address:
         *  Line1,
         *  Line2,
         *  City,
         *  State,
         *  Country,
         *  ZipCode
         */

        #region Valid Data

        public static IEnumerable<object[]> ValidLocationTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> ValidAdminTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> ValidCustomerTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> ValidOrderTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> ValidOrderLineTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> ValidProductTestData =>
            new List<object[]>
            {
                new object[] { "Whole Bag of Coffee Beans" },
                new object[] { "Garlic cloves" },
                new object[] { "Ankh" }
            };

        public static IEnumerable<object[]> ValidInventoryEntryTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> ValidAddressTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        #endregion

        #region Invalid Data

        public static IEnumerable<object[]> InvalidLocationTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> InvalidAdminTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> InvalidCustomerTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> InvalidOrderTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> InvalidOrderLineTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> InvalidProductTestData =>
            new List<object[]>
            {
                new object[] { null },
                new object[] { "" }
            };

        public static IEnumerable<object[]> InvalidInventoryEntryTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        public static IEnumerable<object[]> InvalidAddressTestData =>
            new List<object[]>
            {
                new object[] { },
                new object[] { },
                new object[] { }
            };

        #endregion

        #endregion

        private readonly IRepository _repository;

        public InternalRepository_CreateTests() {
            _repository = new InternalRepository();
        }

        #region Location Creation Tests

        [Fact]
        public void Create_Null_LocationData() {
            // Assign
            LocationData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Location>(data));
            Assert.True(_repository.Count<Location>() == 0);
        }

        [Theory]
        [MemberData(nameof(ValidLocationTestData))]
        public void Create_Valid_LocationData(string name, Guid addressId, List<Guid> orderIds, List<Guid> adminIds, List<Guid> inventoryEntryIds) {
            // Arrange
            var data = new LocationData(name, addressId, orderIds, adminIds, inventoryEntryIds);

            // Act
            _repository.Create<Location>(data);

            // Assert
            Assert.True(_repository.Count<Location>() > 0);

        }

        [Theory]
        [MemberData(nameof(InvalidLocationTestData))]
        public void Create_Invalid_LocationData(string name, Guid addressId, List<Guid> orderIds, List<Guid> adminIds, List<Guid> inventoryEntryIds) {
            // Arrange
            var data = new LocationData(name, addressId, orderIds, adminIds, inventoryEntryIds);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Location>(data));
            Assert.True(_repository.Count<Location>() == 0);
        }

        #endregion

        #region Customer Creation Tests

        [Fact]
        public void Create_Null_CustomerData() {
            // Assign
            CustomerData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Customer>(data));
            Assert.True(_repository.Count<Customer>() == 0);
        }

        [Theory]
        [MemberData(nameof(ValidCustomerTestData))]
        public void Create_Valid_CustomerData(string firstName, string lastName, string userName, string password, string email, string phoneNumber, Guid addressId, DateTime signUpDate, DateTime birthDate, Guid? locationId) {
            // Arrange
            var data = new CustomerData(firstName, lastName, userName, password, email, phoneNumber, addressId, signUpDate, birthDate, locationId);

            // Act
            _repository.Create<Customer>(data);

            // Assert
            Assert.True(_repository.Count<Customer>() > 0);
        }
        
        [Theory]
        [MemberData(nameof(InvalidCustomerTestData))]
        public void Create_Invalid_CustomerData(string firstName, string lastName, string userName, string password, string email, string phoneNumber, Guid addressId, DateTime signUpDate, DateTime birthDate, Guid? locationId) {
            // Arrange
            var data = new CustomerData(firstName, lastName, userName, password, email, phoneNumber, addressId, signUpDate, birthDate, locationId);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Customer>(data));
            Assert.True(_repository.Count<Customer>() == 0);
        }

        #endregion

        #region Admin Creation Tests

        [Fact]
        public void Create_Null_AdminData() {
            // Assign
            AdminData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Admin>(data));
        }

        [Fact]
        public void Create_Valid_AdminData() {
            _repository.Create<Admin>(null);
        }
        
        [Fact]
        public void Create_Invalid_AdminData() {
            _repository.Create<Admin>(null);
        }

        #endregion

        #region Order Creation Tests

        [Fact]
        public void Create_Null_OrderData() {
            // Assign
            OrderData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Order>(data));
        }

        [Fact]
        public void Create_Valid_OrderData() {
            _repository.Create<Order>(null);
        }
        
        [Fact]
        public void Create_Invalid_OrderData() {
            _repository.Create<Order>(null);
        }

        #endregion

        #region OrderLine Creation Tests

        [Fact]
        public void Create_Null_OrderLineData() {
            // Assign
            OrderLineData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<OrderLine>(data));
        }

        [Fact]
        public void Create_Valid_OrderLineData() {
            _repository.Create<OrderLine>(null);
        }
        
        [Fact]
        public void Create_Invalid_OrderLineData() {
            _repository.Create<OrderLine>(null);
        }

        #endregion

        #region InventoryEntry Creation Tests

        [Fact]
        public void Create_Null_InventoryEntryData() {
            // Assign
            InventoryEntryData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<InventoryEntry>(data));
        }

        [Fact]
        public void Create_Valid_InventoryEntryData() {
            _repository.Create<InventoryEntry>(null);
        }

        [Fact]
        public void Create_Invalid_InventoryEntryData() {
            _repository.Create<InventoryEntry>(null);
        }

        #endregion

        #region Product Creation Tests

        [Fact]
        public void Create_Null_ProductData() {
            // Assign
            ProductData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Product>(data));
        }

        [Theory]
        [MemberData(nameof(ValidProductTestData))]
        public void Create_Valid_ProductData(string productName) {
            var data = new ProductData(productName);
            _repository.Create<Product>(data);
            Assert.True(_repository.Count<Product>() > 0);
        }
        
        [Theory]
        [MemberData(nameof(InvalidProductTestData))]
        public void Create_Invalid_ProductData(string productName) {
            // No arranging needed, as that's what is being asserted
            Assert.Throws<ArgumentException>(() => new ProductData(productName));
        }

        #endregion

        #region Address Creation Tests

        [Fact]
        public void Create_Null_AddressData() {
            // Assign
            AddressData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Address>(data));
        }

        [Fact]
        public void Create_Valid_AddressData() {
            _repository.Create<Address>(null);
        }
        
        [Fact]
        public void Create_Invalid_AddressData() {
            _repository.Create<Address>(null);
        }

        #endregion
    }
}
