using HoopTeam.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntSettings : ContentPage
    {
        Entrenador entrenador = new Entrenador();
        Administrador adm = new Administrador();
        public EntSettings()
        {
            InitializeComponent();
        }
        async void LogOut()
        {
            //limpia la sesion del administrador para hacer un Log Out
            entrenador.setCedula("");
            entrenador.setNombre("");
            entrenador.setApellido1("");
            entrenador.setApellido2("");
            entrenador.setCorreo("");
            entrenador.setContrasenna("");

            //pone al super usuario en 0 por seguridad
            adm.setIdAdmin(0);
            adm.setCorreo("");
            adm.setContra("");

            await Navigation.PushModalAsync(new MainPage(), true);
        }
        private void LogOut_Clicked(object sender, EventArgs e)
        {
            LogOut();
        }

        async void Ent()
        {
            await Navigation.PushModalAsync(new EntMain(), true);
        }
        private void Volver_Clicked(object sender, EventArgs e)
        {
            Ent();
        }
    }
}