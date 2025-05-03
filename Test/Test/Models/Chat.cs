using System;
using SQLite;

namespace Test.Models
{
    [Table("Chat")]
    public class Chat
    {
        [PrimaryKey, AutoIncrement]
        public int IdMensaje { get; set; }

        public int IdEmisor { get; set; } // Usuario que envía el mensaje
        public int IdReceptor { get; set; } // Usuario que recibe el mensaje
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leido { get; set; } // 0: No leído, 1: Leído

        // Claves foráneas para acceder a los usuarios si es necesario
        [Ignore]
        public Usuario Emisor { get; set; }

        [Ignore]
        public Usuario Receptor { get; set; }
    }
}