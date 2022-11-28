using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Modelo;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntAgenda : ContentPage
    {
        Entrenador ent = new Entrenador();
        public EntAgenda()
        {
            InitializeComponent();
        }

        async void Agenda()
        {
            await Navigation.PushModalAsync(new AgregarAgenda(), true);
        }

        private void agregarAgenda_Tapped(object sender, EventArgs e)
        {
            Agenda();
        }

        async void verAgenda_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntMain(),true);
        }
    }
}