
using CustompgAdmin.DataAccess.Context;
using CustompgAdmin.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.DataAccess.Repositories.Connection;

public class ConnectionRepository  : IConnectionRepository
{
    private readonly IConfiguration _config;
    public ConnectionRepository(IConfiguration config)
    {
        _config = config;
    }
    public void CreateMigrateUpdateDatabase(ConnectionDB connectionDB)
    {
        //string connectionString = "Host =localhost;Port=5432;Username=postgres;Password=postgres;Database=edu_data";
       string connectionString = $"Host={connectionDB.Host};Port={connectionDB.Port};Username={connectionDB.Username};Password={connectionDB.Password};Database ={connectionDB.Database}";
        // PostgreSQL serveriga bog'lanish
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            // Ma'lumotlar bazasini yaratish
            using (NpgsqlCommand command = new NpgsqlCommand($"CREATE TABLE users(id SERIAL PRIMARY KEY)", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // Environment o'zgaruvchisidan ma'lumotlar bazasiga ulanish uchun connectionString olish
        //string dbConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

        // DbContext bilan ishlash uchun Options sozlash
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString).Options;

        // DbContext ni yaratish
        using (var context = new AppDbContext(dbOptions))
        {
            // Migrationlarni qo'llash va yangilash
            context.Database.Migrate();

            ////Console.WriteLine("Ma'lumotlar bazasi muvaffaqiyatli yaratildi, migrate qilindi va yangilandi.");
        }

    }
}
