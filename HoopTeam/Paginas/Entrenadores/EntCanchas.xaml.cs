using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Implementacion;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntCanchas : ContentPage
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        static int ubicacion { get; set; }
        int cancha { get; set; }
        public EntCanchas()
        {
            InitializeComponent();
        }

       async void verCanchas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntMain(),true);
        }
        async void agregarCancha_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AgregarCancha(), true);
        }

        private async void editarCancha_Tapped(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Editar cancha", "Ubicación:", initialValue: ubicacion.ToString(), maxLength: default, keyboard: Keyboard.Default);
            if (result != null)
            {
                clienteEnt.EditarCancha(cancha, result);
                InitializeComponent();
            }



        }
    }
}