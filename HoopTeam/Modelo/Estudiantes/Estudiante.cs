using System;

namespace HoopTeam.Modelo
{
    class Estudiante
    {
        //campos del objeto estudiante 
        //este no es estatico, permite el collection view 
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }

    }
}
