using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
     class EstudiantesXEntrenadorViewModel
    {
        Entrenador ent = new Entrenador();
        
        public List<EstEntrenador> EstudiantesPorEntrenador { get; set; }

        public EstudiantesXEntrenadorViewModel()
        {
            EstudiantesPorEntrenador = new EstudianteService().GetEstEntrenadores(ent.getCedula());
        }
    }
}
