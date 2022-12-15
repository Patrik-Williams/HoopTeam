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
        //Creación de objetos usando los modelos respectivos
        Agenda agn = new Agenda();
        Cancha cancha = new Cancha();

        //Método que devuelve una lista de agenda o calendario del estudiante seleccionado
        public List<Agenda> GetAgendaEstudiante(string est)
        {
            try//Modelo try-catch se usa para detectar errores de SQL y mencionarselos al usuario si fuera necesario
            {

                //Objetos de MySQL para conectar con la base de datos. Se usa a través de todo el cliente

                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion

                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador3 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador4 = new MySqlDataAdapter();

                //Los DataSet y DataTable se usan a través del código para llenar tablas dentro del cliente con resultados de bases de datos,
                // y las tablas de los resultados llenan los datos de la aplicación. Se usan a través de todo el cliente.
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

                //Este comando establece la sesión con la base de datos. Se usan a través de todo el cliente.
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Comando SQL para traer todos los eventos del calendario del estudiante respectivo
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

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
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

                foreach (DataRow drCurrent in dt3.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    //Se añade la fila con el objeto a la lista
                    list.Add(ag);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<Agenda>();
            }
        }

        //Método que devuelve una lista de agenda o calendario del entrenador seleccionado
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

                //Comando SQL para traer todos los eventos del calendario del entrenador respectivo
                string qry = "select ag.idAgenda, concat(eq.categoria, ' ', eq.genero) as Equipo, ag.descripcion, ag.fechayHora, ca.idCanchas, ca.ubicacion " +
                "from Entrenador en, Equipos eq, Agenda ag, Canchas ca " +
                "where en.cedula =" + ent + " " +
                "and en.cedula = eq.cedEntrenador " +
                "and eq.idEquipo = ag.idEquipo " +
                "and ag.idCanchas = ca.idCanchas; ";

                cmd.CommandText = qry;
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
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

                foreach (DataRow drCurrent in dt3.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    list.Add(ag);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
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

                //Comando SQL para conseguir todos los eventos del calendario
                string qry = "SELECT ag.idAgenda, concat (eq.categoria, ' ', eq.genero) as Equipo, ag.idCanchas, ag.fechayHora, " +
                              "ag.descripcion, ca.ubicacion " +
                              "from Agenda ag, Canchas ca, Equipos eq " +
                              "where ag.idCanchas = ca.idCanchas " +
                              "and ag.idEquipo = eq.idEquipo;";

                cmd.CommandText = qry;
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
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

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Agenda ag = new Agenda();
                    ag.idAgenda = drCurrent["idAgenda"].ToString();
                    ag.Equipo = drCurrent["Equipo"].ToString();
                    ag.Cancha = drCurrent["idCanchas"].ToString();
                    ag.Ubicacion = drCurrent["ubicacion"].ToString();
                    ag.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    ag.Descripcion = drCurrent["descripcion"].ToString();

                    list.Add(ag);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<Agenda>();
            }
        }

        //Método para conseguir los equipos a cargo de un entrenador
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

                //Comando SQL para traer los equipos a cargo del entrenador respectivo
                string qry = "SELECT * FROM Equipos Where cedEntrenador= '" + ent + "' and activo = 1 ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    list.Add(eq);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<Equipos>();
            }
        }

        //Método que hace una lista de todas las canchas
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

                //Comando SQL para traer todas las canchas
                string qry = "SELECT * FROM Canchas; ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Canchas");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Canchas"];

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Cancha cn = new Cancha();
                    cn.idCancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    cn.ubicacion = drCurrent["ubicacion"].ToString();


                    list.Add(cn);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<Cancha>();
            }
        }

        //NO HAY REFERENCIAS!!!
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

                string qry = "SELECT * FROM Agenda where idAgenda = '" + idA + "'";
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
                    agn.FechaHora = DateTime.Parse(drCurrent["fechayHora"].ToString());
                    agn.Descripcion = drCurrent["descripcion"].ToString();

                    flag = "Agn";
                }

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
            return flag;
        }

        //Método para agregar un evento al calendario o agenda
        public void AgregarAgenda(string idEqp, string idCn, string fechaHora, string dcp)
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

                //Comando SQL para agregar un evento al calendario o agenda con los datos ingresados por el usuario
                string qry = "INSERT INTO Agenda (idEquipo,idCanchas,fechayHora,descripcion)values( '" + idEqp + "', '" + idCn + "', '" + fechaHora + "','" + dcp + "')";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
        }

        //Método para editar los detalles de un evento del calendario
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

                //Comando SQL para actualizar los detalles de un evento del calendario con los datos que el usuario ingresa
                string qry = "UPDATE Agenda set descripcion = '" + descripcion + "' where idAgenda= " + idA + ";";

                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Agenda");
                cmd.ExecuteNonQuery();
                
                dt = ds.Tables["Agenda"];

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
            return flag;
        }

        //Método para eliminar un evento de la agenda
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

                //Comando SQL para eliminar el evento que el usuario selecciona
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


