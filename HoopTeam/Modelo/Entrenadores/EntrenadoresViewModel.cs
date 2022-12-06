using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class EntrenadoresViewModel
    {
        public List<EntrenadorNO_Estatico> entrenadores { get; set; }

        public EntrenadoresViewModel()
        {
            entrenadores = new EntrenadorService().GetEntrenadores();
        }
    }
}
