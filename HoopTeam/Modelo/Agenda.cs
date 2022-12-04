using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    public class Agenda
    {
        public string idAgenda {get; set; }
        public string Equipo { get; set; }

        public int idEquipo { get; set; }
        public string Cancha { get; set; }
        public string Ubicacion { get; set; }
        public string FechaHora { get; set; }
        public string Descripcion { get; set; }
    }
}
