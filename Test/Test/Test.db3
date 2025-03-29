-- Tabla de Usuarios
CREATE TABLE Usuarios (
    IdUsuario INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    Contrasena TEXT NOT NULL,
    Rol TEXT NOT NULL, -- Ej: "Paciente" o "Médico"
    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Tabla de Pacientes
CREATE TABLE Pacientes (
    IdPaciente INTEGER PRIMARY KEY AUTOINCREMENT,
    IdUsuario INTEGER NOT NULL,
    FechaNacimiento DATE NOT NULL,
    NumeroSeguro TEXT,
    Telefono TEXT,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);

-- Tabla de Recetas
CREATE TABLE Recetas (
    IdReceta INTEGER PRIMARY KEY AUTOINCREMENT,
    IdPaciente INTEGER NOT NULL,
    IdProfesional INTEGER NOT NULL, -- Hace referencia a un usuario con rol "Médico"
    FechaEmision DATETIME DEFAULT CURRENT_TIMESTAMP,
    Medicamento TEXT NOT NULL,
    Dosis TEXT NOT NULL,
    Instrucciones TEXT,
    Activa BOOLEAN DEFAULT 1, -- 1: Activa, 0: Inactiva
    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente) ON DELETE CASCADE,
    FOREIGN KEY (IdProfesional) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);

-- Tabla de Historial Clínico
CREATE TABLE HistorialClinico (
    IdHistorial INTEGER PRIMARY KEY AUTOINCREMENT,
    IdPaciente INTEGER NOT NULL,
    IdProfesional INTEGER NOT NULL, -- Hace referencia a un usuario con rol "Médico"
    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP,
    Notas TEXT,
    Diagnostico TEXT,
    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente) ON DELETE CASCADE,
    FOREIGN KEY (IdProfesional) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);

-- Tabla de Chat
CREATE TABLE Chat (
    IdMensaje INTEGER PRIMARY KEY AUTOINCREMENT,
    IdEmisor INTEGER NOT NULL, -- Usuario que envía el mensaje
    IdReceptor INTEGER NOT NULL, -- Usuario que recibe el mensaje
    Mensaje TEXT NOT NULL,
    FechaEnvio DATETIME DEFAULT CURRENT_TIMESTAMP,
    Leido BOOLEAN DEFAULT 0, -- 0: No leído, 1: Leído
    FOREIGN KEY (IdEmisor) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdReceptor) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);