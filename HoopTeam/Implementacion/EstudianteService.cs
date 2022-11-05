using HoopTeam.Modelo;
using System;
using System.Collections.Generic;
using System.Text;


namespace HoopTeam.Implementacion
{
    class EstudianteService
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        public List<Estudiante> GetEstudiantes()
        {
            List<Estudiante> estudiantes= new List<Estudiante>();
            estudiantes = clienteEnt.GetList();
            return estudiantes;

        }
    }
}
