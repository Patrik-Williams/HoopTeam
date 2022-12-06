using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using HoopTeam.Modelo.Estudiantes;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstSettings : ContentPage
    {
        EstudiantePago estudiantePago = new EstudiantePago();
        ClienteEstudiante objClienteEst = new ClienteEstudiante();
        EstudianteEstatico estudiante = new EstudianteEstatico();

        public EstSettings()
        {
            InitializeComponent();
            estudiantePago = objClienteEst.EstudianteEstadoPago(estudiante.getCedula());
            IdPago.Text = estudiantePago.getIdPago();
            FechaPago.Text = estudiantePago.getFechaPago();
            if (estudiantePago.getPagoRealizado() == 1)
            {
                PagoRealizado.Text = "Realizado";

            }
            else
            {
                PagoRealizado.Text = "Por realizar";
            }

            Monto.Text = estudiantePago.getMonto();


        }

        async void LogOut()
        {
            estudiante.setCedula("");
            estudiante.setNombre("");
            estudiante.setApellido1("");
            estudiante.setApellido2("");
            estudiante.setCorreo("");
            estudiante.setContrasenna("");
            estudiante.setGenero("");
            estudiante.setNacimiento("");
            await Navigation.PushModalAsync(new MainPage(), true);
        }
        private void LogOut_Clicked(object sender, EventArgs e)
        {
            LogOut();
        }

        async void Est()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }
        private void Volver_Clicked(object sender, EventArgs e)
        {
            Est();
        }

    }
}