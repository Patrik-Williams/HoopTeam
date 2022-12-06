using HoopTeam.Implementacion;
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
    public partial class AgregarCancha : ContentPage
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        public AgregarCancha()
        {
            InitializeComponent();
        }

        async void verCanchas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntCanchas(), true);
        }
        async void Volver()
        {
            await Navigation.PushModalAsync(new EntCanchas(), true);
        }

        private void btnAgregarCancha(object sender, EventArgs e)
        {
 
            string ubicacion = txtUbicacion.Text;

            clienteEnt.AgregarCancha( ubicacion);
            DisplayAlert("Información", "Cancha agregada", "Aceptar");
            Volver();

        }
    }
}