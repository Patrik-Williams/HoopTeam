using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    class Administrador
    {
        private static int idAdmin;
        private static string correo;
        private static string contra;

        private static bool superUser;


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
