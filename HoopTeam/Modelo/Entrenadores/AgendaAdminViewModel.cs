using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo.Entrenadores
{
    class AgendaAdminViewModel
    {
        public List<Agenda> AgendaAdmin { get; set; }

        public AgendaAdminViewModel()
        {
            AgendaAdmin = new AgendaService().GetAg();
        }
    }
}
