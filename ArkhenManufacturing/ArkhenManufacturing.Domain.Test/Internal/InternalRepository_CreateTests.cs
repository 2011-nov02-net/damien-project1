using System;

using ArkhenManufacturing.Domain.Internal;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

using Xunit;

namespace ArkhenManufacturing.Domain.Test.Internal
{
    public class InternalRepository_CreateTests
    {
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

        //[Theory]
        //[MemberData(nameof(ValidLocationTestData))]
        //public void Create_Valid_LocationData(string name, Guid addressId, List<Guid> orderIds, List<Guid> adminIds, List<Guid> inventoryEntryIds) {
        //    // Arrange
        //    var data = new LocationData(name, addressId, orderIds, adminIds, inventoryEntryIds);

        //    // Act
        //    _repository.Create<Location>(data);

        //    // Assert
        //    Assert.True(_repository.Count<Location>() > 0);

        //}

        //[Theory]
        //[MemberData(nameof(InvalidLocationTestData))]
        //public void Create_Invalid_LocationData(string name, Guid addressId, List<Guid> orderIds, List<Guid> adminIds, List<Guid> inventoryEntryIds) {
        //    // Arrange
        //    var data = new LocationData(name, addressId, orderIds, adminIds, inventoryEntryIds);

        //    // Act and Assert
        //    Assert.Throws<ArgumentException>(() => _repository.Create<Location>(data));
        //    Assert.True(_repository.Count<Location>() == 0);
        //}

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

        //[Theory]
        //[MemberData(nameof(ValidCustomerTestData))]
        //public void Create_Valid_CustomerData(string firstName, string lastName, string email, string phoneNumber, Guid addressId, DateTime signUpDate, DateTime birthDate, Guid? locationId) {
        //    // Arrange
        //    var data = new CustomerData(firstName, lastName, email, phoneNumber, addressId, signUpDate, birthDate, locationId);

        //    // Act
        //    _repository.Create<Customer>(data);

        //    // Assert
        //    Assert.True(_repository.Count<Customer>() > 0);
        //}
        
        //[Theory]
        //[MemberData(nameof(InvalidCustomerTestData))]
        //public void Create_Invalid_CustomerData(string firstName, string lastName, string email, string phoneNumber, Guid addressId, DateTime signUpDate, DateTime birthDate, Guid? locationId) {
        //    // Arrange
        //    var data = new CustomerData(firstName, lastName, email, phoneNumber, addressId, signUpDate, birthDate, locationId);

        //    // Act and Assert
        //    Assert.Throws<ArgumentException>(() => _repository.Create<Customer>(data));
        //    Assert.True(_repository.Count<Customer>() == 0);
        //}

        #endregion

        #region Admin Creation Tests

        [Fact]
        public void Create_Null_AdminData() {
            // Assign
            AdminData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Admin>(data));
            Assert.True(_repository.Count<Admin>() == 0);
        }

        //[Theory]
        //[MemberData(nameof(ValidAdminTestData))]
        //public void Create_Valid_AdminData(string firstName, string lastName, string email, Guid locationId) {
        //    var data = new AdminData(firstName, lastName, email, locationId);
        //    _repository.Create<Admin>(data);

        //    Assert.True(_repository.Count<Admin>() == 1);
        //}

        //[Theory]
        //[MemberData(nameof(InvalidAdminTestData))]
        //public void Create_Invalid_AdminData(string firstName, string lastName, string email, Guid locationId) {
        //    var data = new AdminData(firstName, lastName, email, locationId);

        //    Assert.Throws<ArgumentException>(() => _repository.Create<Admin>(data));
        //    Assert.True(_repository.Count<Admin>() == 0);
        //}

        #endregion

        #region Order Creation Tests

        [Fact]
        public void Create_Null_OrderData() {
            // Assign
            OrderData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Order>(data));
            Assert.True(_repository.Count<Order>() == 0);
        }

        //[Theory]
        //[MemberData(nameof(ValidOrderTestData))]
        //public void Create_Valid_OrderData(Guid customerId, Guid adminId, Guid locationId, DateTime placementDate, List<Guid> orderLineIds) {
        //    // Arrange
        //    var data = new OrderData(customerId, adminId, locationId, placementDate, orderLineIds);

        //    // Act
        //    _repository.Create<Order>(data);

        //    // Assert
        //    Assert.True(_repository.Count<Order>() == 1);
        //}

        //[Theory]
        //[MemberData(nameof(InvalidOrderTestData))]
        //public void Create_Invalid_OrderData(Guid customerId, Guid adminId, Guid locationId, DateTime placementDate, List<Guid> orderLineIds) {
        //    // Arrange
        //    var data = new OrderData(customerId, adminId, locationId, placementDate, orderLineIds);

        //    // Act and Assert
        //    Assert.Throws<ArgumentException>(() => _repository.Create<Order>(data));
        //    Assert.True(_repository.Count<Order>() == 1);
        //}

        #endregion

        #region OrderLine Creation Tests

        [Fact]
        public void Create_Null_OrderLineData() {
            // Assign
            OrderLineData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<OrderLine>(data));
            Assert.True(_repository.Count<OrderLine>() == 0);
        }

        //[Fact]
        //public void Create_Valid_OrderLineData() {
        //    _repository.Create<OrderLine>(null);
        //}
        
        //[Fact]
        //public void Create_Invalid_OrderLineData() {
        //    _repository.Create<OrderLine>(null);
        //}

        #endregion

        #region InventoryEntry Creation Tests

        [Fact]
        public void Create_Null_InventoryEntryData() {
            // Assign
            InventoryEntryData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<InventoryEntry>(data));
            Assert.True(_repository.Count<InventoryEntry>() == 0);
        }

        //[Fact]
        //public void Create_Valid_InventoryEntryData() {
        //    _repository.Create<InventoryEntry>(null);
        //}

        //[Fact]
        //public void Create_Invalid_InventoryEntryData() {
        //    _repository.Create<InventoryEntry>(null);
        //}

        #endregion

        #region Product Creation Tests

        [Fact]
        public void Create_Null_ProductData() {
            // Assign
            ProductData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Product>(data));
            Assert.True(_repository.Count<Product>() == 0);
        }

        //[Theory]
        //[MemberData(nameof(ValidProductTestData))]
        //public void Create_Valid_ProductData(string productName) {
        //    var data = new ProductData(productName);
        //    _repository.Create<Product>(data);
        //    Assert.True(_repository.Count<Product>() > 0);
        //}
        
        //[Theory]
        //[MemberData(nameof(InvalidProductTestData))]
        //public void Create_Invalid_ProductData(string productName) {
        //    // No arranging needed, as that's what is being asserted
        //    Assert.Throws<ArgumentException>(() => new ProductData(productName));
        //}

        #endregion

        #region Address Creation Tests

        [Fact]
        public void Create_Null_AddressData() {
            // Assign
            AddressData data = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _repository.Create<Address>(data));
            Assert.True(_repository.Count<Address>() == 0);
        }

        //[Fact]
        //public void Create_Valid_AddressData() {
        //    _repository.Create<Address>(null);
        //}
        
        //[Fact]
        //public void Create_Invalid_AddressData() {
        //    _repository.Create<Address>(null);
        //}

        #endregion
    }
}
