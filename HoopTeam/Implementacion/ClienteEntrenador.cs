using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace HoopTeam.Implementacion
{
    class ClienteEntrenador
    {
        Entrenador ent = new Entrenador();

        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public List<Estudiante> GetList()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Estudiante> list = new List<Estudiante>();
            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Estudiantes";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Estudiantes");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["EstudiantePorEntrenador"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Estudiante est = new Estudiante();
                    est.setCedula(drCurrent["cedula"].ToString());
                    est.setNombre(drCurrent["nombre"].ToString());
                    est.setApellido1(drCurrent["apellido1"].ToString());
                    est.setApellido2(drCurrent["apellido2"].ToString());
                    est.setNacimiento(drCurrent["nacimiento"].ToString());
                    est.setGenero(drCurrent["genero"].ToString());
                    est.setCorreo(drCurrent["correo"].ToString());
                    est.setContrasenna(drCurrent["contrasenna"].ToString());

                    list.Add(est);
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Estudiante>();
            }
        }
    }

}
