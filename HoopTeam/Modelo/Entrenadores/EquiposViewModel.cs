using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class EquiposViewModel
    {
        //referencia al entrenador
        Entrenador ent = new Entrenador();

        //lista de tipos equipos
        public List<Equipos> Equipos { get; set; }

        //llena la lista con todos los equipos en la base de datos
        public EquiposViewModel()
        {
            Equipos = new EquiposService().GetTodosEquipos();
        }
    }
}
