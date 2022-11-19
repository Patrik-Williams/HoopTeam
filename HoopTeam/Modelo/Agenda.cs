using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    public class Agenda
    {
        public int idAgenda {get; set; }
        public string Equipo { get; set; }
        public int Cancha { get; set; }
        public string Ubicacion { get; set; }
        public string FechaHora { get; set; }
        public string Descripcion { get; set; }
    }
}
