using MySql.Data.MySqlClient;

namespace HoopTeam.Implementacion
{
    class ClienteEntrenador
    {
        MySqlCommand cmd = new MySqlCommand();//comandos
#pragma warning disable CS0169 // The field 'ClienteEntrenador.con' is never used
        MySqlConnection con;//conexion
#pragma warning restore CS0169 // The field 'ClienteEntrenador.con' is never used
        MySqlDataAdapter Adaptador = new MySqlDataAdapter();
    }
}
