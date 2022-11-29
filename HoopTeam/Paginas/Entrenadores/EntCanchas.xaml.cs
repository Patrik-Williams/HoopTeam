using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntCanchas : ContentPage
    {
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        static string ubicacion { get; set; }
        static int cancha { get; set; }
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
            string result = await DisplayPromptAsync("Editar cancha", "Ubicación:", initialValue: ubicacion, maxLength: default, keyboard: Keyboard.Default);
            if (result != null)
            {
                clienteEnt.EditarCancha(cancha, result);
                InitializeComponent();
            }
        }

        public async void eliminarCancha_Tapped(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Eliminar cancha", "Cancha:", initialValue: cancha.ToString(), maxLength: default, keyboard: Keyboard.Default);
            if (result != null)
            {
                Debug.Write(result);
                clienteEnt.EliminarCancha(Int32.Parse(result));
                InitializeComponent();
            }
        }

        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }
        void updateSelectionData(IReadOnlyList<Object> previouslySelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedCancha = currentSelected.FirstOrDefault() as Cancha;
            ubicacion = selectedCancha.ubicacion;
            cancha = selectedCancha.idCancha;
        }
    }
}