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
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();

            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEstudiante, "estudiantes");
                cmd.ExecuteNonQuery();
                tbEstudiante = dsEstudiante.Tables["estudiantes"];

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
                string txt = ex.Message;
            }

            return flag;
        }

        public string actualizarEstudiante(string nom, string ap1, string ap2, string fecha, string gen, string correo, string con, string ced)

        {

            string flag = "";



            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con1;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();


            try
            {
                con1 = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con1.Open();
                string qry = "UPDATE Estudiantes set nombre = '" + nom + "', apellido1 = '" + ap1 + "', apellido2 = '" + ap2 + "', fechaNacimiento = '" + fecha + "', genero = '" + gen + "', correo = '" + correo + "',contrasenna = '" + con + "' where cedula = " + ced + "and activo = 1 ";
                cmd.CommandText = qry;
                cmd.Connection = con1;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEstudiante, "Estudiantes");
                cmd.ExecuteNonQuery();

                tbEstudiante = dsEstudiante.Tables["Estudiantes"];


                this.LogIn(correo, con);
            }
            catch (Exception ex)
            {
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
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            Equipos equipos = new Equipos();
            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT e.idEquipo, e.categoria, e.genero " +
                               "FROM Equipos e, EstudianteEquipo eq " +
                               "WHERE  eq.cedEstudiante = " + ced + " " +
                               "AND e.idEquipo = eq.idEquipo";

                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                Adaptador1.Fill(ds1, "EstudianteEquipo");


                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];
                dt1 = ds1.Tables["EstudianteEquipo"];



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
                Console.WriteLine(ex.ToString());
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

                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                List<EstEntrenador> list = new List<EstEntrenador>();
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();


                string qry =
                    "SELECT eq.idEquipo, eq.categoria , eq.genero, " +
                    "concat(es.nombre, ' ', es.apellido1, ' ', es.apellido2) as Nombre, " +
                    "es.cedula " +
                    "from EstudianteEquipo ee, Estudiantes es, Equipos eq " +
                    "where es.cedula = ee.cedEstudiante " +
                    "and ee.idEquipo = eq.idEquipo " +
                    "and eq.idEquipo in ( " +
                    "SELECT eq.idEquipo " +
                    "FROM EstudianteEquipo ee, Equipos eq, Estudiantes es " +
                    " WHERE es.cedula =" + ced + " " +
                    "and es.cedula = ee.cedEstudiante " +
                    "and ee.idEquipo = eq.idEquipo); ";

                //string qry = "select ent.nombre from Entrenador ent, Equipos e where ent.cedula = e.cedEntrenador";

                cmd.CommandText = qry;
                cmd.Connection = con;

                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;

                // Adaptador.Fill(ds, "Estudiantes");
                Adaptador2.Fill(ds2, "Estudiantes");
                Adaptador1.Fill(ds1, "Equipos");
                Adaptador.Fill(ds, "EstudianteEquipo");

                cmd.ExecuteNonQuery();

                dt2 = ds2.Tables["Estudiantes"];
                dt = ds.Tables["EstudianteEquipo"];
                dt1 = ds1.Tables["Equipos"];

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



        public EstudiantePago EstudianteEstadoPago(string ced)
        {
            string flag = "";

            Console.WriteLine(ced);

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            EstudiantePago estadoPago = new EstudiantePago();

            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT p.idPago, p.monto, p.fechaPago, p.pagoRealizado " +
                             "FROM Estudiantes es, Pagos p " +
                             "WHERE es.cedula = " + ced + " " +
                             "AND es.cedula = p.cedEstudiante";

                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador.Fill(ds, "Estudiantes");
                Adaptador1.Fill(ds1, "Pagos");


                cmd.ExecuteNonQuery();

                dt = ds.Tables["Estudiantes"];
                dt1 = ds1.Tables["Pagos"];



                foreach (DataRow drCurrent in dt1.Rows)
                {
                    estadoPago.setIdPago(drCurrent["idPago"].ToString());
                    estadoPago.setMonto(drCurrent["monto"].ToString());
                    estadoPago.setFechaPago(drCurrent["fechaPago"].ToString());
                    estadoPago.setPagoRealizado(Int32.Parse(drCurrent["pagoRealizado"].ToString()));

                    Console.WriteLine("Hola mundo");
                }

                return estadoPago;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return estadoPago;

            }

        }
    }
}