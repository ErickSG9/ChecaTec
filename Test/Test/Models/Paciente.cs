using SQLite;

namespace Test.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [PrimaryKey, AutoIncrement]
        public int IdPaciente { get; set; }

        public int IdUsuario { get; set; } // Relación con Usuarios
        public string FechaNacimiento { get; set; }
        public string NumeroSeguro { get; set; }
        public string Telefono { get; set; }

        // Clave foránea (para referencias si las necesitas en consultas avanzadas)
        [Ignore]
        public Usuario Usuario { get; set; }
    }
}