using HoopTeam.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodosEstudiantes : ContentPage
    {

        Administrador adm = new Administrador();
        public TodosEstudiantes()
        {
            InitializeComponent();
        }

        async void Sett()
        {
            //si es administrador, lo devulve directo a la pagina de administrador
            if (adm.getSuperUser())
            {
                await Navigation.PushModalAsync(new EntMain(), true);
            }
            else
            {
                await Navigation.PushModalAsync(new EntEstudiantes(), true);
            }
            
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        //si se da click en el agregar
        private async void agregarEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AgregarEst(), true);
        }

    }
}