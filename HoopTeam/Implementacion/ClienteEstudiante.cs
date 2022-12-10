using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using HoopTeam.Modelo.Entrenadores;
using System.Diagnostics;
using HoopTeam.Modelo.Estudiantes;
using HoopTeam.Modelo;

namespace HoopTeam.Implementacion
{
    class ClienteEstudiante
    {
        EstudianteEstatico est = new EstudianteEstatico();
        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        //Declara la variable tipo DataAdapter
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public ClienteEstudiante()
        {
        }

        //actualizar datos del estudiante
        public string LogIn(string correo, string contra)
        {
            string flag = "";


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            //Declara la variable tipo DataAdapter
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            //objeto para almacenar datos
            DataSet dsEstudiante = new DataSet();
            //Representar tablas dataset 
            DataTable tbEstudiante = new DataTable();

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
                string qry = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion 
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(dsEstudiante, "estudiantes");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                tbEstudiante = dsEstudiante.Tables["estudiantes"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in tbEstudiante.Rows)
                {
                    est.setCedula(drCurrent["cedula"].ToString());
                    est.setNombre(drCurrent["nombre"].ToString());
                    est.setApellido1(drCurrent["apellido1"].ToString());
                    est.setApellido2(drCurrent["apellido2"].ToString());
                    est.setNacimiento(drCurrent["fechaNacimiento"].ToString());
                    est.setGenero(drCurrent["genero"].ToString());
                    est.setCorreo(drCurrent["correo"].ToString());
                    est.setContrasenna(drCurrent["contrasenna"].ToString());

                    flag = "Est";
                }
            }
            catch (Exception ex)

            {
                //declara variable 
                string txt = ex.Message;
            }

            return flag;
        }

        public string actualizarEstudiante(string nom, string ap1, string ap2, string gen, string correo, string con, string ced)

        {

            string flag = "";



            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con1;//conexion
            //Declara la variable tipo DataAdapter
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            //objeto para almacenar datos
            DataSet dsEstudiante = new DataSet();
            //Representar tablas dataset 
            DataTable tbEstudiante = new DataTable();


            try
            {
                //conexion base de datos 
                con1 = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con1.Open();
                //consulta base de datos 
                string qry = "UPDATE Estudiantes set nombre = '" + nom + "', apellido1 = '" + ap1 + "', apellido2 = '" + ap2 + "', genero = '" + gen + "', correo = '" + correo + "',contrasenna = '" + con + "' where cedula = " + ced + " ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con1;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(dsEstudiante, "Estudiantes");
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                tbEstudiante = dsEstudiante.Tables["Estudiantes"];

                //Vuleve a realizar el login para actualizar los datos 
                this.LogIn(correo, con);
            }
            catch (Exception ex)
            {
                //declara variable
                string txt = ex.Message;

            }
            return flag;

        }

        public Equipos getEquipo(string ced)
        {
            string flag = "";

            Console.WriteLine(ced);

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            //Declara la variable tipo DataAdapter
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();

            //objeto para almacenar datos
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            //Representar tablas dataset 
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            Equipos equipos = new Equipos();
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
                string qry = "SELECT e.idEquipo, e.categoria, e.genero " +
                               "FROM Equipos e, EstudianteEquipo eq " +
                               "WHERE  eq.cedEstudiante = " + ced + " " +
                               "AND e.idEquipo = eq.idEquipo";

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Equipos");
                Adaptador1.Fill(ds1, "EstudianteEquipo");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Equipos"];
                dt1 = ds1.Tables["EstudianteEquipo"];


                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {
                    equipos.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    equipos.categoria = drCurrent["categoria"].ToString();
                    equipos.genero = drCurrent["genero"].ToString();

                    Console.WriteLine("Hola mundo");
                }
                return equipos;
            }
            catch (Exception ex)
            {
                //Escribe una linea en la consola 
                Console.WriteLine(ex.ToString());
                //Retorna el obajeto equipos 
                return equipos;

            }

        }

        public List<EstEntrenador> GetEstEquipo(string ced)
        {
            Console.WriteLine(ced);
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

                //Crea una nueva lista de EstEntrenador
                List<EstEntrenador> list = new List<EstEntrenador>();
                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //consulta base de datos 
                string qry =
                    "SELECT eq.idEquipo, eq.categoria , eq.genero, " +
                    "concat(es.nombre, ' ', es.apellido1, ' ', es.apellido2) as Nombre, " +
                    "es.cedula " +
                    "from EstudianteEquipo ee, Estudiantes es, Equipos eq " +
                    "where es.cedula = ee.cedEstudiante " +
                    "and ee.idEquipo = eq.idEquipo " +
                    "and es.activo = 1 " +
                    "and ee.activo = 1 " +
                    "and eq.idEquipo in ( " +
                    "SELECT eq.idEquipo " +
                    "FROM EstudianteEquipo ee, Equipos eq, Estudiantes es " +
                    " WHERE es.cedula =" + ced + " " +
                    "and ee.activo = 1 " +
                    "and es.cedula = ee.cedEstudiante " +
                    "and ee.idEquipo = eq.idEquipo); ";

                //string qry = "select ent.nombre from Entrenador ent, Equipos e where ent.cedula = e.cedEntrenador";

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;

                //Indica la tabla con la que se llena el dataset
                Adaptador2.Fill(ds2, "Estudiantes");
                Adaptador1.Fill(ds1, "Equipos");
                Adaptador.Fill(ds, "EstudianteEquipo");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt2 = ds2.Tables["Estudiantes"];
                dt = ds.Tables["EstudianteEquipo"];
                dt1 = ds1.Tables["Equipos"];

                //Hacer un dt y un ds para cada una de las tablas de la consulta

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt2.Rows)
                {
                    //Crea un nuevo objeto 
                    EstEntrenador est = new EstEntrenador();
                    est.Cedula = drCurrent["Cedula"].ToString();
                    est.NombreCompleto = drCurrent["Nombre"].ToString();
                    est.IdEquipo = drCurrent["idEquipo"].ToString();
                    est.Categoria = drCurrent["categoria"].ToString();
                    est.Genero = drCurrent["genero"].ToString();
                    
                    //Agrega el objeto a la lista
                    list.Add(est);

                    //Agrega un mensaje a la consola 
                    Console.WriteLine("Hola mundo");
                }
                //Retorna la lista 
                return list;
            }
            catch (Exception ex)
            {
                //declara variable
                string txt = ex.Message;
                //Retorna la nueva lista
                return new List<EstEntrenador>();
            }
        }



        public EstudiantePago EstudianteEstadoPago(string ced)
        {
            string flag = "";

            Console.WriteLine(ced);

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            //Declara la variable tipo DataAdapter
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();

            //objeto para almacenar datos
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            //Representar tablas dataset 
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            //Crea un objeto
            EstudiantePago estadoPago = new EstudiantePago();

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
                string qry = "SELECT p.idPago, p.monto, p.fechaPago, p.pagoRealizado " +
                             "FROM Estudiantes es, Pagos p " +
                             "WHERE es.cedula = " + ced + " " +
                             "AND es.cedula = p.cedEstudiante";

                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Estudiantes");
                Adaptador1.Fill(ds1, "Pagos");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Estudiantes"];
                dt1 = ds1.Tables["Pagos"];


                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt1.Rows)
                {
                    estadoPago.setIdPago(drCurrent["idPago"].ToString());
                    estadoPago.setMonto(drCurrent["monto"].ToString());
                    estadoPago.setFechaPago(drCurrent["fechaPago"].ToString());
                    estadoPago.setPagoRealizado(Int32.Parse(drCurrent["pagoRealizado"].ToString()));

                    //Crea  mensaje en consola 
                    Console.WriteLine("Hola mundo");
                }

                //Retorna objeto
                return estadoPago;
            }
            catch (Exception ex)
            {
                //Agrega un mensaje a la consola 
                Console.WriteLine(ex.ToString());
                //Retorna objeto
                return estadoPago;

            }

        }
    }
}