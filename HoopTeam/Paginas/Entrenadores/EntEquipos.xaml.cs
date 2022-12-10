using HoopTeam.Implementacion;
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
    public partial class EntEquipos : ContentPage
    {   
        static int cupo { get; set; }
        int equipo { get; set; }
        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        public EntEquipos()
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

        private async void verEquipos_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodosEquipos(), true);
        }

        private async void editarEquipo_Tapped(object sender, EventArgs e)
        {
            Debug.WriteLine(equipo);
            string result = await DisplayPromptAsync("Editar Cupo", "Cupo:", initialValue: cupo.ToString(), maxLength: 2, keyboard: Keyboard.Numeric);
            if(result != null)
            {
               clienteEnt.EditarCupo(Int32.Parse(result), equipo);
               InitializeComponent();
            }
         


        }

        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedEquipo = currentSelected.FirstOrDefault() as Equipos;
            cupo = selectedEquipo.cupo;
            equipo = selectedEquipo.idEquipo;
            Debug.WriteLine(selectedEquipo.categoria);
            Debug.WriteLine("Hola");
        }
    }
}