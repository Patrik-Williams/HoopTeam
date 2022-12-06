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

                string qry = "INSERT INTO Equipos (categoria, genero, cedEntrenador, cupo, activo) values ('" + categoria + "', '" + genero + "', " + ent + ", " + cupo + ", 1)";
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
                string qry = "SELECT * FROM Entrenador where activo = 1";
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
                string qry = "SELECT * FROM Entrenador where cedula = " + ced + " and activo = 1;";
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
        public void EliminarEquipo(int idEquipo)
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


                string qry = "Update Equipos set activo = 0 where idEquipo = "+idEquipo+"";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry2 = "UPDATE EstudianteEquipo SET activo = 0 where idEquipo = "+idEquipo+"";
                cmd.CommandText = qry2;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        public void EditarEquipo(string cate, string gen, int ent, int cupo, int idEquipo)
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

                string qry = "UPDATE Equipos set categoria = '"+cate+"', genero = '"+gen+"', cedEntrenador = "+ent+", cupo = "+cupo+" where  idEquipo = "+idEquipo+" ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        public void AgregarEntrenador(int ced, string nom, string ap1, string ap2, string correo, string contra)
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

                string qry = "INSERT INTO Entrenador (cedula, nombre, apellido1, apellido2, correo, contrasenna, activo) values ("+ced+", '"+nom+"', '"+ap1+"', '"+ap2+"', '"+correo+"', '"+contra+"', 1)";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }
        public void EditarEntrenador(int ced, string nom, string ap1, string ap2, string correo, string contra)
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

                string qry = "UPDATE Entrenador set nombre = '"+nom+"', apellido1 = '"+ap1+"', apellido2 = '"+ap2+"', correo = '"+correo+"' where cedula = "+ced+"";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        public int verificarEquipoEnt(int ced)
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


                string qry = "select count(*) as cant from Equipos where cedEntrenador = "+ced+" and activo = 1 ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];
                int cant = 0;
                foreach (DataRow drCurrent in dt.Rows)
                {

                    cant = Int32.Parse(drCurrent["cant"].ToString());


                    Debug.Write("Hola mundo");
                    Console.WriteLine("Hola mundo");
                }
                return cant;



            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                int i = 0;
                return i;

            }
        }

        public void EliminarEntrenador(int ced)
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


                string qry = "Update Entrenador set activo = 0 where cedula = "+ced+"";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }
    }
}
