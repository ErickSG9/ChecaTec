using SQLite;

namespace Test.Models
{
    [Table("HistorialClinico")]
    public class HistorialClinico
    {
        [PrimaryKey, AutoIncrement]
        public int IdHistorial { get; set; }

        public int IdPaciente { get; set; } 
        public int IdProfesional { get; set; } 
        public string FechaRegistro { get; set; }
        public string Notas { get; set; }
        public string Diagnostico { get; set; }

        // Claves foráneas (para acceder a las relaciones en consultas)
        [Ignore]
        public Pacientes Paciente { get; set; }

        [Ignore]
        public Usuario Profesional { get; set; }
    }
}