using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo
{
    class AgendaPorEntrenadorViewModel
    {
        Entrenador ent = new Entrenador();

        public List<Agenda> AgendaPorEntrenador { get; set; }

        public AgendaPorEntrenadorViewModel()
        {
            AgendaPorEntrenador = new AgendaService().GetAgEntrenador(ent.getCedula());
        }
    }
}
