using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam
{
    class Datos
    {
        private static string cedula;

        public void setCedula(string ced)
        {
            cedula = ced;
        }

        public string getCedula()
        {
            return cedula;
        }
    }
}
