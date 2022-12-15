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
        //Creación de un objeto tipo Entrenador usando el modelo respectivo
        Entrenador ent = new Entrenador();

        //Objetos de MySQL para conectar con la base de datos. Se usa a través de todo el cliente
        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        //Declara la variable tipo DataAdapter
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public ClienteEntrenador()
        {
        }

        //Método para verificar el ingreso a la sesión de un entrenador
        public string LogIn(string correo, string contra)
        {
            string flag = " ";

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            //Declara la variable tipo DataAdapter
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();

            //Los DataSet y DataTable se usan a través del código para llenar tablas dentro del cliente con resultados de bases de datos,
            // y las tablas de los resultados llenan los datos de la aplicación. Se usan a través de todo el cliente.
            DataSet ds = new DataSet();
            //Representar tablas dataset 
            DataTable dt = new DataTable();

            try//Modelo try-catch se usa para detectar errores de SQL y mencionarselos al usuario si fuera necesario. Se usa a través de todo el cliente
            {
                //Este comando establece la sesión con la base de datos. Se usan a través de todo el cliente.
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                       "port = 3306; " +
                                       "username = admin; " +
                                       "password = hoopteamAdmin;" +
                                       "database =HoopTeam");
                con.Open();

                //Comando SQL para conseguir el correo y contraseña del entrenador respectivo, para hacer la comparación para ingresar a la sesión
                string qry = "SELECT * FROM Entrenador where correo= '" + correo + "'and contrasenna= '" + contra + "'";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando

                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Entrenador");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Entrenador"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que llena los datos del entrenador si se verificó su sesión
                    ent.setCedula(drCurrent["cedula"].ToString());
                    ent.setNombre(drCurrent["nombre"].ToString());
                    ent.setApellido1(drCurrent["apellido1"].ToString());
                    ent.setApellido2(drCurrent["apellido2"].ToString());
                    ent.setCorreo(drCurrent["Correo"].ToString());
                    ent.setContrasenna(drCurrent["Contraseña"].ToString());


                    //El uso de la variable y darle el siguiente valor determina que la sesión activa es de un entrenador 
                    flag = "Ent";
                }

            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;
            }
            return flag;
        }

        //Método para que el entrenador pueda actualizar o editar sus datos
        public string actualizarEntrenador(string Nom, string apE1, string apE2, string cor, string con, string ced)
        {
            string flag = " ";

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con1;//conexion
            //Declara la variable tipo DataAdapter
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            //objeto para almacenar datos
            DataSet ds = new DataSet();
            // Representar tablas dataset
            DataTable dt = new DataTable();
            try
            {
                //conexion base de datos 
                con1 = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                   "port = 3306; " +
                                   "username = admin; " +
                                   "password = hoopteamAdmin;" +
                                   "database =HoopTeam");
                con1.Open();

                //Comando SQL que actualiza los datos del entrenador con los datos que el usuario proporciona
                string qry = "UPDATE Entrenador set nombre= '" + Nom + "', apellido1 = '" + apE1 + "', apellido2 = '" + apE2 + "', correo = '" + cor + "', contrasenna ='" + con + "' where cedula = " + ced + " ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con1;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Entrenador");

                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Entrenador"];

                //Reestablece los datos del entrenador en su sesión para mantenerla activa
                this.LogIn(cor, con);
            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;
            }
            return flag;
        }

        //Método que trae una lista de todos los estudiantes activos en la base de datos
        public List<Estudiante> GetEstudiantes()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                //objeto para almacenar datos
                DataSet ds = new DataSet();
                // Representar tablas dataset
                DataTable dt = new DataTable();

                //se establece una lista de tipo Estudiante para llenar los datos
                List<Estudiante> list = new List<Estudiante>();
                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database = HoopTeam;" +
                                          "Convert Zero Datetime=True;");
                con.Open();

                //Comando SQL que trae todos los estudiantes activos
                string qry = "SELECT * FROM Estudiantes where activo = 1";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Estudiantes");
                //Ejecuta el query
                cmd.ExecuteNonQuery();


                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Estudiantes"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla

                    Estudiante est = new Estudiante();
                    est.Cedula = drCurrent["cedula"].ToString();
                    est.Nombre = drCurrent["nombre"].ToString();
                    est.Apellido1 = drCurrent["apellido1"].ToString();
                    est.Apellido2 = drCurrent["apellido2"].ToString();
                    est.NombreCompleto = est.Nombre + " " + est.Apellido1 + " " + est.Apellido2;
                    est.Nacimiento = DateTime.Parse(drCurrent["fechaNacimiento"].ToString());
                    est.Genero = drCurrent["genero"].ToString();
                    est.Correo = drCurrent["correo"].ToString();
                    est.Contrasenna = drCurrent["contrasenna"].ToString();


                    //Agrega objeto a la lista 
                    list.Add(est);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<Estudiante>();
            }
        }

        //Método que trae todos los estudiantes de todos equipos bajo un entrenador seleccionado
        public List<EstEntrenador> GetEstEntrenador(string ent)
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

                // Representar tablas dataset
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();

                //Declaración de una lista que se va a llenar con datos de tipo Estudiante de un entrenador 
                //Para combinar el equipo con el estudiante, en el modelo se declara el objeto de Estudiante por Entrenador (EstEntrenador)
                List<EstEntrenador> list = new List<EstEntrenador>();
                //conexion base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Consulta SQL que trae todos los estudiantes y el equipo respectivo de cada uno para el entrenador seleccionado
                string qry = "SELECT es.cedula as Cedula, concat(es.nombre, ' ', es.apellido1, ' ', es.apellido2) as Nombre ,eq.idEquipo, eq.categoria, eq.genero, p.pagoRealizado " +
                    "FROM Estudiantes es, Entrenador en, Equipos eq, EstudianteEquipo ee, Pagos p " +
                    "WHERE en.cedula = " + ent + " " +
                    "AND en.cedula = eq.cedEntrenador " +
                    "AND eq.idequipo = ee.idequipo " +
                    "AND ee.cedestudiante = es.cedula " +
                    "AND ee.activo = 1 " +
                    "AND es.activo = 1 " +
                    "AND p.cedEstudiante = es.cedula;";

                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador1.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                Adaptador3.SelectCommand = cmd;
                Adaptador4.SelectCommand = cmd;

                Adaptador3.Fill(ds, "EstudianteEquipo");
                Adaptador2.Fill(ds2, "Estudiantes");
                Adaptador.Fill(ds, "Entrenador");
                Adaptador1.Fill(ds1, "Equipos");
                Adaptador4.Fill(ds3, "Pagos");

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt2 = ds2.Tables["Estudiantes"];
                dt = ds.Tables["Entrenador"];
                dt1 = ds1.Tables["Equipos"];
                dt3 = ds3.Tables["EstudianteEquipo"];
                dt4 = ds4.Tables["Pagos"];

                foreach (DataRow drCurrent in dt2.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    EstEntrenador est = new EstEntrenador();
                    est.Cedula = drCurrent["Cedula"].ToString();
                    est.NombreCompleto = drCurrent["Nombre"].ToString();
                    est.IdEquipo = drCurrent["idEquipo"].ToString();
                    est.Categoria = drCurrent["categoria"].ToString();
                    est.Genero = drCurrent["genero"].ToString();

                    //Dado que la columna "pagoRealizado" es un booleano en la base de datos, aquí se convierte a un string para la vista del usuario
                    if (Int32.Parse(drCurrent["pagoRealizado"].ToString()) == 1)
                    {
                        est.EstadoPago = "Realizado";
                    }
                    else
                    {
                        est.EstadoPago = "Por realizar";
                    }

                    //Se añade la fila con el objeto a la lista
                    list.Add(est);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos
            }
            catch (Exception ex)
            {
                //Declara variable 
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<EstEntrenador>();
            }
        }

        //Método que retorna una lista de todos los equipos en la base de datos
        public List<Equipos> GetTodosEquipos()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                MySqlDataAdapter Adaptador2 = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                // Representar tablas dataset
                DataTable dt = new DataTable();

                DataSet ds2 = new DataSet();
                DataTable dt2 = new DataTable();

                //Lista de tipo equipo
                List<Equipos> list = new List<Equipos>();
                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Consulta SQL que trae todos los equipos en la base
                string qry = "SELECT e.idEquipo, e.categoria, e.genero, e.cedEntrenador, e.cupo, concat(et.nombre, ' ', et.apellido1, ' ', et.apellido2) as Nombre FROM Equipos e, Entrenador et " +
                    "WHERE e.cedEntrenador = et.cedula and e.activo = 1;";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador2.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, "Equipos");
                Adaptador2.Fill(ds2, "Entrenador");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables["Equipos"];
                dt2 = ds2.Tables["Entrenador"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Equipos eq = new Equipos();
                    eq.idEquipo = Int32.Parse(drCurrent["idEquipo"].ToString());
                    eq.categoria = drCurrent["categoria"].ToString();
                    eq.genero = drCurrent["genero"].ToString();
                    eq.cedEntrenador = Int32.Parse(drCurrent["cedEntrenador"].ToString());
                    eq.Entrenador = drCurrent["Nombre"].ToString();
                    eq.cupo = Int32.Parse(drCurrent["cupo"].ToString());

                    //Se añade la fila con el objeto a la lista
                    list.Add(eq);
                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                //Declara varibale 
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<Equipos>();
            }
        }

        //Método que trae todos los equipos de un entrenador
        public List<Equipos> GetEquipos(string ent)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                // Representar tablas dataset
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

                //Consulta SQL que trae los datos de los equipos con el entrenador seleccionado
                string qry = "SELECT * FROM Equipos Where cedEntrenador = " + ent + " and activo = 1";
                //convertir qry en comando
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
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

                    //Se añade la fila con el objeto a la lista
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

        //Método que trae la información de un estudiante específico
        public Estudiante GetEstudiante(string ced)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                //Creación de objeto de tipo estudiante
                Estudiante est = new Estudiante();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                // Representar tablas dataset
                DataTable dt = new DataTable();

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Comando SQL que trae el estudiante con la cédula seleccionada
                string qry = "SELECT * FROM Estudiantes Where cedula = " + ced + " and activo = 1 ";
                cmd.CommandText = qry;
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Estudiantes");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Estudiantes"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    est.Cedula = drCurrent["cedula"].ToString();
                    est.Nombre = drCurrent["nombre"].ToString();
                    est.Apellido1 = drCurrent["apellido1"].ToString();
                    est.Apellido2 = drCurrent["apellido2"].ToString();
                    est.Genero = drCurrent["genero"].ToString();
                    est.Nacimiento = DateTime.Parse(drCurrent["fechaNacimiento"].ToString());
                    est.Correo = drCurrent["correo"].ToString();
                    est.Contrasenna = drCurrent["contrasenna"].ToString();
                }
                return est;//Regresa el objeto creado a partir de la base de datos
            }
            catch (Exception ex)
            {
                //Declara variable
                string txt = ex.Message;
                //Si no hay resultados, devuelve un objeto en blanco para evitar errores
                return new Estudiante();
            }
        }

        //Método para buscar los equipos de un género específico
        public List<Equipos> GetEquiposGenero(string gen)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                //objeto para almacenar datos
                DataSet ds = new DataSet();
                // Representar tablas dataset
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

                //Consulta SQL donde se buscan equipos filtrados por el género respectivo
                string qry = "SELECT * FROM Equipos Where genero = '" + gen + "' and (cupo > 0) and activo = 1 ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
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

                    //Se añade la fila con el objeto a la lista
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

        //Método que trae los equipos por cada género bajo un entrenador específico
        //Es importante para cuando el entrenador ingresa un estudiante, ingresa el género y necesita ver los equipos que entrena con ese género específico
        public List<Equipos> GetEquiposGen_Ent(string gen, int ent)
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

                //Comando SQL que trae los equipos por el género seleccionado bajo el entrenador seleccionado
                string qry = "SELECT * FROM Equipos Where genero = '" + gen + "' and cedEntrenador = " + ent + " and (cupo > 0) and activo = 1;";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                ////Indica la tabla con la que se llena el dataset
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

                    //Se añade la fila con el objeto a la lista
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

        //Método para editar o actualizar la información de un estudiante específico
        public void EditarInfoEst(int ced, string nom, string ap1, string ap2, DateTime fecha, string correo, string contra, int equipoNuevo, int equipoViejo, int pago)
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

                //Comando SQL para actualizar los datos del estudiante con los valores ingresados
                string qry = "UPDATE Estudiantes set Nombre = '" + nom + "', Apellido1 ='" + ap1 + "', Apellido2 ='" + ap2 + "'" +
                    ", fechaNacimiento = '" + fecha.ToString("yyyy-MM-dd") + "', correo= '" + correo + "', " + "contrasenna = '" + contra + "' where cedula = " + ced + " ";
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //Si se cambia al estudiante de equipo, se debe añadir la conexión de estudiante al nuevo equipo
                // e inhabilitar la conexión anterior con el equipo viejo
                if (equipoNuevo != equipoViejo)
                {
                    //consulta base de datos
                    string qry2 = "INSERT INTO EstudianteEquipo (fechaInicio, cedEstudiante, idEquipo, activo )values( curdate(), " + ced + ", " + equipoNuevo + ", 1)";
                    //convertir qry en comando 
                    cmd.CommandText = qry2;
                    //convierte el string conexion en conexion
                    cmd.Connection = con;
                    //Ejecuta el query
                    cmd.ExecuteNonQuery();

                    //consulta base de datos
                    string qry6 = "UPDATE EstudianteEquipo SET activo = 0 where cedEstudiante = " + ced + " and idEquipo = " + equipoViejo + ";";
                    //convertir qry en comando 
                    cmd.CommandText = qry6;
                    //convierte el string conexion en conexion
                    cmd.Connection = con;
                    //Ejecuta el query
                    cmd.ExecuteNonQuery();
                }

                //Se actualiza el cupo de ambos equipos
                string qry3 = "UPDATE Equipos SET cupo= cupo-1 WHERE idEquipo=" + equipoNuevo + "";
                //convertir qry en comando 
                cmd.CommandText = qry3;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //consulta base de datos
                string qry4 = "UPDATE Equipos SET cupo= cupo+1 WHERE idEquipo=" + equipoViejo + "";
                //convertir qry en comando 
                cmd.CommandText = qry4;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //Se establece que se debe actualizar si el pago ha sido realizado en la base de datos
                if (pago == 1)
                {
                    //consulta base de datos
                    string qry5 = "UPDATE Pagos SET pagoRealizado = " + pago + ", fechaPago = DATE_ADD(fechaPago, INTERVAL +1 MONTH) WHERE cedEstudiante=" + ced + "";
                    //convertir qry en comando 
                    cmd.CommandText = qry5;
                    //convierte el string conexion en conexion
                    cmd.Connection = con;
                    //Ejecuta el query
                    cmd.ExecuteNonQuery();
                }
                if (pago == 0)
                {
                    //consulta base de datos
                    string qry5 = "UPDATE Pagos SET pagoRealizado = " + pago + " WHERE cedEstudiante=" + ced + "";
                    //convertir qry en comando 
                    cmd.CommandText = qry5;
                    //convierte el string conexion en conexion
                    cmd.Connection = con;

                    //Ejecuta el query
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                //Declara variable
                string txt = ex.Message;

            }
        }

        //Método para inhabilitar un estudiante
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

                //Comandos SQL para inhabilitar al estudiante y para inhabilitar su conexión con el equipo que tenía
                string qry = "update Estudiantes set activo = 0 where cedula = " + ced + " ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry2 = "UPDATE EstudianteEquipo SET activo = 0 where cedEstudiante = " + ced + " ";
                cmd.CommandText = qry2;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        //Método para ver todas las canchas en la academia
        public List<Cancha> GetCanchas()
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

                //Comando SQL que trae todas las canchas en la base
                string qry = "SELECT * FROM Canchas; ";
                cmd.CommandText = qry;
                cmd.Connection = con;

                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Canchas");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Canchas"];

                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    Cancha ca = new Cancha();
                    ca.idCancha = Int32.Parse(drCurrent["idCanchas"].ToString());
                    ca.ubicacion = drCurrent["ubicacion"].ToString();

                    //Se añade la fila con el objeto a la lista
                    list.Add(ca);

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

        //Método para agregar una nueva cancha a la base de datos
        public void AgregarCancha(string ub)
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

                //Comando SQL que recibe la información para crear una nueva cancha
                string qry = "INSERT INTO Canchas (ubicacion)values( '" + ub + "');";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
        }

        //Método para editar o actualizar una cancha
        public void EditarCancha(int id, string ub)
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

                //Comando SQL que actualiza la cancha con la información que da el usuario
                string qry = "UPDATE Canchas SET ubicacion = '" + ub + "' WHERE idCanchas =" + id + ";";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        //Método para eliminar una cancha de la base de datos
        public void EliminarCancha(int id)
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

                //Comando SQL que elimina la cancha seleccionada del sistema
                string qry = "Delete from Canchas WHERE idCanchas =" + id + ";";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                string txt = ex.Message;

                //Mensajes de error cuando la cancha no se puede eliminar al estar asociada con un evento actual del calendario
                Console.WriteLine("Error eliminando cancha");
                Console.WriteLine("Cancha asociada a una agenda" + ex);
            }
        }

        //Método para agregar un nuevo estudiante a la base de datos
        public void AgregarEstudiante(int ced, string nom, string ap1, string ap2, DateTime fecha, string gen, string correo, string contra, int equipo)
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

                //Comandos SQL para ingresar al estudiante, ingresar su conexión al equipo seleccionado,
                //  actualizar el cupo de ese equipo y también crear su detalle de pago 
                string qry = "INSERT INTO Estudiantes Values(" + ced + ", '" + nom + "', '" + ap1 + "', '" + ap2 + "', '" + fecha.ToString("yyyy-MM-dd") + "', '" + gen + "', '" + correo + "', '" + contra + "', 1);";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry2 = "INSERT INTO EstudianteEquipo (fechaInicio, cedEstudiante, idEquipo, activo )values( curdate(), " + ced + ", " + equipo + ", 1)";
                cmd.CommandText = qry2;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry3 = "UPDATE Equipos SET cupo= cupo-1 WHERE idEquipo=" + equipo + "";
                cmd.CommandText = qry3;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string qry4 = "INSERT INTO Pagos (cedEstudiante, fechaPago, pagoRealizado, monto) values (" + ced + ", curdate(), 1, 15000 );";
                cmd.CommandText = qry4;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Mensaje de error al crear al usuario
                Console.WriteLine("Error agregando estudiante" + ex);


            }
        }

        //Método para editar el cupo de un equipo
        public void EditarCupo(int cupo, int equipo)
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

                //Comando SQL para actualizar el cupo del equipo seleccionado con el número nuevo que se ingresa
                string qry = "UPDATE Equipos SET cupo= " + cupo + " WHERE idEquipo=" + equipo + "";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Mensaje de error
                Console.WriteLine("Error actualizando cupo" + ex);
            }

        }

    }

}
