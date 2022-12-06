using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Modelo;

namespace HoopTeam.Implementacion
{
    class AgendaService
    {
        ClienteAgenda clienteAg = new ClienteAgenda();
      /*  public List<Agenda> GetAgenda()
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            estudiantes = clienteEnt.GetEstudiantes();
            return estudiantes;

        }
      */
        public List<Agenda> GetAgEstudiante(string est)
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = clienteAg.GetAgendaEstudiante(est);
            return agendas;
        }

        public List<Agenda> GetAgEntrenador(string ent)
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = clienteAg.GetAgendaEntrenador(ent);
            return agendas;
        }

        public List<Agenda> GetAg()
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = clienteAg.GetAgenda();
            return agendas;
        }
    }
}
