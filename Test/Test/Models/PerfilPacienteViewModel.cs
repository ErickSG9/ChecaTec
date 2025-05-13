using System;

public class PerfilPacienteViewModel
{
    // Datos de la tabla Usuario
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public string Contrasena { get; set; }
    public string Rol { get; set; }
    public DateTime FechaRegistro { get; set; }

    // Datos de la tabla Paciente
    public string Genero { get; set; }
    public int Edad { get; set; }
    public string Telefono { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public string Alergias { get; set; }
    public string AntecedentesClinicos { get; set; }
    public string Medicamentos { get; set; }
    public string Vacunas { get; set; }
    public string Discapacidad { get; set; }

    // Datos de emergencia
    public string NombreE { get; set; }
    public string ParentescoE { get; set; }
    public string TelefonoE { get; set; }
}
