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

        
    }
}