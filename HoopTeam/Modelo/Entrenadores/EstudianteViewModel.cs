using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Modelo;

namespace HoopTeam.Modelo.Entrenadores
{
    class EstudianteViewModel
    {   
        //lista de tipo estudiantes
       public List<Estudiante> Estudiantes { get; set; }

        //Llena la lista con todos los estudiantes que hay en la base 
        public EstudianteViewModel()
        {
            Estudiantes = new EstudianteService().GetEstudiantes();
        }
    }
}
