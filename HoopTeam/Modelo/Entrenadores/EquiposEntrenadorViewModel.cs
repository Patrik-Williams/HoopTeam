using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class EquiposEntrenadorViewModel
    {

        Entrenador ent = new Entrenador();

        public List<Equipos> EquiposEntrenador { get; set; }

        public EquiposEntrenadorViewModel()
        {
            EquiposEntrenador = new EquiposService().GetEquipo(ent.getCedula());
        }
    }
}
