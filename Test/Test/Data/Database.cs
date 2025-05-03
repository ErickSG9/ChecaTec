using SQLite;
using System;
using System.Collections.Generic;
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
        public static void EnviarMensaje(int idEmisor, int idReceptor, string mensaje)
        {
            var db = GetConnection();

            var nuevoMensaje = new Chat
            {
                IdEmisor = idEmisor,
                IdReceptor = idReceptor,
                Mensaje = mensaje,
                FechaEnvio = DateTime.Now,
                Leido = false
            };

            db.Insert(nuevoMensaje);
        }

        // Obtener mensajes entre dos usuarios
        public static List<Chat> ObtenerConversacion(int idUsuario1, int idUsuario2)
        {
            var db = GetConnection();

            return db.Table<Chat>()
                .Where(c =>
                    (c.IdEmisor == idUsuario1 && c.IdReceptor == idUsuario2) ||
                    (c.IdEmisor == idUsuario2 && c.IdReceptor == idUsuario1))
                .OrderBy(c => c.FechaEnvio)
                .ToList();
        }
        public static void InsertarMensajePrueba(int idEmisor, int idReceptor, string texto)
        {
            var db = GetConnection();

            var mensaje = new Chat
            {
                IdEmisor = idEmisor,
                IdReceptor = idReceptor,
                Mensaje = texto,
                FechaEnvio = DateTime.Now,
                Leido = false
            };

            db.Insert(mensaje);
        }
        public static List<Usuario> GetUsuariosPorRol(string rol)
        {
            var db = GetConnection();
            return db.Table<Usuario>().Where(u => u.Rol == rol).ToList();
        }
        public static List<Chat> GetChatsPorUsuario(int usuarioId)
        {
            var db = GetConnection();
            return db.Table<Chat>().Where(c => c.IdEmisor == usuarioId || c.IdReceptor == usuarioId).ToList();
        }
        public static void GuardarMensaje(int idEmisor, int idReceptor, string mensaje, bool leido)
        {
            var db = GetConnection();
            var nuevoMensaje = new Chat
            {
                IdEmisor = idEmisor,
                IdReceptor = idReceptor,
                Mensaje = mensaje,
                FechaEnvio = DateTime.Now,
                Leido = leido
            };
            db.Insert(nuevoMensaje);
        }
        public static Usuario GetUsuarioPorId(int id)
        {
            var db = GetConnection();
            return db.Table<Usuario>().FirstOrDefault(u => u.IdUsuario == id);
        }
    }
}