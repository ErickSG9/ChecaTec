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
        public double Telefono { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public string NombreE { get; set; }
        public string ParentescoE{ get; set; }
        public double TelefonoE { get; set; }
        public string FechaRegistro { get; set; }
    }
}