using HoopTeam.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;



namespace HoopTeam.Implementacion
{
    class Cliente
    {
        EstudianteEstatico est = new EstudianteEstatico();
        Entrenador ent = new Entrenador();
        Administrador adm = new Administrador();

        MySqlCommand cmd = new MySqlCommand();//comandos
        MySqlConnection con;//conexion
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();

        public Cliente()
        {
        }

        public string LogIn(string correo, string contra)
        {
            string flag = "";
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

                if (tbEntrenador.Rows.Count != 0)
                {
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
                            flag = "Usuario o Contraseña equivocada";
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
