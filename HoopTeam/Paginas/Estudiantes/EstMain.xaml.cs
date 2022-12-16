using HoopTeam.Implementacion;
using HoopTeam.Modelo.Estudiantes;
using HoopTeam.Modelo;
using HoopTeam.Paginas.Estudiantes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Modelo.Entrenadores;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstMain : ContentPage
    {
        //referencia a los clientes necesarios
        Cliente objCliente = new Cliente();
        ClienteEstudiante objClienteEst = new ClienteEstudiante();
        EstudiantePago estudiantePago = new EstudiantePago();

        //referencia a la sesion del estudiante
        EstudianteEstatico estudiante = new EstudianteEstatico();
        Equipos equipo = new Equipos();
        public EstMain()
        {
            InitializeComponent();
            //se define la informacion de pago del estudiante
            estudiantePago = objClienteEst.EstudianteEstadoPago(estudiante.getCedula());
            //se trae el equipo al que pertenece el estudiante
            equipo = objClienteEst.getEquipo(estudiante.getCedula());
            
            //si el pago es realizado, se habilitan las funciones de la app
            if (estudiantePago.getPagoRealizado() == 1)
            {
                Equipos.IsEnabled = true;
                Calendario.IsEnabled = true;
                EstadoPago.IsVisible = false;
                EstadoPago1.IsVisible = false;
            }
            //si no ha realizado el pago, se dehabilitan las funciones
            if (estudiantePago.getPagoRealizado() == 0)
            {
                Equipos.IsEnabled = false;
                Calendario.IsEnabled = false;
                InfoEquipos.IsVisible = false;
                EstadoPago.IsVisible = true;
                EstadoPago1.IsVisible = true;

            }
            //se llenan los campos de informacion en la pagina
            lbInfo.Text = estudiante.getNombre() + " " + estudiante.getApellido1() + " " + estudiante.getApellido2();
            lbCorreo.Text = estudiante.getCorreo();
            lbCedula.Text = estudiante.getCedula();

            lbId.Text = equipo.idEquipo.ToString();
            lbCategoria.Text = equipo.categoria;
            lbGenero.Text = equipo.genero;



        }

        async private void verPerfil_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EstEditar(), true);
        }


        async void Sett()
        {
            await Navigation.PushModalAsync(new EstSettings(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        async private void verAgenda_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EstAgenda(), true);
        }

        async private void verEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EstEquipos(), true);
        }


    }
}