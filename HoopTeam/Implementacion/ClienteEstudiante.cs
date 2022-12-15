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
        //Creación de un objeto tipo EstudianteEstatico usando el modelo respectivo
        //El EstudianteEstatico se define en el modelo, es para la sesión
        EstudianteEstatico est = new EstudianteEstatico();

        //Objetos de MySQL para conectar con la base de datos. Se usa a través de todo el cliente
        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public ClienteEstudiante()
        {
        }

        //Método para verificar el ingreso a la sesión de un estudiante
        public string LogIn(string correo, string contra)
        {
            string flag = "";


            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            //Los DataSet y DataTable se usan a través del código para llenar tablas dentro del cliente con resultados de bases de datos,
            // y las tablas de los resultados llenan los datos de la aplicación. Se usan a través de todo el cliente.
            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();

            try//Modelo try-catch se usa para detectar errores de SQL y mencionarselos al usuario si fuera necesario. Se usa a través de todo el cliente
            {
                //Este comando establece la sesión con la base de datos. Se usan a través de todo el cliente.
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                cmd.CommandText = qry;
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEstudiante, "estudiantes");
                cmd.ExecuteNonQuery();
                tbEstudiante = dsEstudiante.Tables["estudiantes"];

                foreach (DataRow drCurrent in tbEstudiante.Rows)
                {//Para llenar los datos del estudiante de la sesión, se llenan con los datos de la base
                    est.setCedula(drCurrent["cedula"].ToString());
                    est.setNombre(drCurrent["nombre"].ToString());
                    est.setApellido1(drCurrent["apellido1"].ToString());
                    est.setApellido2(drCurrent["apellido2"].ToString());
                    est.setNacimiento(drCurrent["fechaNacimiento"].ToString());
                    est.setGenero(drCurrent["genero"].ToString());
                    est.setCorreo(drCurrent["correo"].ToString());
                    est.setContrasenna(drCurrent["contrasenna"].ToString());

                    //Variable que establece que la sesión es de un estudiante
                    flag = "Est";
                }
            }
            catch (Exception ex)

            {
                string txt = ex.Message;
            }

            return flag;
        }

        //Método para que el estudiante edite o actualice sus datos
        public string actualizarEstudiante(string nom, string ap1, string ap2, string gen, string correo, string con, string ced)

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
                
                //Comando SQL que actualiza la información del estudiante con los datos que ingresa
                string qry = "UPDATE Estudiantes set nombre = '" + nom + "', apellido1 = '" + ap1 + "', apellido2 = '" + ap2 + "', genero = '" + gen + "', correo = '" + correo + "',contrasenna = '" + con + "' where cedula = " + ced + " ";
                cmd.CommandText = qry;
                cmd.Connection = con1;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEstudiante, "Estudiantes");
                cmd.ExecuteNonQuery();

                tbEstudiante = dsEstudiante.Tables["Estudiantes"];

                //Instrucción para asegurar que la sesión del estudiante siga activa al actualizar los datos
                this.LogIn(correo, con);
            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
            return flag;

        }

        //Método que indica el equipo de un estudiante específico
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

                //Comando SQL que trae los datos del equipo en el que está el estudiante seleccionado
                string qry = "SELECT e.idEquipo, e.categoria, e.genero " +
                               "FROM Equipos e, EstudianteEquipo eq " +
                               "WHERE  eq.cedEstudiante = " + ced + " " +
                               "AND e.idEquipo = eq.idEquipo";

                cmd.CommandText = qry;
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                Adaptador1.Fill(ds1, "EstudianteEquipo");


                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];
                dt1 = ds1.Tables["EstudianteEquipo"];

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena los datos del objeto
                    equipos.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    equipos.categoria = drCurrent["categoria"].ToString();
                    equipos.genero = drCurrent["genero"].ToString();
                }
                return equipos;//Regresa el objeto creado a partir de la base de datos
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //Si no hay resultados, devuelve un objeto en blanco para evitar errores
                return equipos;

            }

        }

        //Método que trae una lista de estudiantes que pertenecen al equipo de un estudiante específico
        public List<EstEntrenador> GetEstEquipo(string ced)
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

                //Lista de tipo Estudiantes por entrenador, para poder tener los datos de los estudiantes y su equipo al mismo tiempo
                List<EstEntrenador> list = new List<EstEntrenador>();

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Comando SQL para seleccionar a todos los estudiantes en el equipo del estudiante seleccionado
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

                cmd.CommandText = qry;
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;

                Adaptador2.Fill(ds2, "Estudiantes");
                Adaptador1.Fill(ds1, "Equipos");
                Adaptador.Fill(ds, "EstudianteEquipo");

                cmd.ExecuteNonQuery();

                dt2 = ds2.Tables["Estudiantes"];
                dt = ds.Tables["EstudianteEquipo"];
                dt1 = ds1.Tables["Equipos"];

                foreach (DataRow drCurrent in dt2.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    EstEntrenador est = new EstEntrenador();
                    est.Cedula = drCurrent["Cedula"].ToString();
                    est.NombreCompleto = drCurrent["Nombre"].ToString();
                    est.IdEquipo = drCurrent["idEquipo"].ToString();
                    est.Categoria = drCurrent["categoria"].ToString();
                    est.Genero = drCurrent["genero"].ToString();

                    list.Add(est);//Se añade la fila con el objeto a la lista
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<EstEntrenador>();
            }
        }

        //Método que trae los datos del pago de un estudiante específico
        public EstudiantePago EstudianteEstadoPago(string ced)
        {
            string flag = "";

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlDataAdapter Adaptador1 = new MySqlDataAdapter();

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            //Objeto de tipo EstudiantePago que se define en el modelo
            EstudiantePago estadoPago = new EstudiantePago();

            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                //Comando SQL que trae los datos del pago del estudiante seleccionado
                string qry = "SELECT p.idPago, p.monto, p.fechaPago, p.pagoRealizado " +
                             "FROM Estudiantes es, Pagos p " +
                             "WHERE es.cedula = " + ced + " " +
                             "AND es.cedula = p.cedEstudiante";

                cmd.CommandText = qry;
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador.Fill(ds, "Estudiantes");
                Adaptador1.Fill(ds1, "Pagos");


                cmd.ExecuteNonQuery();

                dt = ds.Tables["Estudiantes"];
                dt1 = ds1.Tables["Pagos"];

                foreach (DataRow drCurrent in dt1.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena el objeto
                    estadoPago.setIdPago(drCurrent["idPago"].ToString());
                    estadoPago.setMonto(drCurrent["monto"].ToString());
                    estadoPago.setFechaPago(drCurrent["fechaPago"].ToString());
                    estadoPago.setPagoRealizado(Int32.Parse(drCurrent["pagoRealizado"].ToString()));

                    Console.WriteLine("Hola mundo");
                }

                return estadoPago;//Regresa el objeto creado a partir de la base de datos 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //Si no hay resultados, devuelve un objeto en blanco para evitar errores
                return estadoPago;

            }

        }
    }
}