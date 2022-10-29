using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;


namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntMain : ContentPage
    {
        Cliente objCliente = new Cliente();

        Entrenador entrenador = new Entrenador();
        public EntMain()
        {
            InitializeComponent();

            lbInfo.Text = entrenador.getNombre() + " " + entrenador.getApellido1() + " " + entrenador.getApellido2();
            lbCorreo.Text = entrenador.getCorreo();
            lbCedula.Text = entrenador.getCedula();
        }
        async void LogOut()
        {
            entrenador.setCedula("");
            entrenador.setNombre("");
            entrenador.setApellido1("");
            entrenador.setApellido2("");
            entrenador.setCorreo("");
            entrenador.setContrasenna("");
            await Navigation.PushModalAsync(new MainPage(), true);
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            LogOut();
        }
    }
}