using System;
using SQLite;

namespace Test.Models
{
    [Table("Pacientes")]
    public class Pacientes
    {
        [PrimaryKey, AutoIncrement]
        public int IdPaciente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroSeguro { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Alergias { get; set; }
        public string AntecedentesClinicos { get; set; }
        public string Medicamentos { get; set; }
        public string Vacunas { get; set; }
        public string Discapacidad { get; set; }
        public string NombreE { get; set; }
        public string ParentescoE { get; set; }
        public string TelefonoE { get; set; }
        [Ignore]
        public Usuario Usuario { get; set; }
    }
}