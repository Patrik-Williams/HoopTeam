using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo
{
    class AgendaPorEntrenadorViewModel
    {
        //referencia al entrenador que permite tener los datos de la sesion de entrenador
        Entrenador ent = new Entrenador();

        //lista de tipo agenda
        public List<Agenda> AgendaPorEntrenador { get; set; }

        //Llena la lista con la informacion del la base de datos de agendas segun el entrenador
        public AgendaPorEntrenadorViewModel()
        {
            AgendaPorEntrenador = new AgendaService().GetAgEntrenador(ent.getCedula());
        }
    }
}
