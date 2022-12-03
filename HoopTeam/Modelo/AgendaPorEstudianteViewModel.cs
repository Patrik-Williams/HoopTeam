using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo
{
    class AgendaPorEstudianteViewModel
    {
        EstudianteEstatico est = new EstudianteEstatico();

        public List<Agenda> AgendaPorEstudiante { get; set; }

        public AgendaPorEstudianteViewModel()
        {
            AgendaPorEstudiante = new AgendaService().GetAgEstudiante(est.getCedula());
        }
    }
}
