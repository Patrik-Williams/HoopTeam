using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Modelo;

namespace HoopTeam.Implementacion
{
    class AgendaService
    {
        //Llama a objeto de tipo ClienteAgenda para traer resultados de la base de datos
        ClienteAgenda clienteAg = new ClienteAgenda();

        //Método para retornar una lista del cliente con la agenda del estudiante que entra por parámetro 
        public List<Agenda> GetAgEstudiante(string est)
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = clienteAg.GetAgendaEstudiante(est);
            return agendas;
        }

        //Método para retornar una lista del cliente con la agenda del entrenador que entra por parámetro 
        public List<Agenda> GetAgEntrenador(string ent)
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = clienteAg.GetAgendaEntrenador(ent);
            return agendas;
        }

        //Método para retornar una lista del cliente con todo el calendario disponible
        public List<Agenda> GetAg()
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = clienteAg.GetAgenda();
            return agendas;
        }
    }
}
