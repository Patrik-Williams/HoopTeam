using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    public class Agenda
    {
        //campos del objeto agenda
        //no estatico, permite el collectionview
        public string idAgenda {get; set; }
        public string Equipo { get; set; }
        public int idEquipo { get; set; }
        public string Cancha { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; }
    }
}
