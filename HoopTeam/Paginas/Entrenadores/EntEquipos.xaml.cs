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

        //referencia al cliente entrenador
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

        //lleva a la pagina de todos los equipos
        private async void verEquipos_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodosEquipos(), true);
        }

        private async void editarEquipo_Tapped(object sender, EventArgs e)
        {
            //despliega una ventana para editar
            string result = await DisplayPromptAsync("Editar Cupo", "Cupo:", initialValue: cupo.ToString(), maxLength: 2, keyboard: Keyboard.Numeric);
            //si la informacion es diferente a nula
            if(result != null)
            {
                //llama al metodo de editar
               clienteEnt.EditarCupo(Int32.Parse(result), equipo);
               InitializeComponent();
            }
        }

        //captura el presionar
        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        //agarra la informacion de la entrada presionada
        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedEquipo = currentSelected.FirstOrDefault() as Equipos;
            cupo = selectedEquipo.cupo;
            equipo = selectedEquipo.idEquipo;

        }
    }
}