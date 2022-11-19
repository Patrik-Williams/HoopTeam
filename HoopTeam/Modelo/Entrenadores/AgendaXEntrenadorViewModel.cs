using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    class AgendaXEntrenadorViewModel
    {
        Entrenador ent = new Entrenador();

        public List<EstEntrenador> AgendaPorEntrenador { get; set; }

        public AgendaXEntrenadorViewModel()
        {
            AgendaPorEntrenador = new EstudianteService().GetEstEntrenadores(ent.getCedula());
        }
    }
}
