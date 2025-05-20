using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Test.Models;

namespace Test.Data
{
    public static class Database
    {
        public static void InitializeDatabase()
        {
            var db = GetConnection();
            db.CreateTable<Usuario>();
            db.CreateTable<Pacientes>();
            db.CreateTable<Medico>();
            db.CreateTable<Receta>();
            db.CreateTable<HistorialClinico>();
            db.CreateTable<Chat>(); 
            db.CreateTable<Consulta>();
            db.CreateTable<Operacion>();
        }
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

        public static void RegistrarUsuario(string nombre, string apellidos, int edad, string telefono,string genero, string email, string contrasena, string rol)
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
                Genero = genero,
                Email = email,
                Contrasena = contrasena, 
                Rol = rol,
                FechaRegistro = DateTime.Now
            };

            db.Insert(nuevoUsuario);
        }
        public static Usuario ValidarUsuario(string email, string contrasena)
        {
            var db = GetConnection();
            return db.Table<Usuario>().FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);
        }
        public static void RegistrarMedico(int idUsuario, string especialidad, string horario, string clinica)
        {
            var db = GetConnection();

            var nuevoMedico = new Medico
            {
                IdUsuario = idUsuario,
                Especialidad = especialidad,
                HorarioAtencion = horario,
                Clinica = clinica,
            };

            db.Insert(nuevoMedico);
        }
        public static Medico GetMedicoPorUsuarioId(int idUsuario)
        {
            var db = GetConnection();
            return db.Table<Medico>().FirstOrDefault(m => m.IdUsuario == idUsuario);
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
        public static Pacientes GetPacientePorId(int id)
        {
            var db = GetConnection();
            return db.Table<Pacientes>().FirstOrDefault(u => u.IdPaciente == id);
        }
        public static List<Operacion> ObtenerOperacionesPorPaciente(int idPaciente)
        {
            var db = GetConnection();
            return db.Table<Operacion>().Where(o => o.IdUsuario == idPaciente).ToList();
        }
        public static List<Receta> ObtenerRecetasPorPaciente(int idPaciente)
        {
            var db = GetConnection();
            return db.Table<Receta>()
                     .Where(r => r.IdPaciente == idPaciente)
                     .ToList();
        }
        public static void InsertarOperacion(Operacion operacion)
        {
            var db = GetConnection();
            db.Insert(operacion);
        }
        public static void InsertarConsulta(Consulta consulta)
        {
            var db = GetConnection();
            db.Insert(consulta);
        }
        public static void InsertarReceta(Receta receta) 
        { 
            var db = GetConnection();
            db.Insert(receta);
        }
        public static List<Consulta> ObtenerConsultasPorPaciente(int idPaciente)
        {
            var db = GetConnection();
            return db.Table<Consulta>().Where(c => c.IdUsuario == idPaciente).ToList();
        }
        public static void RegistrarPaciente(int idUsuario,DateTime fechaNacimiento,string numeroSeguro,double peso,double altura,string alergias,string antecedentes,string medicamentos,string vacunas,string discapacidad, string nomE, string parE, string telE)
        {
            var db = GetConnection();

            var nuevoPaciente = new Pacientes
            {
                IdUsuario = idUsuario,
                FechaNacimiento = fechaNacimiento,
                NumeroSeguro = numeroSeguro,
                Peso = peso,
                Altura = altura,
                Alergias = alergias,
                AntecedentesClinicos = antecedentes,
                Medicamentos = medicamentos,
                Vacunas = vacunas,
                Discapacidad = discapacidad,
                NombreE = nomE,
                ParentescoE = parE,
                TelefonoE = telE,
            };

            db.Insert(nuevoPaciente);
        }

    }
}