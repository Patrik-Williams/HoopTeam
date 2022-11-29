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
    public partial class TodosEquipos : ContentPage
    {
        Administrador adm = new Administrador();
        public TodosEquipos()
        {
            InitializeComponent();
            if (adm.getSuperUser())
            {
                agregar.IsVisible = true;
            }
            else
            {
                agregar.IsVisible = false;
            }

        }

        async void Sett()
        {
            if (adm.getSuperUser())
            {
                await Navigation.PushModalAsync(new EntMain(), true);
            }
            else
            {
                await Navigation.PushModalAsync(new EntEquipos(), true);
            }
            
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private async void agregarEquipos_Tapped(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new AgregarEquipos(), true);
        }

    }
}