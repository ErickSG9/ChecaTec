﻿using System;
using SQLite;

namespace Test.Models
{
    public class ChatResumen
    {
        public int IdEmisor { get; set; }
        public int IdReceptor { get; set; }        // ID del otro usuario
        public string Nombre { get; set; }         // Nombre del usuario con quien hablas
        public string UltimoMensaje { get; set; }  // Último mensaje enviado o recibido
        public DateTime FechaHora { get; set; } // NUEVO: usar para ordenar correctamente
        public string Hora => FechaHora.ToString("HH:mm");
        public string Foto { get; set; }           // Ruta de imagen o avatar (opcional)
    }
}