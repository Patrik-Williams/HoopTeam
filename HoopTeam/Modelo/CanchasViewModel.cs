using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo
{
    class CanchasViewModel
    {
        
        //lista de tipo cancha
        public List<Cancha> CanchasTotal { get; set; }

        //carga la lista con todas las canchas 
        public CanchasViewModel()
        {
            CanchasTotal = new CanchaService().GetCanchas();
        }
    }
}
