using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Modelo;

namespace HoopTeam.Paginas.Estudiantes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstAgenda : ContentPage
    {
        EstudianteEstatico est = new EstudianteEstatico();
        public EstAgenda()
        {
            InitializeComponent();
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