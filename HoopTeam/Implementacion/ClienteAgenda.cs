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

                //Declara la variable tipo DataAdapter
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

                //Representar tablas dataset 
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();

                //Crea lista
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

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
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
                //Declara variable 
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

                //Comando SQL para traer todos los eventos del calendario del entrenador respectivo
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

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
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

                foreach (DataRow drCurrent in dt3.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
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
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                //Declara variable 
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

                //Comando SQL para conseguir todos los eventos del calendario
                string qry = "SELECT ag.idAgenda, concat (eq.categoria, ' ', eq.genero) as Equipo, ag.idCanchas, ag.fechayHora, " +
                              "ag.descripcion, ca.ubicacion " +
                              "from Agenda ag, Canchas ca, Equipos eq " +
                              "where ag.idCanchas = ca.idCanchas " +
                              "and ag.idEquipo = eq.idEquipo;";

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
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

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
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
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                //Declara varibale 
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

                //Comando SQL para traer los equipos a cargo del entrenador respectivo
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
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    //Agrega objeto a la lista 
                    list.Add(eq);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                //Declara variable
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

                //Comando SQL para traer todas las canchas
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
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Cancha cn = new Cancha();
                    cn.idCancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    cn.ubicacion = drCurrent["ubicacion"].ToString();

                    //Agrega objeto a la lista 
                    list.Add(cn);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                //Declara variable 
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

                string qry = "SELECT * FROM Agenda where idAgenda = '" + idA + "'";
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
            catch (Exception ex)
            {
                //Declara variable 
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

                //Comando SQL para agregar un evento al calendario o agenda con los datos ingresados por el usuario
                string qry = "INSERT INTO Agenda (idEquipo,idCanchas,fechayHora,descripcion)values( '" + idEqp + "', '" + idCn + "', '" + fechaHora + "','" + dcp + "')";
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

        //Método para revisar si la hora y lugar a agendar ya está reservada en la base de datos
        public int VerificarAgenda(string idCn, string fecha, string hora)
        {
            int contador=0;
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

                //Comando SQL para revisar si ya hay eventos en la agenda en las condiciones seleccionadas
                string qry = "Select count(*) from Agenda " +
                    "where DATE_FORMAT(fechaYHora, '%Y-%m-%d') = '" + fecha + "' AND( " +
                    "DATE_FORMAT(fechaYHora, '%H:%i:%s') = '" + hora + "'" +
                    "OR(DATE_FORMAT(fechaYHora, '%H:%i:%s') < '" + hora + "' AND addtime(DATE_FORMAT(fechaYHora, '%H:%i:%s'), '2:00:00') > '" + hora + "') " +
                    "OR(DATE_FORMAT(fechaYHora, '%H:%i:%s') < addtime('" + hora + "', '2:00:00') AND addtime(DATE_FORMAT(fechaYHora, '%H:%i:%s'), '2:00:00') > addtime('" + hora + "', '2:00:00')) " +
                    "OR(DATE_FORMAT(fechaYHora, '%H:%i:%s') > '" + hora + "' AND addtime(DATE_FORMAT(fechaYHora, '%H:%i:%s'), '2:00:00') < addtime('" + hora + "', '2:00:00')) " +
                    "OR(DATE_FORMAT(fechaYHora, '%H:%i:%s') < '" + hora + "' AND addtime(DATE_FORMAT(fechaYHora, '%H:%i:%s'), '2:00:00') > addtime('" + hora + "', '2:00:00')) " +
                    ") " +
                    "and idCanchas = " + idCn + ";";
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Agenda");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Agenda"];

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado añade al contador

                    contador = Int32.Parse(drCurrent["count(*)"].ToString());

                }
                return contador;//Regresa el valor del contador
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                return 1;
            }
        }

        //Método para editar los detalles de un evento del calendario

        public void EditarAgenda(string idA, string descripcion, string fechaHora)
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
                string qry = "UPDATE Agenda set fechayHora = '" + fechaHora + "', descripcion = '" + descripcion + "' where idAgenda= " + idA + ";";
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

        }

        //Método para eliminar un evento de la agenda
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

                //Comando SQL para eliminar el evento que el usuario selecciona
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


