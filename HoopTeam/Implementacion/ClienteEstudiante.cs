using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HoopTeam.Implementacion
{
    class ClienteEstudiante
    {
        EstudianteEstatico est = new EstudianteEstatico();
        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public ClienteEstudiante()
        {
        }

        //actualizar datos del estudiante
        public string LogIn(string correo, string contra)
        {
            string flag = "";


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();

            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEstudiante, "estudiantes");
                cmd.ExecuteNonQuery();
                tbEstudiante = dsEstudiante.Tables["estudiantes"];

                    foreach (DataRow drCurrent in tbEstudiante.Rows)
                    {
                        est.setCedula(drCurrent["cedula"].ToString());
                        est.setNombre(drCurrent["nombre"].ToString());
                        est.setApellido1(drCurrent["apellido1"].ToString());
                        est.setApellido2(drCurrent["apellido2"].ToString());
                        est.setNacimiento(drCurrent["fechaNacimiento"].ToString());
                        est.setGenero(drCurrent["genero"].ToString());
                        est.setCorreo(drCurrent["correo"].ToString());
                        est.setContrasenna(drCurrent["contrasenna"].ToString());

                        flag = "Est";
                    }
                }
                catch(Exception ex)

            {
                string txt = ex.Message;
            }

            return flag;
            }
            
            public string actualizarEstudiante(string nom, string ap1, string ap2, string correo, string con, string ced)
      
            {

            string flag = "";



            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con1;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();


            try
            {
                con1 = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con1.Open();
                string qry = "UPDATE Estudiantes set nombre = '" + nom + "', apellido1 = '" + ap1 + "', apellido2 = '" + ap2 + "', correo = '" + correo + "',contrasenna = '" + con + "' where cedula = " + ced + " ";
                cmd.CommandText = qry;
                cmd.Connection = con1;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEstudiante, "Estudiantes");
                cmd.ExecuteNonQuery();

                tbEstudiante = dsEstudiante.Tables["Estudiantes"];


                this.LogIn(correo, con);
            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
            return flag;

        }
    }
}





