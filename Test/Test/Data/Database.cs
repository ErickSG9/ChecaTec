using SQLite;
using System;
using System.IO;
using Test.Models;

namespace Test.Data
{
    public static class Database
    {
        private static SQLiteConnection _database;

        public static SQLiteConnection GetConnection()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test.db3");
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
        public static void RegistrarUsuario(string nombre, string apellidos, int edad, double telefono, string email, string contrasena, string rol, string nomE, string parE, double telE)
        {
            var db = GetConnection();

            // Verificar si el usuario ya existe
            var usuarioExistente = db.Table<Usuario>().FirstOrDefault(u => u.Email == email);
            if (usuarioExistente != null)
            {
                throw new Exception("El correo ya está registrado.");
            }

            // Insertar el usuario en la base de datos
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Apellidos = apellidos,
                Edad = edad,
                Telefono = telefono,    
                Email = email,
                Contrasena = contrasena, 
                Rol = rol,
                NombreE = nomE,
                ParentescoE = parE,
                TelefonoE = telE,
                FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            db.Insert(nuevoUsuario);
        }
        public static Usuario ValidarUsuario(string email, string contrasena)
        {
            var db = GetConnection();
            return db.Table<Usuario>().FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);
        }
    }
}