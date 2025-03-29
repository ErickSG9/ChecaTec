using SQLite;

namespace Test.Models
{
    [Table("HistorialClinico")]
    public class HistorialClinico
    {
        [PrimaryKey, AutoIncrement]
        public int IdHistorial { get; set; }

        public int IdPaciente { get; set; } // Relación con Pacientes
        public int IdProfesional { get; set; } // Relación con Usuarios (Médico)
        public string FechaRegistro { get; set; }
        public string Notas { get; set; }
        public string Diagnostico { get; set; }

        // Claves foráneas (para acceder a las relaciones en consultas)
        [Ignore]
        public Paciente Paciente { get; set; }

        [Ignore]
        public Usuario Profesional { get; set; }
    }
}