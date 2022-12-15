using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class EquiposEntrenadorViewModel
    {
        //referencia al entrenador
        Entrenador ent = new Entrenador();

        //lista de tipo equipos
        public List<Equipos> EquiposEntrenador { get; set; }

        //llena la lista con los equipos que tiene el entrenador
        public EquiposEntrenadorViewModel()
        {
            EquiposEntrenador = new EquiposService().GetEquipo(ent.getCedula());
        }
    }
}
