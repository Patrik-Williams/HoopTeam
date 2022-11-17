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
        public TodosEstudiantes()
        {
            InitializeComponent();
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EntEstudiantes(), true);
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private async void agregarEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AgregarEst(), true);
        }

    }
}