using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Entrenadores
{
    class EntrenadoresViewModel
    {   
        //lista de entrenador que no es estatico, pernite el collection view
        public List<EntrenadorNO_Estatico> entrenadores { get; set; }

        //llena la lista con todos los entrenadores en la base 
        public EntrenadoresViewModel()
        {
            entrenadores = new EntrenadorService().GetEntrenadores();
        }
    }
}
