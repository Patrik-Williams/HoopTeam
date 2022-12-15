using HoopTeam.Modelo;
using HoopTeam.Modelo.Entrenadores;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace HoopTeam.Implementacion
{
    class ClienteAdmin
    {
        //Método para agregar equipo de la academia a la base de datos
        public void AgregarEquipo(string categoria, string genero, int ent, int cupo)
        {
            try//Modelo try-catch se usa para detectar errores de SQL y mencionarselos al usuario si fuera necesario
            {
                //Objetos de MySQL para conectar con la base de datos. Se usa a través de todo el cliente

                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();

                //Los DataSet y DataTable se usan a través del código para llenar tablas dentro del cliente con resultados de bases de datos,
                // y las tablas de los resultados llenan los datos de la aplicación. Se usan a través de todo el cliente.

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                //Este comando establece la sesión con la base de datos. Se usan a través de todo el cliente.
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Comando SQL para ingresar el equipo a la base de datos con los valores que ingresa el usuario
                string qry = "INSERT INTO Equipos (categoria, genero, cedEntrenador, cupo, activo) values ('" + categoria + "', '" + genero + "', " + ent + ", " + cupo + ", 1)";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        //Método para conseguir todos los entrenadores en la base
        public List<EntrenadorNO_Estatico> GetEntrenadores()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                Estudiante est = new Estudiante();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                List<EntrenadorNO_Estatico> list = new List<EntrenadorNO_Estatico>();

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();

                //Método para traer todos los entrenadores activos
                string qry = "SELECT * FROM Entrenador where activo = 1";
                cmd.CommandText = qry;
                cmd.Connection = con;

                //Los adaptadores usan el comando SQL para llenar las tablas del programa con la información de la base de datos
                //Se usa un adaptador por cada tabla en uso en el comando
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Entrenador");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Entrenador"];

                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla
                    EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
                    ent.Cedula = drCurrent["cedula"].ToString();
                    ent.Nombre = drCurrent["nombre"].ToString();
                    ent.Apellido1 = drCurrent["apellido1"].ToString();
                    ent.Apellido2 = drCurrent["apellido2"].ToString();
                    ent.NombreCompleto = ent.Nombre + " " + ent.Apellido1 + " " + ent.Apellido2;
                    ent.Correo = drCurrent["correo"].ToString();
                   
                    list.Add(ent);

                }
                return list;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new List<EntrenadorNO_Estatico>();
            }
        }

        //Método sin uso!!!!!
        public EntrenadorNO_Estatico GetEntrenador(int ced)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();//comandos
                MySqlConnection con;//conexion
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                Estudiante est = new Estudiante();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                

                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Entrenador where cedula = " + ced + " and activo = 1;";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Entrenador");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Entrenador"];
                EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
                foreach (DataRow drCurrent in dt.Rows)
                {//Ciclo que por cada resultado que da la base de datos llena una nueva fila en la tabla

                    ent.Cedula = drCurrent["cedula"].ToString();
                    ent.Nombre = drCurrent["nombre"].ToString();
                    ent.Apellido1 = drCurrent["apellido1"].ToString();
                    ent.Apellido2 = drCurrent["apellido2"].ToString();
                    ent.NombreCompleto = ent.Nombre + " " + ent.Apellido1 + " " + ent.Apellido2;
                    ent.Correo = drCurrent["correo"].ToString();

                }
                return ent;//Regresa la lista de datos creada a partir de la base de datos 
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                //Si no hay resultados, devuelve una lista en blanco para evitar errores
                return new EntrenadorNO_Estatico();
            }
        }

        //Método que se usa para deshabilitar equipo
        public void EliminarEquipo(int idEquipo)
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

                //Comando SQL para deshabilitar el equipo que el usuario ingresa
                string qry = "Update Equipos set activo = 0 where idEquipo = "+idEquipo+"";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                //Comando SQL para deshabilitar la conexión de estudiantes en cada equipo 
                string qry2 = "UPDATE EstudianteEquipo SET activo = 0 where idEquipo = "+idEquipo+"";
                cmd.CommandText = qry2;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        //Método para editar la información de los equipos
        public void EditarEquipo(string cate, string gen, int ent, int cupo, int idEquipo)
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

                //Comando SQL para actualizar la información del equipo con los valores ingresados por el usuario
                string qry = "UPDATE Equipos set categoria = '"+cate+"', genero = '"+gen+"', cedEntrenador = "+ent+", cupo = "+cupo+" where  idEquipo = "+idEquipo+" ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }
        }

        //Método para ingresar un entrenador a la base de datos
        public void AgregarEntrenador(int ced, string nom, string ap1, string ap2, string correo, string contra)
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

                //Comando SQL para ingresar el entrenador a la base con la información que da el usuario
                string qry = "INSERT INTO Entrenador (cedula, nombre, apellido1, apellido2, correo, contrasenna, activo) values ("+ced+", '"+nom+"', '"+ap1+"', '"+ap2+"', '"+correo+"', '"+contra+"', 1)";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        //Método para editar la información del entrenador
        public void EditarEntrenador(int ced, string nom, string ap1, string ap2, string correo, string contra)
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

                //Comando SQL para actualizar la información del entrenador con los datos que ingresa el usuario
                string qry = "UPDATE Entrenador set nombre = '"+nom+"', apellido1 = '"+ap1+"', apellido2 = '"+ap2+"', correo = '"+correo+"' where cedula = "+ced+"";
                cmd.CommandText = qry;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string txt = ex.Message;

            }
        }

        //Método que verifica que el entrenador no tenga un equipo a su cargo antes de deshabilitarlo
        public int verificarEquipoEnt(int ced)
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

                //Comando SQL que regresa la cantidad de equipos que tiene el entrenador a su cargo
                string qry = "select count(*) as cant from Equipos where cedEntrenador = "+ced+" and activo = 1 ";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(ds, "Equipos");
                cmd.ExecuteNonQuery();

                dt = ds.Tables["Equipos"];
                int cant = 0;
                foreach (DataRow drCurrent in dt.Rows)
                {
                    cant = Int32.Parse(drCurrent["cant"].ToString());
                }
                return cant;
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
                int i = 0;
                return i;

            }
        }

        //Método para deshabilitar el entrenador
        public void EliminarEntrenador(int ced)
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

                //Método para establecer que el entrenador que el usuario selecciona quede como inactivo
                string qry = "Update Entrenador set activo = 0 where cedula = "+ced+"";
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
