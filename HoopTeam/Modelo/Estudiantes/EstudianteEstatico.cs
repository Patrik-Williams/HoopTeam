﻿using System;

namespace HoopTeam.Modelo
{
    class EstudianteEstatico
    {
        //campos del objeto Estudiante
        // es estatico para mantener los datos de la sesion
        private static string cedula;
        private static string Nombre;
        private static string Apellido1;
        private static string Apellido2;
        private static string Nacimiento;
        private static string genero;
        private static string correo;
        private static string contrasenna;


        //getters and setters
        public String getCedula()
        {
            return cedula;
        }

        public void setCedula(String ced)
        {
            cedula = ced;
        }

        public String getNombre()
        {
            return Nombre;
        }

        public void setNombre(String nom)
        {
            Nombre = nom;
        }

        public String getApellido1()
        {
            return Apellido1;
        }

        public void setApellido1(String apellido1)
        {
            Apellido1 = apellido1;
        }

        public String getApellido2()
        {
            return Apellido2;
        }

        public void setApellido2(String apellido2)
        {
            Apellido2 = apellido2;
        }

        public String getNacimiento()
        {
            return Nacimiento;
        }

        public void setNacimiento(String Nac)
        {
            Nacimiento = Nac;
        }

        public String getGenero()
        {
            return genero;
        }

        public void setGenero(String Genero)
        {
            genero = Genero;
        }



        public String getCorreo()
        {
            return correo;
        }

        public void setCorreo(String cor)
        {
            correo = cor;
        }

        public String getContrasenna()
        {
            return contrasenna;
        }

        public void setContrasenna(String contra)
        {
            contrasenna = contra;
        }



    }
}
