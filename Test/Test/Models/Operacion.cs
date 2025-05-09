using SQLite;
using System;

namespace Test.Models
{
    [Table("Operacion")]
    public class Operacion
    {
        [PrimaryKey, AutoIncrement]
        public int IdOperacion { get; set; }

        public int IdUsuario { get; set; } 

        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }

        public string Cirujano { get; set; }

        public string Hora { get; set; }

        public string Quirofano { get; set; }

        public string Estado { get; set; }

        public string Observaciones { get; set; }
    }
}
