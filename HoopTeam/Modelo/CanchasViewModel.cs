using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo
{
    class CanchasViewModel
    {
        //Entrenador ent = new Entrenador();

        public List<Cancha> CanchasTotal { get; set; }

        public CanchasViewModel()
        {
            CanchasTotal = new CanchaService().GetCanchas();
        }
    }
}
