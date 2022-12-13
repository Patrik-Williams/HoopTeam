using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;



namespace HoopTeam.Implementacion
{
    class Cliente
    {
        //Crar objeto 
        EstudianteEstatico est = new EstudianteEstatico();
        Entrenador ent = new Entrenador();
        Administrador adm = new Administrador();

        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        //Declara la variable tipo DataAdapter
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public Cliente()
        {
        }

        public string LogIn(string correo, string contra)
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
                //consulta base de datos 
                string qry = "SELECT * FROM Entrenador where correo = '" + correo + "' and contrasenna = '" + contra + "' and activo = 1";
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
                    //Asigna los valores a su respectiva variable en el objeto por cada fila 
                    foreach (DataRow drCurrent in tbEntrenador.Rows)
                    {
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
                    //consulta base de datos
                    string qry2 = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "' and activo = 1 ";
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
                        //consulta base de datos 
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
                            {
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
                            flag = "Usuario o contraseña equivocada";
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
                //consulta base de datos 
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
                    //consulta base de datos
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

                //consulta base de datos 
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
                //consulta base de datos 
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
                //consulta base de datos 
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
