using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo.Entrenadores
{
    class AgendaAdminViewModel
    {   

        //crea la lista de agenda que viene desde la base de datos
        public List<Agenda> AgendaAdmin { get; set; }

        //llena la lista con la informacion de la base de datos con una llamada al servicio de la agenda
        public AgendaAdminViewModel()
        {
            AgendaAdmin = new AgendaService().GetAg();
        }
    }
}
