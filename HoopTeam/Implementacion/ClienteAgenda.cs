using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using HoopTeam.Modelo.Entrenadores;
using System.Diagnostics;

namespace HoopTeam.Implementacion
{
    class ClienteAgenda
    { 
        //Crea objetos
        Agenda agn = new Agenda();
        Cancha cancha = new Cancha();
      

        public List<Agenda> GetAgendaEstudiante(string est)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion

                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador3 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador4 = new MySqlDataAdapter();

                //objeto para almacenar datos
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                DataSet ds4 = new DataSet();

                //Representar tablas dataset 
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();

                //Crea lista
                List<Agenda> list = new List<Agenda>();

                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos 
                string qry = "select ag.idAgenda, concat(eq.categoria, ' ', eq.genero) as Equipo, ag.descripcion, ag.fechayHora, ca.idCanchas, ca.ubicacion " +
                    "from Estudiantes es, EstudianteEquipo ee, Equipos eq, Agenda ag, Canchas ca " +
                    "where es.cedula = " + est + " " +
                    "and es.cedula = ee.cedEstudiante " +
                    "and ee.idEquipo = eq.idEquipo " +
                    "and eq.idEquipo = ag.idEquipo " +
                    "and ag.idCanchas = ca.idCanchas " +
                    "and ee.activo = 1;";

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador3.SelectCommand = cmd;
                Adaptador4.SelectCommand = cmd;

                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Estudiantes");
                Adaptador1.Fill(ds1, "EstudianteEquipo");
                Adaptador2.Fill(ds2, "Equipos");
                Adaptador3.Fill(ds3, "Agenda");
                Adaptador3.Fill(ds4, "Canchas");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Estudiantes"];
                dt1 = ds1.Tables["EstudianteEquipo"];
                dt2 = ds2.Tables["Equipos"];
                dt3 = ds3.Tables["Agenda"];
                dt4 = ds4.Tables["Canchas"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt3.Rows)
                {
                    //Crea un nuevo objeto 
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    //Agrega el objeto a la lista 
                    list.Add(ag);
                }
                //Retorna lista 
                return list;
            }
            catch (Exception ex)
            {
                //Declara variable 
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

                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador3 = new MySqlDataAdapter();

                //objeto para almacenar datos
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();

                //Representar tablas dataset 
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                //Crea lista
                List<Agenda> list = new List<Agenda>();

                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos 
                string qry = "select ag.idAgenda, concat(eq.categoria, ' ', eq.genero) as Equipo, ag.descripcion, ag.fechayHora, ca.idCanchas, ca.ubicacion " +
                "from Entrenador en, Equipos eq, Agenda ag, Canchas ca " +
                "where en.cedula =" + ent + " " +
                "and en.cedula = eq.cedEntrenador " +
                "and eq.idEquipo = ag.idEquipo " +
                "and ag.idCanchas = ca.idCanchas; ";

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador3.SelectCommand = cmd;

                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Entrenador");
                Adaptador1.Fill(ds1, "Equipos");
                Adaptador2.Fill(ds2, "Agenda");
                Adaptador3.Fill(ds3, "Canchas");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Entrenador"];
                dt1 = ds1.Tables["Equipos"];
                dt2 = ds2.Tables["Agenda"];
                dt3 = ds3.Tables["Canchas"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta
                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt3.Rows)
                {
                    //Crea objeto 
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    //Agrega objeto a la lista 
                    list.Add(ag);
                }
                //Retona la lista 
                return list;
            }
            catch (Exception ex)
            {
                //Declara variable 
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

                //Declara la variable tipo DataAdapter 
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();

                //objeto para almacenar datos
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();

                //Representar tablas dataset 
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                //Crea lista tipo agenda 
                List<Agenda> list = new List<Agenda>();
                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos
                string qry = "SELECT ag.idAgenda, concat (eq.categoria, ' ', eq.genero) as Equipo, ag.idCanchas, ag.fechayHora, " +
                              "ag.descripcion, ca.ubicacion " +
                              "from Agenda ag, Canchas ca, Equipos eq " +
                              "where ag.idCanchas = ca.idCanchas " +
                              "and ag.idEquipo = eq.idEquipo;" ;

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Agenda");
                Adaptador1.Fill(ds1, "Canchas");
                Adaptador2.Fill(ds1, "Equipos");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Agenda"];
                dt1 = ds1.Tables["Canchas"];
                dt2 = ds2.Tables["Equipos"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {
                    //Crea un objeto 
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    //Agrega objeto a la lista 
                    list.Add(ag);
                }
                //Retorna lista
                return list;
            }
            catch (Exception ex)
            {
                //Declara varibale 
                string txt = ex.Message;
                return new List<Agenda>();
            }
        }

        public Agenda AgendaE(string idA)
        {
            try { 


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion

            //Declara la variable tipo DataAdapter 
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            //Crea objeto 
            Agenda est = new Agenda();
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
                string qry = "SELECT * FROM Agenda where idAgenda = '" + idA + "'";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Agenda");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Agenda"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {
                    //Crea objeto 
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    
                }
                //Retorna objeto 
                return agn;
            }
            catch (Exception ex)
            {
                //Declara variable 
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
                //Declara la variable tipo DataAdapter 
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();
                //Crea lista 
                List<Equipos> list = new List<Equipos>();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                       "port = 3306; " +
                                       "username = admin; " +
                                       "password = hoopteamAdmin;" +
                                       "database =HoopTeam");
                con.Open();

                //concatenar equipos con agenda para editar
                //consulta base de datos
                string qry = "SELECT * FROM Equipos Where cedEntrenador= '" + ent + "' and activo = 1 ";
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

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
               {
                    //Crea objeto 
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    //Agrega objeto a la lista 
                    list.Add(eq);
                }
                //Retorn lista 
                return list;
            }
            catch(Exception ex)
            {
                //Declara variable
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
                //Declara la variable tipo DataAdapter 
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                //objeto para almacenar datos
                DataSet ds = new DataSet();
                //Representar tablas dataset 
                DataTable dt = new DataTable();
                //Crea lista Cancha
                List<Cancha> list = new List<Cancha>();
                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                       "port = 3306; " +
                                       "username = admin; " +
                                       "password = hoopteamAdmin;" +
                                       "database =HoopTeam");
                con.Open();

                //concatenar equipos con agenda para editar
                //consulta base de datos 
                string qry = "SELECT * FROM Canchas; ";
                //convertir qry en comando
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Canchas");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Canchas"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {

                    //Crea objeto 
                    Cancha cn = new Cancha();
                    cn.idCancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    cn.ubicacion = drCurrent["ubicacion"].ToString();

                    //Agrega objeto a la lista 
                    list.Add(cn);
                }
                //Retorna lista 
                return list;
            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;
                return new List<Cancha>();
            }
        }
        public string idAgenda(string idA)
        {
            string flag = "";

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            //Declara la variable tipo DataAdapter 
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            //objeto para almacenar datos
            DataSet ds = new DataSet();
            //Representar tablas dataset 
            DataTable dt = new DataTable();

            try
            {
                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos
                string qry = "SELECT * FROM Agenda where idAgenda = '"+idA+"'";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Agenda");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Agenda"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {
                    agn.idAgenda = drCurrent["idAgenda"].ToString();
                    agn.Equipo = drCurrent["Equipo"].ToString();
                    agn.Cancha = drCurrent["idCanchas"].ToString();
                    agn.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    agn.Descripcion = drCurrent["descripcion"].ToString();

                    flag = "Agn";
                }

            }
            catch(Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;
            }
            return flag;
            }


        public void AgregarAgenda(string idEqp, string idCn, string fechaHora, string dcp)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter 
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet dsAgenda = new DataSet();
                //Representar tablas dataset 
                DataTable dtAgenda = new DataTable();

                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "INSERT INTO Agenda (idEquipo,idCanchas,fechayHora,descripcion)values( '" + idEqp + "', '" + idCn + "', curdate(), '" + dcp + "');";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //consulta base de datos 
                string qry3 = "UPDATE Equipos SET cupo= cupo-1 WHERE idEquipo=" + idEqp + "";
                //convertir qry en comando 
                cmd.CommandText = qry3;
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
        public string EditarAgenda(string idA, string descripcion)
        {
            string flag = "";


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            //Declara la variable tipo DataAdapter 
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            //objeto para almacenar datos
            DataSet ds = new DataSet();
            //Representar tablas dataset 
            DataTable dt = new DataTable();
            try
            {

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //consulta base de datos 
                string qry = "UPDATE Agenda set descripcion = '" + descripcion + "' where idAgenda= " + idA + ";";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Agenda");
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                /*  if(idEqN != ideqV)
                  {

                  }*/
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Agenda"];

                

            }
            catch (Exception ex)
            {
                //Declara variable 
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
                string qry = "DELETE FROM Agenda where idAgenda ='" + idA + "'";
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
    

