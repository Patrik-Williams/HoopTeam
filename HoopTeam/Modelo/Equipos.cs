using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    public class Equipos
    {
        //campos del ojeto equipo
        public int idEquipo { get; set; }
        public string categoria { get; set; }
        public string genero { get; set; }
        public int cedEntrenador { get; set; }
        public string Entrenador { get; set;}
        public int cupo { get; set; }
    }
}
