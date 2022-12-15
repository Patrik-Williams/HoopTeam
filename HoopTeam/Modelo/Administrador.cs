using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    class Administrador
    {
        //campos del objeto de tipo administrador
        //estatico, permite conservar la sesion
        private static int idAdmin;
        private static string correo;
        private static string contra;

        //bandera booleana que permite saber si el usuario es super Admin
        private static bool superUser;

        //getters and setters
        public int getIdAdmin()
        {
            return idAdmin;
        }
        public void setIdAdmin(int id)
        {
            idAdmin = id;
        }
        public String getCorreo()
        {
            return correo;
        }
        public void setCorreo(string corr)
        {
            correo = corr;
        }
        public String getContra()
        {
            return contra;
        }
        public void setContra(string cont)
        {
            contra = cont;
        }
        public bool getSuperUser()
        {
            return superUser;
        }
        public void setSuperUser(bool flag)
        {
            superUser = flag;
        }
    }
}
