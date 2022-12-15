using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;



namespace HoopTeam.Implementacion
{
    class Cliente
    {
        //Llama a varios objetos de los Modelos para ser utilizados más tarde

        EstudianteEstatico est = new EstudianteEstatico();
        Entrenador ent = new Entrenador();
        Administrador adm = new Administrador();

        //Objetos de MySQL para conectar con la base de datos
        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();


        //Los DataSet y DataTable se usan a través del código para llenar tablas dentro del cliente con resultados de bases de datos,
        // y las tablas de los resultados llenan los datos de la aplicación

        public Cliente()
        {
        }

        //Método para iniciar sesión. Recibe de parámetros el correo y la contraseña, lo compara en la base de datos
        // y busca qué tipo de usuario es
        public string LogIn(string correo, string contra)
        {
            string flag = "";
 
            DataSet dsEntrenador = new DataSet();
            DataTable tbEntrenador = new DataTable();

            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();

            DataSet dsAdmin = new DataSet();
            DataTable tbAdmin = new DataTable();

            //Estructura try-catch para que cualquier error de SQL se pueda detectar y explicar al usuario si se necesitara.
            try
            {
                //Este comando establece la sesión con la base de datos
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Comando SQL para hacer el inicio de sesión
                string qry = "SELECT * FROM Entrenador where correo = '" + correo + "' and contrasenna = '" + contra + "' and activo = 1";
                //convertir qry en comando
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;

                //El adaptador llena los datos del código con los de la base de datos
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(dsEntrenador, "entrenador");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                tbEntrenador = dsEntrenador.Tables["entrenador"];

                //Recorre la base buscando datos que correspondan con el inicio de sesión de algún entrenador 
                if (tbEntrenador.Rows.Count != 0)
                {
                    //Asigna los valores a su respectiva variable en el objeto por cada fila 
                    foreach (DataRow drCurrent in tbEntrenador.Rows)
                    {
                        //Establece los valores de cada campo del Entrenador para ver sus datos al iniciar sesión
                        ent.setCedula(drCurrent["cedula"].ToString());
                        ent.setNombre(drCurrent["nombre"].ToString());
                        ent.setApellido1(drCurrent["apellido1"].ToString());
                        ent.setApellido2(drCurrent["apellido2"].ToString());
                        ent.setCorreo(drCurrent["correo"].ToString());
                        ent.setContrasenna(drCurrent["contrasenna"].ToString());
                        adm.setSuperUser(false);

                        flag = "Ent";
                        return flag;
                    }
                }
                else
                {
                    //Si no llegan datos de entrenadores, se recorre la base buscando datos que correspondan
                    // con el inicio de sesión de algún estudiante 
                    string qry2 = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "' and activo = 1 ";
                    cmd.CommandText = qry2;
                    cmd.Connection = con;
                    Adaptador.SelectCommand = cmd;
                    Adaptador.Fill(dsEstudiante, "Estudiantes");
                    cmd.ExecuteNonQuery();
                    tbEstudiante = dsEstudiante.Tables["Estudiantes"];

                    if (tbEstudiante.Rows.Count != 0)
                    {
                        foreach (DataRow drCurrent in tbEstudiante.Rows)
                        {//Establece los valores de cada campo del Estudiante para ver sus datos al iniciar sesión

                            est.setCedula(drCurrent["cedula"].ToString());
                            est.setNombre(drCurrent["nombre"].ToString());
                            est.setApellido1(drCurrent["apellido1"].ToString());
                            est.setApellido2(drCurrent["apellido2"].ToString());
                            est.setNacimiento(drCurrent["fechaNacimiento"].ToString());
                            est.setGenero(drCurrent["genero"].ToString());
                            est.setCorreo(drCurrent["correo"].ToString());
                            est.setContrasenna(drCurrent["contrasenna"].ToString());
                            adm.setSuperUser(false);

                            flag = "Est";
                            return flag;
                        }
                    }
                    else
                    {
                        //Si no llegan datos de estudiantes, se recorre la base buscando datos que correspondan
                        // con el inicio de sesión del administrador
                        string qry3 = "SELECT * FROM Administrador where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                        //convertir qry en comando 
                        cmd.CommandText = qry3;
                        //convierte el string conexion en conexion
                        cmd.Connection = con;
                        //El adaptador selecciona el comando
                        Adaptador.SelectCommand = cmd;
                        //Indica la tabla con la que se llena el dataset
                        Adaptador.Fill(dsAdmin, "Administrador");
                        //Ejecuta el query
                        cmd.ExecuteNonQuery();
                        //LLena el datatable con la informacion que trajo el dataset 
                        tbAdmin = dsAdmin.Tables["Administrador"];

   
                        if (tbAdmin.Rows.Count != 0)
                        {
                            //Asigna los valores a su respectiva variable en el objeto por cada fila 
                            foreach (DataRow drCurrent in tbAdmin.Rows)
                            {//Establece los valores de cada campo del Administrador para ver sus datos al iniciar sesión
                                adm.setIdAdmin(Int32.Parse(drCurrent["idAdmin"].ToString()));
                                adm.setCorreo(drCurrent["correo"].ToString());
                                adm.setContra(drCurrent["contrasenna"].ToString());
                                adm.setSuperUser(true);
                                flag = "Sup";
                                return flag;
                            }
                        }
                        else
                        {
                            //Mensaje de error cuando el correo y contraseña no corresponde con ningún usuario
                            flag = "Usuario o contraseña equivocada";
                            return flag;
                        }
                    }
                }
            }
            catch (Exception ex) //Mensaje de error de SQL para detectar problemas o notificar a usuario
            {
                return ex.Message;
            }
            return flag;

        }

        //Método para verificar que el correo está en la base cuando se olvida la contraseña 
        public string verEmail(string correo)
        {
            string flag = "";
            //objeto para almacenar datos
            DataSet dsEntrenador = new DataSet();
            //Representar tablas dataset
            DataTable tbEntrenador = new DataTable();

            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();

            DataSet dsAdmin = new DataSet();
            DataTable tbAdmin = new DataTable();

            try
            {
                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Comando para revisar si el correo está en la base de Entrenador

                string qry = "SELECT * FROM Entrenador where correo = '" + correo + "' and activo = 1";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(dsEntrenador, "entrenador");
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                tbEntrenador = dsEntrenador.Tables["entrenador"];

                if (tbEntrenador.Rows.Count != 0)
                {
                        flag = "Entrenador";
                        return flag;
                   
                }
                else
                {
                    //Si no se reciben datos de Entrenador, este es el comando para revisar si el correo está en la base de Estudiante

                    string qry2 = "SELECT * FROM Estudiantes where correo = '" + correo + "' and activo = 1 ";
                    //convertir qry en comando
                    cmd.CommandText = qry2;
                    //convierte el string conexion en conexion
                    cmd.Connection = con;
                    //El adaptador selecciona el comando 
                    Adaptador.SelectCommand = cmd;
                    //Indica la tabla con la que se llena el dataset
                    Adaptador.Fill(dsEstudiante, "Estudiantes");
                    //Ejecuta el query
                    cmd.ExecuteNonQuery();
                    //LLena el datatable con la informacion que trajo el dataset 
                    tbEstudiante = dsEstudiante.Tables["Estudiantes"];

                    if (tbEstudiante.Rows.Count != 0)
                    {
                        //Asigna los valores a su respectiva variable en el objeto por cada fila 
                        foreach (DataRow drCurrent in tbEstudiante.Rows)
                        {
                            flag = "Estudiantes";
                            return flag;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return flag;

        }

        //Método para actualizar la contraseña cuando se selecciona olvidar contraseña y se verifica que existe el correo
        public void CambiarContrasenna (string ced, string contra, string tabla)
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
                //Comando para actualizar la contraseña en la tabla (la tabla es Entrenador o Estudiante dependiendo del parametro que reciba)
                string qry = "UPDATE "+tabla+" SET contrasenna = '" + contra + "' WHERE cedula = " + ced + ";";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //Ejecuta el query
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                //declara variable 
                string txt = ex.Message;

            }
        }

        //  En el email de cambiar contraseña, se usa el nombre completo para digirirse a la persona por el nombre. 
        //  Este método consigue el nombre completo
        public string GetPersona(string correo, string tabla)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //Crea objeto 
                Estudiante est = new Estudiante();
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

                //Comando SQL para conseguir los datos del Entrenador o Estudiante
                string qry = "SELECT * FROM " + tabla + " Where  correo = '" + correo + "' and activo = 1 ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, tabla);
                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables[tabla];
                //crea variable 
                string nombreCompleto = "";
                foreach (DataRow drCurrent in dt.Rows)
                {
 
                    //Esta parte trae el nombre, apellido 1 y apellido 2 del usuario que se busca y los une en un solo string
                    nombreCompleto = drCurrent["nombre"].ToString() + " " + drCurrent["apellido1"].ToString() + " " + drCurrent["apellido2"].ToString();

                }
                //Retorna variable 
                return nombreCompleto;
            }
            catch (Exception ex)
            {
                //declara variable 
                string txt = ex.Message;
                return txt;
            }
        }

        //  Este método consigue la cédula del usuario para mandar el correo de cambiar contraseña al correo electrónico de la persona
        public string GetCedula(string correo, string tabla)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                //Declara la variable tipo DataAdapter
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                //Crea objeto
                Estudiante est = new Estudiante();
                //objeto para almacenar datos
                DataSet ds = new DataSet();
                // Representar tablas dataset
                DataTable dt = new DataTable();

                //conexion base de datos 
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                // Comando SQL que consigue la cédula del usuario indicado
                string qry = "SELECT cedula FROM " + tabla + " Where  correo = '" + correo + "' and activo = 1 ";
                //convertir qry en comando 
                cmd.CommandText = qry;
                //convierte el string conexion en conexion
                cmd.Connection = con;
                //El adaptador selecciona el comando 
                Adaptador.SelectCommand = cmd;
                //Indica la tabla con la que se llena el dataset
                Adaptador.Fill(ds, tabla);
                //Ejecuta el query
                cmd.ExecuteNonQuery();
                //LLena el datatable con la informacion que trajo el dataset 
                dt = ds.Tables[tabla];
                string cedula = "";
                //Asigna los valores a su respectiva variable en el objeto por cada fila 
                foreach (DataRow drCurrent in dt.Rows)
                {

                    cedula = drCurrent["cedula"].ToString();

                }
                //Returna Variable 
                return cedula;
            }
            catch (Exception ex)
            {
                //declara variable 
                string txt = ex.Message;
                return txt;
            }
        }
    }
}
