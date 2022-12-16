using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodosEntrenadores : ContentPage
    {
        //referncia al entrenador no estatico 
        EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
        public TodosEntrenadores()
        {
            InitializeComponent();
        }
        async void Sett()
        {
            await Navigation.PushModalAsync(new EntMain(), true);
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        private async void verEditEntrenador_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditEntrenador(ent), true);
        }

        private async void agregarEntrenador_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AgregarEntrenador(), true);
        }

        //captura el evento de precionar una entrada 
        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        //captura la informacion de la entrada seleccionada
        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedEntrenador = currentSelected.FirstOrDefault() as EntrenadorNO_Estatico;
            ent = selectedEntrenador;
            Debug.WriteLine(selectedEntrenador.NombreCompleto);
        }

    }
}