using HoopTeam.Modelo;
using HoopTeam.Modelo.Entrenadores;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HoopTeam.Implementacion
{
    class ClienteEntrenador
    {
        Entrenador ent = new Entrenador();
        

        public List<Estudiante> GetEstudiantes()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<Estudiante> list = new List<Estudiante>();
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
                Adaptador.Fill(ds,"Estudiantes");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Estudiantes"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Estudiante est = new Estudiante();
                    est.Cedula = drCurrent["cedula"].ToString();
                    est.Nombre = drCurrent["nombre"].ToString();
                    est.Apellido1 = drCurrent["apellido1"].ToString();
                    est.Apellido2 = drCurrent["apellido2"].ToString();
                    est.NombreCompleto = est.Nombre + " " + est.Apellido1 + " " + est.Apellido2;
                    est.Nacimiento = drCurrent["fechaNacimiento"].ToString();
                    est.Genero = drCurrent["genero"].ToString();
                    est.Correo = drCurrent["correo"].ToString();
                    est.Contrasenna = drCurrent["contrasenna"].ToString();



                    list.Add(est);
                    Debug.Write("Hola mundo");
                    Console.WriteLine("Hola mundo");
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Estudiante>();
            }
        }

        public List<EstEntrenador> GetEstEntrenador(string ent)
        {
            Console.WriteLine(ent);
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();


                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                List<EstEntrenador> list = new List<EstEntrenador>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                /*  string qry = "select es.cedula as Cedula, concat(es.nombre, ' ', es.apellido1, ' ', es.apellido2) as Nombre," +
                    "eq.idEquipo, eq.categoria, eq.genero" +
                    "from Estudiantes es, Entrenador en, Equipos eq, EstudianteEquipo ee" +
                    "where en.cedula = '" + ent + "'" +
                    "and en.cedula = eq.cedEntrenador" +
                    "and eq.idequipo = ee.idequipo" +
                    "and ee.cedestudiante = es.cedula;";*/

                string qry = "select ent.nombre from Entrenador ent, Equipos e where ent.cedula = e.cedEntrenador";

                                 cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                // Adaptador.Fill(ds, "Estudiantes");
                Adaptador.Fill(ds, "Entrenador" );
                Adaptador1.Fill(ds1, "Equipos");
               // Adaptador.Fill(ds, "EstudianteEquipo");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Entrenador"];
                dt1 = ds1.Tables["Equipos"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta
                
                foreach (DataRow drCurrent in dt.Rows)
                {
                    EstEntrenador est = new EstEntrenador();
                      est.NombreCompleto = drCurrent["Nombre"].ToString();
                    //est.IdEquipo = drCurrent["idEquipo"].ToString();
                    //est.Categoria = drCurrent["categoria"].ToString();
                    //est.Genero = drCurrent["genero"].ToString();

                    list.Add(est);
                 
                    Console.WriteLine("Hola mundo");
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<EstEntrenador>();
            }
        }
    }

}
