using HoopTeam.Modelo.Estudiantes;
using HoopTeam.Modelo;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;


namespace HoopTeam.Implementacion
{
    class EstudianteService
    {
        //Creación de objetos para llamar a los clientes del entrenador y estudiante y sus métodos
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        ClienteEstudiante clienteEst = new ClienteEstudiante();

        //Método que devuelve una lista de todos los estudiantes
        public List<Estudiante> GetEstudiantes()
        {//Define la lista, llama al método para la conexión a la base de datos y regresa la lista
            List<Estudiante> estudiantes = new List<Estudiante>();
            estudiantes = clienteEnt.GetEstudiantes();
            return estudiantes;

        }

        //Método que devuelve una lista de todos los estudiantes de un entrenador seleccionado
        public List<EstEntrenador> GetEstEntrenadores(string ent)
        {//Define la lista, llama al método para la conexión a la base de datos y regresa la lista
            List<EstEntrenador> estudiantes = new List<EstEntrenador>();
            estudiantes = clienteEnt.GetEstEntrenador(ent);
            return estudiantes;
        }

        //Método que devuelve una lista de estudiantes que pertenecen al equipo de un estudiante seleccionado
        public List<EstEntrenador> GetEstEquipo(string ced)
        {//Define la lista, llama al método para la conexión a la base de datos y regresa la lista
            List<EstEntrenador> estudiantes = new List<EstEntrenador>();
            estudiantes = clienteEst.GetEstEquipo(ced);
            return estudiantes;
        }
    }
}
