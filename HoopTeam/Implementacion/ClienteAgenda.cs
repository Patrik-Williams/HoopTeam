using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HoopTeam.Modelo;
using MySql.Data.MySqlClient;


namespace HoopTeam.Implementacion
{
    class ClienteAgenda
    {
        public List<Agenda> GetAgendaEstudiante(string est)
        {
             try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion

                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador3 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador4 = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                DataSet ds4 = new DataSet();


                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();

                List<Agenda> list = new List<Agenda>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();


                string qry = "select ag.idAgenda, concat(eq.categoria, ' ', eq.genero) as Equipo, ag.descripcion, ag.fechayHora, ca.idCanchas, ca.ubicacion " +
                    "from Estudiantes es, EstudianteEquipo ee, Equipos eq, Agenda ag, Canchas ca " +
                    "where es.cedula = " + est + " " +
                    "and es.cedula = ee.cedEstudiante " +
                    "and ee.idEquipo = eq.idEquipo "+
                    "and eq.idEquipo = ag.idEquipo "+
                    "and ag.idCanchas = ca.idCanchas "+
                    "and ee.activo = 1;";

                cmd.CommandText = qry;
                cmd.Connection = con;

                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador3.SelectCommand = cmd;
                Adaptador4.SelectCommand = cmd;

                Adaptador.Fill(ds, "Estudiantes");
                Adaptador1.Fill(ds1, "EstudianteEquipo");
                Adaptador2.Fill(ds2, "Equipos");
                Adaptador3.Fill(ds3, "Agenda");
                Adaptador3.Fill(ds4, "Canchas");

                cmd.ExecuteNonQuery();

                dt = ds.Tables["Estudiantes"];
                dt1 = ds1.Tables["EstudianteEquipo"];
                dt2 = ds2.Tables["Equipos"];
                dt3 = ds3.Tables["Agenda"];
                dt4 = ds4.Tables["Canchas"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                foreach (DataRow drCurrent in dt3.Rows)
                {
                    Agenda ag = new Agenda();
                    ag.idAgenda = Int32.Parse(drCurrent["idAgenda"].ToString());
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = drCurrent["fechayHora"].ToString();
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    list.Add(ag);
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Agenda>();
            }
        }

        public List<Agenda> GetAgendaEntrenador(string ent)
        {
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

                List<Agenda> list = new List<Agenda>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();


                string qry = "select ag.idAgenda, concat(eq.categoria, ' ', eq.genero) as Equipo, ag.descripcion, ag.fechayHora, ca.idCanchas, ca.ubicacion " +
                "from Entrenador en, Equipos eq, Agenda ag, Canchas ca "+
                "where en.cedula =" +ent+" " + 
                "and en.cedula = eq.cedEntrenador " +
                "and eq.idEquipo = ag.idEquipo " +
                "and ag.idCanchas = ca.idCanchas; ";

                cmd.CommandText = qry;
                cmd.Connection = con;

                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador3.SelectCommand = cmd;

                Adaptador.Fill(ds, "Entrenador");
                Adaptador1.Fill(ds1, "Equipos");
                Adaptador2.Fill(ds2, "Agenda");
                Adaptador3.Fill(ds3, "Canchas");

                cmd.ExecuteNonQuery();

                dt = ds.Tables["Entrenador"];
                dt1 = ds1.Tables["Equipos"];
                dt2 = ds2.Tables["Agenda"];
                dt3 = ds3.Tables["Canchas"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                foreach (DataRow drCurrent in dt3.Rows)
                {
                    Agenda ag = new Agenda();
                    ag.idAgenda = Int32.Parse(drCurrent["idAgenda"].ToString());
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = drCurrent["fechayHora"].ToString();
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    list.Add(ag);
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Agenda>();
            }
        }
    }
}
