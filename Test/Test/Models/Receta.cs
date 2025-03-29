using SQLite;

namespace Test.Models
{
    [Table("Recetas")]
    public class Receta
    {
        [PrimaryKey, AutoIncrement]
        public int IdReceta { get; set; }
        public int IdPaciente { get; set; } // Relación con Pacientes
        public int IdProfesional { get; set; } // Relación con Usuarios (Médico)
        public string FechaEmision { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Instrucciones { get; set; }
        public bool Activa { get; set; } // 1: Activa, 0: Inactiva

        // Claves foráneas (solo para referencia en código, no en SQLite)
        [Ignore]
        public Paciente Paciente { get; set; }

        [Ignore]
        public Usuario Profesional { get; set; }
    }
}
    