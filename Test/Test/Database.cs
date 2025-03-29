using SQLite;
using System;
using System.IO;
using Test.Models;

namespace Test
{
    public static class Database
    {
        private static SQLiteConnection _database;

        public static SQLiteConnection GetConnection()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test.sql");
                _database = new SQLiteConnection(dbPath);
            }
            return _database;
        }

        public static void InitializeDatabase()
        {
            var db = GetConnection();
            db.CreateTable<Usuario>();
            db.CreateTable<Paciente>();
            db.CreateTable<Receta>();
            db.CreateTable<HistorialClinico>();
            db.CreateTable<Chat>();
        }
    }
}