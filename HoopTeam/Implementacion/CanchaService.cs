using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Modelo;


namespace HoopTeam.Implementacion
{
    class CanchaService
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();



        public List<Cancha> GetCanchas()
        {
            List<Cancha> canchas = new List<Cancha>();
            canchas = clienteEnt.GetCanchas();
            return canchas;

        }
    }
}
