using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using HoopTeam.Modelo.Entrenadores;



namespace HoopTeam.Implementacion
{
    class ClienteAgenda
    {
        Agenda agn = new Agenda();
        Cancha cancha = new Cancha();
      

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
                    "and ee.idEquipo = eq.idEquipo " +
                    "and eq.idEquipo = ag.idEquipo " +
                    "and ag.idCanchas = ca.idCanchas " +
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
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
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
                "from Entrenador en, Equipos eq, Agenda ag, Canchas ca " +
                "where en.cedula =" + ent + " " +
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
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
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

        public List<Agenda> GetAgenda()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion

                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();


                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                List<Agenda> list = new List<Agenda>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();


                string qry = "SELECT ag.idAgenda, concat (eq.categoria, ' ', eq.genero) as Equipo, ag.idCanchas, ag.fechayHora, " +
                              "ag.descripcion, ca.ubicacion " +
                              "from Agenda ag, Canchas ca, Equipos eq " +
                              "where ag.idCanchas = ca.idCanchas " +
                              "and ag.idEquipo = eq.idEquipo;" ;

                cmd.CommandText = qry;
                cmd.Connection = con;

                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;

                Adaptador.Fill(ds, "Agenda");
                Adaptador1.Fill(ds1, "Canchas");
                Adaptador2.Fill(ds1, "Equipos");

                cmd.ExecuteNonQuery();

                dt = ds.Tables["Agenda"];
                dt1 = ds1.Tables["Canchas"];
                dt2 = ds2.Tables["Equipos"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
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

        public Agenda AgendaE(string idA)
        {
            try { 


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            Agenda est = new Agenda();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            
            
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                string qry = "SELECT * FROM Agenda where idAgenda = '" + idA + "'";

                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Agenda");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Agenda"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = drCurrent["fechayHora"].ToString();
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    
                }
                return agn;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new Agenda();
            }
           


        }
        public List<Equipos> GetEquiposA(int ent)
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

                //concatenar equipos con agenda para editar
                string qry = "SELECT * FROM Equipos Where cedEntrenador= '" + ent + "' and activo = 1 ";
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
            catch(Exception ex)
            {
                string txt = ex.Message;
                return new List<Equipos>();
            }
        }
        public List<Cancha> GetCanchasA()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<Cancha> list = new List<Cancha>();

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                       "port = 3306; " +
                                       "username = admin; " +
                                       "password = hoopteamAdmin;" +
                                       "database =HoopTeam");
                con.Open();

                //concatenar equipos con agenda para editar
                string qry = "SELECT * FROM Canchas; ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Canchas");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Canchas"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    Cancha cn = new Cancha();
                    cn.idCancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    cn.ubicacion = drCurrent["ubicacion"].ToString();


                    list.Add(cn);
                }
                return list;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return new List<Cancha>();
            }
        }
        public string idAgenda(string idA)
        {
            string flag = "";

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                string qry = "SELECT * FROM Agenda where idAgenda = '"+idA+"'";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Agenda");
                cmd.ExecuteNonQuery();
                dt = ds.Tables["Agenda"];

                foreach (DataRow drCurrent in dt.Rows)
                {
                    agn.idAgenda = drCurrent["idAgenda"].ToString();
                    agn.Equipo = drCurrent["Equipo"].ToString();
                    agn.Cancha = drCurrent["idCanchas"].ToString();
                    agn.FechaHora = drCurrent["fechayHora"].ToString();
                    agn.Descripcion = drCurrent["descripcion"].ToString();

                    flag = "Agn";
                }

            }
            catch(Exception ex)
            {
                string txt = ex.Message;
            }
            return flag;
            }


        public void AgregarAgenda(string idEqp, string idCn, string fecha, string dcp)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                DataSet dsAgenda = new DataSet();
                DataTable dtAgenda = new DataTable();

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                string qry = "INSERT INTO Agenda (idEquipo,idCanchas,fechayHora,descripcion)values( '" + idEqp + "', '" + idCn + "', curdate(), '" + dcp + "');";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry3 = "UPDATE Equipos SET cupo= cupo-1 WHERE idEquipo=" + idEqp + "";
                cmd.CommandText = qry3;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
        }
        public string EditarAgenda(string idA, string descripcion)
        {
            string flag = "";


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {


                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                string qry = "UPDATE Agenda set descripcion = '" + descripcion + "' where idAgenda= " + idA + ";";
                
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Agenda");
                cmd.ExecuteNonQuery(); 

              /*  if(idEqN != ideqV)
                {

                }*/

                dt = ds.Tables["Agenda"];

                

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
            return flag;
        }


        public void EliminarAgenda(string idA)
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

                string qry = "DELETE FROM Agenda where idAgenda ='" + idA + "'";
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
    

