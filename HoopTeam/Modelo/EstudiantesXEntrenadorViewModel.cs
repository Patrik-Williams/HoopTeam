using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    class EstudiantesXEntrenadorViewModel
    {
        public List<Estudiante> Estudiantes { get; set; }

        public EstudiantesXEntrenadorViewModel()
        {
            Estudiantes = new EstudianteService().GetEstudiantes();
        }
    }
}
