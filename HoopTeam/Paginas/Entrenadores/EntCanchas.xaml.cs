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
        //referencia al cliente entrenador
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

        //captura la accion de presionar en el editar
        private async void editarCancha_Tapped(object sender, EventArgs e)
        {
            //muestra un dialogo para editar la ubicacion de la cancha
            string result = await DisplayPromptAsync("Editar cancha", "Ubicación:", initialValue: ubicacion, maxLength: default, keyboard: Keyboard.Default);
            //si la respuesta no esta vacia
            if (result != null)
            {
                //llama al metodo de editar cancha 
                clienteEnt.EditarCancha(cancha, result);
                InitializeComponent();
            }
        }

        //captura la accion de presionar en el eliminar
        public async void eliminarCancha_Tapped(object sender, EventArgs e)
        {
            //muestra un dialogo de aviso para eliminar
            string result = await DisplayPromptAsync("Eliminar cancha", "Cancha:", initialValue: cancha.ToString(), maxLength: default, keyboard: Keyboard.Default);
            //si la respuesta es si
            if (result != null)
            {
                //llama al metodo de eliminar
                clienteEnt.EliminarCancha(Int32.Parse(result));
                InitializeComponent();
            }
        }

        //captura el evento de presionar 
        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        //captura la informacion de la entrada seleccionada
        void updateSelectionData(IReadOnlyList<Object> previouslySelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedCancha = currentSelected.FirstOrDefault() as Cancha;
            ubicacion = selectedCancha.ubicacion;
            cancha = selectedCancha.idCancha;
        }
    }
}