using SQLite;

namespace Test.Models
{
    [Table("Medicos")]
    public class Medico
    {
        [PrimaryKey, AutoIncrement]
        public int IdMedico { get; set; }
        public int IdUsuario { get; set; }
        public string Genero { get; set; }
        public string Especialidad { get; set; }
        public string HorarioAtencion { get; set; }
        public string Clinica { get; set; }

    }
}
