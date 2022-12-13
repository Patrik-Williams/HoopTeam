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
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "INSERT INTO Equipos (categoria, genero, cedEntrenador, cupo, activo) values ('" + categoria + "', '" + genero + "', " + ent + ", " + cupo + ", 1)";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //declara variable
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
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
               //Crea objeto 
                Estudiante est = new Estudiante();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //Crea lista 
                List<EntrenadorNO_Estatico> list = new List<EntrenadorNO_Estatico>();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "SELECT * FROM Entrenador where activo = 1";
                //convertir qry en comando
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Entrenador");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Entrenador"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {
                    //Crea objeto 
                    EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
                    ent.Cedula = drCurrent["cedula"].ToString();
                    ent.Nombre = drCurrent["nombre"].ToString();
                    ent.Apellido1 = drCurrent["apellido1"].ToString();
                    ent.Apellido2 = drCurrent["apellido2"].ToString();
                    ent.NombreCompleto = ent.Nombre + " " + ent.Apellido1 + " " + ent.Apellido2;
                    ent.Correo = drCurrent["correo"].ToString();
                   
                    //Agrega objeto a la lista 
                    list.Add(ent);
                    Debug.Write("Hola mundo");
                    Console.WriteLine("Hola mundo");
                }
                //Retorna lista 
                return list;
            }
            catch (Exception ex)
            {
                //declara variable
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
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //Crea objeto 
                Estudiante est = new Estudiante();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();


                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "SELECT * FROM Entrenador where cedula = " + ced + " and activo = 1;";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Entrenador");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Entrenador"];
                //Crea objeto 
                EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
                //Asigna los valores a su respectiva variable en el objeto por cada fila 
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
                //Retorna objeto 
                return ent;
            }
            catch (Exception ex)
            {
                //Declara variable 
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
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos 
                string qry = "Update Equipos set activo = 0 where idEquipo = "+idEquipo+"";
                //convertir qry en comando
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //consulta base de datos 
                string qry2 = "UPDATE EstudianteEquipo SET activo = 0 where idEquipo = "+idEquipo+"";
                //convertir qry en comando
                cmd.CommandText = qry2;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;

            }
        }

        public void EditarEquipo(string cate, string gen, int ent, int cupo, int idEquipo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                // Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos
                string qry = "UPDATE Equipos set categoria = '"+cate+"', genero = '"+gen+"', cedEntrenador = "+ent+", cupo = "+cupo+" where  idEquipo = "+idEquipo+" ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                // convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;

            }
        }

        public void AgregarEntrenador(int ced, string nom, string ap1, string ap2, string correo, string contra)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "INSERT INTO Entrenador (cedula, nombre, apellido1, apellido2, correo, contrasenna, activo) values ("+ced+", '"+nom+"', '"+ap1+"', '"+ap2+"', '"+correo+"', '"+contra+"', 1)";
                //convertir qry en comando
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;

            }
        }
        public void EditarEntrenador(int ced, string nom, string ap1, string ap2, string correo, string contra)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "UPDATE Entrenador set nombre = '"+nom+"', apellido1 = '"+ap1+"', apellido2 = '"+ap2+"', correo = '"+correo+"' where cedula = "+ced+"";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;

            }
        }

        public int verificarEquipoEnt(int ced)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos 
                string qry = "select count(*) as cant from Equipos where cedEntrenador = "+ced+" and activo = 1 ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Equipos");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Equipos"];
                //Declara variable 
                int cant = 0;
                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {

                    cant = Int32.Parse(drCurrent["cant"].ToString());


                    Debug.Write("Hola mundo");
                    Console.WriteLine("Hola mundo");
                }
                //Retorna variable 
                return cant;



            }
            catch (Exception ex)
            {
                //Decalra variable 
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
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos 
                string qry = "Update Entrenador set activo = 0 where cedula = "+ced+"";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;

            }
        }
    }
}
