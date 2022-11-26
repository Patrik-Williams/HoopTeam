using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstMain : ContentPage
    {
        Cliente objCliente = new Cliente();

        EstudianteEstatico estudiante = new EstudianteEstatico();
        public EstMain()
        {
            InitializeComponent();

            //lbInfo.Text = objCliente.MostrarNombre(datos.getCedula(), "Estudiantes");
            lbInfo.Text = estudiante.getNombre() + " " + estudiante.getApellido1() + " " + estudiante.getApellido2();
            lbCorreo.Text = estudiante.getCorreo();
            lbCedula.Text = estudiante.getCedula();

            
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

    }
}