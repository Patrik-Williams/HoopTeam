using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Implementacion
{
    class EquiposService
    {
        //Creación de objeto para llamar al cliente del entrenador y sus métodos
        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        //Método que devuelve una lista de todos los equipos de un entrenador seleccionado
        public List<Equipos> GetEquipo(string ent)
        {//Define la lista, llama al método para la conexión a la base de datos y regresa la lista
            List<Equipos> equipos = new List<Equipos>();
            equipos = clienteEnt.GetEquipos(ent);
            return equipos;

        }

        //Método que devuelve una lista de todos los equipos en la base de datos
        public List<Equipos> GetTodosEquipos()
        {//Define la lista, llama al método para la conexión a la base de datos y regresa la lista
            List<Equipos> equipos = new List<Equipos>();
            equipos = clienteEnt.GetTodosEquipos();
            return equipos;
        }


    }
}
