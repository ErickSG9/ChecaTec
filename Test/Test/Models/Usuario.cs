using System;
using SQLite;

namespace Test.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public string Especialidad { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}