using System;
using System.Collections.Generic;
using System.Text;
using HoopTeam.Modelo;


namespace HoopTeam.Implementacion
{
    class CanchaService
    {
        //Llama a objeto de tipo ClienteEntrenador para traer resultados de la base de datos
        ClienteEntrenador clienteEnt = new ClienteEntrenador();


        //Método para retornar una lista del cliente con todas las canchas en la academia
        public List<Cancha> GetCanchas()
        {
            List<Cancha> canchas = new List<Cancha>();
            canchas = clienteEnt.GetCanchas();
            return canchas;

        }
    }
}
