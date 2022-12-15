using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Implementacion
{
    class EntrenadorService
    {
        //Creación de objeto para llamar al cliente del administrador y sus métodos
        ClienteAdmin clienteAdm = new ClienteAdmin();

        //Método que devuelve una lista de todos los entrenadores
        public List<EntrenadorNO_Estatico> GetEntrenadores()
        {
            //Define la lista, llama al método para la conexión a la base de datos y regresa la lista
            List<EntrenadorNO_Estatico > entrenadores = new List<EntrenadorNO_Estatico>();
            entrenadores= clienteAdm.GetEntrenadores();
            return entrenadores;

        }
    }
}
