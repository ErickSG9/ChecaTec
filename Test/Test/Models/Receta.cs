using System;
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
        public DateTime FechaEmision { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Instrucciones { get; set; }
        public string Nota { get; set; }
        public bool Activa { get; set; } 

        // Claves foráneas (solo para referencia en código, no en SQLite)
        [Ignore]
        public Pacientes Paciente { get; set; }

        [Ignore]
        public string Profesional { get; set; }
        [Ignore]
        public string EstadoReceta => Activa ? "Activo" : "Completado";
    }
}
    