using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Estudiantes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstEquipos : ContentPage
    {
        Estudiante est = new Estudiante();

        public EstEquipos()
        {
            InitializeComponent();
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
    }
}

