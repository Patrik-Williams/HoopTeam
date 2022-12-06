using HoopTeam.Modelo;
using HoopTeam.Modelo.Entrenadores;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace HoopTeam.Implementacion
{
    class ClienteAdmin
    {

        public void AgregarEquipo(string categoria, string genero, int ent, int cupo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                string qry = "INSERT INTO Equipos (categoria, genero, cedEntrenador, cupo) values ('" + categoria + "', '" + genero + "', " + ent + ", " + cupo + ")";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //App.Current.MainPage.DisplayAlert("Alert", "your message", "OK");

            }
        }

        public List<EntrenadorNO_Estatico> GetEntrenadores()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                Estudiante est = new Estudiante();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                List<EntrenadorNO_Estatico> list = new List<EntrenadorNO_Estatico>();

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Entrenador";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Entrenador");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Entrenador"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
                    ent.Cedula = drCurrent["cedula"].ToString();
                    ent.Nombre = drCurrent["nombre"].ToString();
                    ent.Apellido1 = drCurrent["apellido1"].ToString();
                    ent.Apellido2 = drCurrent["apellido2"].ToString();
                    ent.NombreCompleto = ent.Nombre + " " + ent.Apellido1 + " " + ent.Apellido2;
                    ent.Correo = drCurrent["correo"].ToString();
                   
                    list.Add(ent);
                    Debug.Write("Hola mundo");
                    Console.WriteLine("Hola mundo");
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<EntrenadorNO_Estatico>();
            }
        }

        public EntrenadorNO_Estatico GetEntrenador(int ced)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                Estudiante est = new Estudiante();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Entrenador where cedula = " + ced + ";";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Entrenador");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Entrenador"];
                EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
                foreach (DataRow drCurrent in dt.Rows)
                {
                    
                    ent.Cedula = drCurrent["cedula"].ToString();
                    ent.Nombre = drCurrent["nombre"].ToString();
                    ent.Apellido1 = drCurrent["apellido1"].ToString();
                    ent.Apellido2 = drCurrent["apellido2"].ToString();
                    ent.NombreCompleto = ent.Nombre + " " + ent.Apellido1 + " " + ent.Apellido2;
                    ent.Correo = drCurrent["correo"].ToString();

                    
                    Debug.Write("Hola mundo");
                    Console.WriteLine("Hola mundo");
                }
                return ent;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new EntrenadorNO_Estatico();
            }
        }
    }
}
