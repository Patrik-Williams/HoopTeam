using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;



namespace HoopTeam.Implementacion
{
    class Cliente
    {
        public string MostrarNombre(string cedula)
        {// Inicio del metodo MOSTRAR NOMBRE

            
            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            DataSet dsEntrenador = new DataSet();
            DataTable tbEntrenador = new DataTable();
            string txt = "";
            try//Intente
            {
                con = new MySqlConnection("server = localhost; " +
                                          "port = 3306; " +
                                          "username = root; " +
                                          "password = admin;" +
                                          "database =hoopteam");
                con.Open();
                string qry = "SELECT * FROM Entrenador where cedula=" + cedula;
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEntrenador, "Entrenadores");
                cmd.ExecuteNonQuery();

                tbEntrenador = dsEntrenador.Tables["Entrenadores"];

                foreach (DataRow drCurrent in tbEntrenador.Rows)
                {
                    txt = drCurrent["nombre"].ToString();
                    txt += " " + drCurrent["apellido1"].ToString(); 
                }

                return txt;
            }// fin del try
            catch (Exception ex)
            {
                return ex.Message;
            }
        }// Fin del metodo MOSTRAR NOMBRE

        public string LogIn(string correo, string contra)
        {
            string flag = "";

            MySqlCommand cmd = new MySqlCommand();//comandos
            MySqlConnection con;//conexion
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            DataSet dsEntrenador = new DataSet();
            DataTable tbEntrenador = new DataTable();

            DataSet dsEstudiante = new DataSet();
            DataTable tbEstudiante = new DataTable();

            DataSet dsAdmin = new DataSet();
            DataTable tbAdmin = new DataTable();

            try
            {
                con = new MySqlConnection("server = hoopteam.ckftwuueje9o.us-east-1.rds.amazonaws.com; " +
                                          "port = 3306; " +
                                          "username = admin; " +
                                          "password = hoopteamAdmin;" +
                                          "database =HoopTeam");
                con.Open();
                string qry = "SELECT * FROM Entrenador where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                cmd.CommandText = qry;
                cmd.Connection = con;
                Adaptador.SelectCommand = cmd;
                Adaptador.Fill(dsEntrenador, "entrenador");
                cmd.ExecuteNonQuery();
                tbEntrenador = dsEntrenador.Tables["entrenador"];

                if(tbEntrenador.Rows.Count != 0)
                {
                    foreach (DataRow drCurrent in tbEntrenador.Rows)
                    {
                        flag = "Ent";
                        return flag;
                    }
                }
                else
                {
                    string qry2 = "SELECT * FROM Estudiantes where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                    cmd.CommandText = qry2;
                    cmd.Connection = con;
                    Adaptador.SelectCommand = cmd;
                    Adaptador.Fill(dsEstudiante, "Estudiantes");
                    cmd.ExecuteNonQuery();
                    tbEstudiante = dsEstudiante.Tables["Estudiantes"];

                    if(tbEstudiante.Rows.Count != 0)
                    {
                        foreach (DataRow drCurrent in tbEstudiante.Rows)
                        {
                            flag = "Est";
                            return flag;
                        }
                    }
                    else
                    {
                        string qry3 = "SELECT * FROM Administrador where correo = '" + correo + "' and contrasenna = '" + contra + "'";
                        cmd.CommandText = qry3;
                        cmd.Connection = con;
                        Adaptador.SelectCommand = cmd;
                        Adaptador.Fill(dsAdmin, "Administrador");
                        cmd.ExecuteNonQuery();
                        tbAdmin = dsAdmin.Tables["Administrador"];

                        if (tbAdmin.Rows.Count != 0)
                        {
                            foreach (DataRow drCurrent in tbAdmin.Rows)
                            {

                                flag = "Sup";
                                return flag;
                            }
                        }
                        else
                        {
                            flag = "Usuario o contrasenna equivocada";
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
    }
}
