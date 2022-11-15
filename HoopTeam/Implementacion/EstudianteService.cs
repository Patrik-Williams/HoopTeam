using HoopTeam.Modelo;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;


namespace HoopTeam.Implementacion
{
    class EstudianteService
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        ClienteEstudiante clienteEst = new ClienteEstudiante();
        public List<Estudiante> GetEstudiantes()
        {
            List<Estudiante> estudiantes= new List<Estudiante>();
            estudiantes = clienteEnt.GetEstudiantes();
            return estudiantes;

        }
       // public List<Estudiante> GetEstudiantesPerfil(string estP)
       // {
       //     List<Estudiante> estudiantesP = new List<Estudiante>();
       //     estudiantesP = clienteEst.GetEstudiantesPerfil(estP);
       //     return estudiantesP;

       // }
        public List<EstEntrenador> GetEstEntrenadores(string ent)
        {
            List<EstEntrenador> estudiantes = new List<EstEntrenador>();
            estudiantes = clienteEnt.GetEstEntrenador(ent);
            return estudiantes;
        }


    }
}
