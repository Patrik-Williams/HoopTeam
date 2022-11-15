using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class EquiposViewModel
    {
        Entrenador ent = new Entrenador();

        public List<Equipos> Equipos { get; set; }

        public EquiposViewModel()
        {
            Equipos = new EquiposService().GetTodosEquipos();
        }
    }
}
