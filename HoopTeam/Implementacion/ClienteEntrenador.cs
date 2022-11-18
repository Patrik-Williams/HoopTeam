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
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador3 = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                List<EstEntrenador> list = new List<EstEntrenador>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();


                string qry = "SELECT es.cedula as Cedula, concat(es.nombre, ' ', es.apellido1, ' ', es.apellido2) as Nombre ,eq.idEquipo, eq.categoria, eq.genero " +
                    "FROM Estudiantes es, Entrenador en, Equipos eq, EstudianteEquipo ee " +
                    "WHERE en.cedula = "+ ent +" " +
                    "AND en.cedula = eq.cedEntrenador "  +
                    "AND eq.idequipo = ee.idequipo " +
                    "AND ee.cedestudiante = es.cedula " +
                    "AND ee.activo = 1; ";

                //string qry = "select ent.nombre from Entrenador ent, Equipos e where ent.cedula = e.cedEntrenador";

                cmd.CommandText = qry;
                cmd.Connection = con;

                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador3.SelectCommand = cmd;

                // Adaptador.Fill(ds, "Estudiantes");
                Adaptador2.Fill(ds2, "Estudiantes");
                Adaptador.Fill(ds, "Entrenador"); 
                Adaptador1.Fill(ds1, "Equipos");    
                Adaptador3.Fill(ds3, "EstudianteEquipo");
               
                cmd.ExecuteNonQuery();

                dt2 = ds2.Tables["Estudiantes"];
                dt = ds.Tables["Entrenador"];
                dt1 = ds1.Tables["Equipos"];
                dt3 = ds3.Tables["EstudianteEquipo"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                foreach (DataRow drCurrent in dt2.Rows)
                {
                    EstEntrenador est = new EstEntrenador();
                    est.Cedula = drCurrent["Cedula"].ToString();
                    est.NombreCompleto = drCurrent["Nombre"].ToString();
                    est.IdEquipo = drCurrent["idEquipo"].ToString();
                    est.Categoria = drCurrent["categoria"].ToString();
                    est.Genero = drCurrent["genero"].ToString();

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

        public List<Equipos> GetTodosEquipos()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                DataSet ds2 = new DataSet();
                DataTable dt2 = new DataTable();
                List<Equipos> list = new List<Equipos>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT e.idEquipo, e.categoria, e.genero, e.cedEntrenador, e.cupo, concat(et.nombre, ' ', et.apellido1, ' ', et.apellido2) as Nombre FROM Equipos e, Entrenador et " +
                    "WHERE e.cedEntrenador = et.cedula;";
                cmd.CommandText = qry;
                cmd.Connection = con;

                Adaptador.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                Adaptador2.Fill(ds2, "Entrenador");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];
                dt2 = ds2.Tables["Entrenador"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.Entrenador = drCurrent["Nombre"].ToString();
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    list.Add(eq);
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Equipos>();
            }
        }

        public List<Equipos> GetEquipos(string ent)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<Equipos> list = new List<Equipos>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Equipos Where cedEntrenador = " + ent + " ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    list.Add(eq);

                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Equipos>();
            }
        }

        public Estudiante GetEstudiante(string ced)
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
                string qry = "SELECT * FROM Estudiantes Where cedula = " + ced + " ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Estudiantes");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Estudiantes"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    est.Cedula = drCurrent["cedula"].ToString();
                    est.Nombre = drCurrent["nombre"].ToString();
                    est.Apellido1 = drCurrent["apellido1"].ToString();
                    est.Apellido2 = drCurrent["apellido2"].ToString();
                    est.Genero = drCurrent["genero"].ToString();
                    est.Nacimiento = drCurrent["fechaNacimiento"].ToString();
                    est.Correo = drCurrent["correo"].ToString();
                    est.Contrasenna = drCurrent["contrasenna"].ToString();
                }
                return est;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new Estudiante();
            }
        }

        public List<Equipos> GetEquiposGenero(string gen)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<Equipos> list = new List<Equipos>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Equipos Where genero = '" + gen + "' and (cupo > 0) ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    list.Add(eq);

                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Equipos>();
            }
        }

        public List<Equipos> GetEquiposGen_Ent(string gen, int ent)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<Equipos> list = new List<Equipos>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Equipos Where genero = '" + gen + "' and cedEntrenador = " + ent + " and (cupo > 0) ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    list.Add(eq);

                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Equipos>();
            }
        }

        public void EditarInfoEst(int ced, string nom, string ap1, string ap2, string correo, string contra, int equipoNuevo, int equipoViejo)
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

                string qry = "UPDATE Estudiantes set Nombre = '" + nom + "', Apellido1 ='" + ap1 + "', Apellido2 ='" + ap2 + "'" +
                    ", correo= '" + correo + "', " + "contrasenna = '" + contra + "' where cedula = " + ced + " ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                if(equipoNuevo != equipoViejo)
                {
                    string qry2 = "INSERT INTO EstudianteEquipo (fechaInicio, cedEstudiante, idEquipo, activo )values( curdate(), " + ced + ", " + equipoNuevo + ", 1)";
                    cmd.CommandText = qry2;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string qry5 = "UPDATE EstudianteEquipo SET activo = 0 where cedEstudiante = " + ced + " and idEquipo = " + equipoViejo + ";";
                    cmd.CommandText = qry5;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }



                string qry3 = "UPDATE Equipos SET cupo= cupo-1 WHERE idEquipo=" + equipoNuevo + "";
                cmd.CommandText = qry3;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


                string qry4 = "UPDATE Equipos SET cupo= cupo+1 WHERE idEquipo=" + equipoViejo + "";
                cmd.CommandText = qry4;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        public void EliminarEstudiante(int ced)
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

                string qry = "DELETE FROM Estudiantes where cedula = " + ced + " ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry2 = "DELETE FROM EstudianteEquipo where cedula = " + ced + " ";
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
