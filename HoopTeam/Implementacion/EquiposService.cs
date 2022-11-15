using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Implementacion
{
    class EquiposService
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
       

        public List<Equipos> GetEquipo(string ent)
        {
            List<Equipos> equipos = new List<Equipos>();
            equipos = clienteEnt.GetEquipos(ent);
            return equipos;

        }

        public List<Equipos> GetTodosEquipos()
        {
            List<Equipos> equipos = new List<Equipos>();
            equipos = clienteEnt.GetTodosEquipos();
            return equipos;
        }


    }
}
