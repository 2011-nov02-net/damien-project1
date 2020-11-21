using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Repository.InternalRepository;

namespace ArkhenManufacturing.Library.Test.Library.Ephemeral
{
    public class InternalRepositoryCreateTests
    {
        private readonly IRepository _repository;

        public InternalRepositoryCreateTests() {
            _repository = new InternalRepository();
        }

        #region Location Creation Tests

        public void CreateOne_ValidLocation() {
            // Asset.IsAssignableFrom<T>(object);

            _repository.Create<Location>(null);
        }

        public void CreateOne_InvalidLocation() {
            // Use this as one would use First()
            //  dbcontext.DbSet.Single();
            _repository.Create<Location>(null);
        }

        #endregion

        #region Customer Creation Tests

        public void Customer_Create_Succeeds() {
            _repository.Create<Customer>(null);
        }

        public void Customer_Create_Fails() {
            _repository.Create<Customer>(null);
        }

        #endregion

        #region Admin Creation Tests

        public void Admin_Create_Succeeds() {
            _repository.Create<Admin>(null);
        }

        public void Admin_Create_Fails() {
            _repository.Create<Admin>(null);
        }

        #endregion

        #region Order Creation Tests

        public void Order_Create_Succeeds() {
            _repository.Create<Order>(null);
        }

        public void Order_Create_Fails() {
            _repository.Create<Order>(null);
        }

        #endregion

        #region Product Creation Tests

        public void Product_Create_Succeeds() {
            _repository.Create<Product>(null);
        }

        public void Product_Create_Fails() {
            _repository.Create<Product>(null);
        }

        #endregion

        #region InventoryEntry Creation Tests

        public void InventoryEntry_Create_Succeeds() {
            _repository.Create<InventoryEntry>(null);
        }

        public void InventoryEntry_Create_Fails() {
            _repository.Create<InventoryEntry>(null);
        }

        #endregion

        #region Address Creation Tests

        public void Address_Create_Succeeds() {
            _repository.Create<Address>(null);
        }

        public void Address_Create_Fails() {
            _repository.Create<Address>(null);
        }

        #endregion
    }
}
