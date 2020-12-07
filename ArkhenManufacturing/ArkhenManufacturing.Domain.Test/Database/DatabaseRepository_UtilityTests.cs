using System;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Domain.Database;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ArkhenManufacturing.Domain.Test.Database
{
    public class DatabaseRepository_UtilityTests
    {
        private readonly IRepository _repository;

        public DatabaseRepository_UtilityTests() {
            using var connection = new SqliteConnection("Data Source=:memory:");
            var optionsBuilder = new DbContextOptionsBuilder<ArkhenContext>()
                .UseSqlite(connection);
            _repository = new DatabaseRepository(optionsBuilder.Options);
        }
    }
}
