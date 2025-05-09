using SQLite;
using System;

namespace Test.Models
{
    [Table("Consulta")]
    public class Consulta
    {
        [PrimaryKey, AutoIncrement]
        public int IdConsulta { get; set; }
        public int IdUsuario { get; set; } 
        public int IdDoctor { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }
        public string PresionArterial { get; set; } 
        public int FrecuenciaCardiaca { get; set; } 
        public double Temperatura { get; set; }
        public string Observaciones { get; set; }
        [Ignore]
        public string NombreDoctor { get; set; }
    }
}
