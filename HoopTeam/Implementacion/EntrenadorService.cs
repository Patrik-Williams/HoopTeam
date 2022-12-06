using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Implementacion
{
    class EntrenadorService
    {
        ClienteAdmin clienteAdm = new ClienteAdmin();

        public List<EntrenadorNO_Estatico> GetEntrenadores()
        {
            List<EntrenadorNO_Estatico > entrenadores = new List<EntrenadorNO_Estatico>();
            entrenadores= clienteAdm.GetEntrenadores();
            return entrenadores;

        }
    }
}
