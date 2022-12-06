using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;



namespace HoopTeam.Modelo.Estudiantes
{
    class EstudianteXEquipoViewModel
    {
        EstudianteEstatico est = new EstudianteEstatico();
        public List<EstEntrenador> EstudiantesPorEquipo { get; set; }
        public EstudianteXEquipoViewModel()
        {
            EstudiantesPorEquipo = new EstudianteService().GetEstEquipo(est.getCedula());
        }
    }
}
