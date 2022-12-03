using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    class AgendaEstatico
    {
        private static string idAgenda;
        private static string Equipo;
        private static string Cancha;
        private static string Ubicacion;
        private static string FechaHora;
        private static string Descripcion;

        public String getidAgenda()
        {
            return idAgenda;
        }

        public void setidAgenda(String idA)
        {
            idAgenda = idA;
        }
        public String getEquipo()
        {
            return Equipo;
        }

        public void setEquipo(String Eq)
        {
            Equipo = Eq;
        }
        public String getCancha()
        {
            return Cancha;
        }

        public void setCancha(String can)
        {
            Cancha = can;
        }
        public String getUbicacion()
        {
            return Ubicacion;
        }

        public void setUbicacion(String ubi)
        {
            Ubicacion = ubi;
        }
        public String getFechaHora()
        {
            return FechaHora;
        }

        public void setFechaHora(String fechaH)
        {
            FechaHora = fechaH;
        }
        public String getDescripcion()
        {
            return Descripcion;
        }

        public void setDescripcion(String des)
        {
            Descripcion = des;
        }

    }
}
