using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo.Entrenadores
{
    class EstudianteViewModel
    {
       public List<Estudiante> Estudiantes { get; set; }

        public EstudianteViewModel()
        {
            Estudiantes = new EstudianteService().GetEstudiantes();
        }
    }
}
