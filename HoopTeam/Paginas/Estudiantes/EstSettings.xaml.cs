using HoopTeam.Modelo;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstSettings : ContentPage
    {
        Estudiante estudiante = new Estudiante();
        public EstSettings()
        {
            InitializeComponent();
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