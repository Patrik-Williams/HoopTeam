using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class Agenda
    {
        public int idAgenda { get; set; }
        public int idEquipo { get; set; }
        public int idCanchas { get; set; }
        public string fechaHora { get; set; }
        public string descripcion { get; set; }
    }
}
