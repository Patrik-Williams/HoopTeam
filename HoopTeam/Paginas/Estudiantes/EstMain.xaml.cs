using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Modelo;
using HoopTeam.Paginas.Estudiantes;
using HoopTeam.Modelo.Estudiantes;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstMain : ContentPage
    {
        Cliente objCliente = new Cliente();
        ClienteEstudiante objClienteEst = new ClienteEstudiante();
        EstudiantePago estudiantePago = new EstudiantePago();


        EstudianteEstatico estudiante = new EstudianteEstatico();
        Equipos equipo = new Equipos();
        public EstMain()
        {
            InitializeComponent();
            estudiantePago = objClienteEst.EstudianteEstadoPago(estudiante.getCedula());
            equipo = objClienteEst.getEquipo(estudiante.getCedula());
            //lbInfo.Text = objCliente.MostrarNombre(datos.getCedula(), "Estudiantes");
            if (estudiantePago.getPagoRealizado()==1)
            {
                Equipos.IsEnabled = true;
                Calendario.IsEnabled = true;
                EstadoPago.IsVisible = false;
                EstadoPago1.IsVisible = false;
            }
            if (estudiantePago.getPagoRealizado() == 0)
            {
                Equipos.IsEnabled = false;
                Calendario.IsEnabled = false;
                InfoEquipos.IsVisible = false;
                //lb3.IsEnabled = false;
                EstadoPago.IsVisible = true;
                EstadoPago1.IsVisible = true;

            }
            lbInfo.Text = estudiante.getNombre() + " " + estudiante.getApellido1() + " " + estudiante.getApellido2();
            lbCorreo.Text = estudiante.getCorreo();
            lbCedula.Text = estudiante.getCedula();

            lbId.Text = equipo.idEquipo.ToString();
            lbCategoria.Text = equipo.categoria;
            lbGenero.Text = equipo.genero;
           
             

        }

        private void verPerfil_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Informacion", "PERFIL", "Ok");
        }


        async void Sett()
        {
            await Navigation.PushModalAsync(new EstSettings(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        async private void verEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EstEquipos(), true);
        }


    }
}