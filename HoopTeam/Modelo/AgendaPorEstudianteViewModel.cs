using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo
{
    class AgendaPorEstudianteViewModel
    {
        //referencia a la sesion del estudiante
        EstudianteEstatico est = new EstudianteEstatico();

        //lista de tipo agenda
        public List<Agenda> AgendaPorEstudiante { get; set; }

        //carga la agenda segun el estudiante usando la cedula
        public AgendaPorEstudianteViewModel()
        {
            AgendaPorEstudiante = new AgendaService().GetAgEstudiante(est.getCedula());
        }
    }
}
