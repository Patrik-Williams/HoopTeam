using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
     class EstudiantesXEntrenadorViewModel
    {
        //referenia al entrenador
        Entrenador ent = new Entrenador();
        
        //lista de tipo estudiantes
        public List<EstEntrenador> EstudiantesPorEntrenador { get; set; }

        //llena la lista con los estudiantes que tiene el entrenador segun la cedula del entrenador
        public EstudiantesXEntrenadorViewModel()
        {
            EstudiantesPorEntrenador = new EstudianteService().GetEstEntrenadores(ent.getCedula());
        }
    }
}
