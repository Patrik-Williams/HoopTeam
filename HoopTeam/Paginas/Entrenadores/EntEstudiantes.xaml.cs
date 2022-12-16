using HoopTeam.Implementacion;
using HoopTeam.Modelo;
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
    public partial class EntEstudiantes : ContentPage
    {
        //referencia a la sesion del entrenador
        Entrenador ent = new Entrenador();

        string ced { get; set; }
        int equipo { get; set; }


        public EntEstudiantes()
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
        private async void verEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodosEstudiantes(), true);
        }

        async private void verEditEst_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditEstudiante(ced, equipo), true);
        }

        //captura el evento de presionar en una de las entradas de la lista 
        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        //captura la informacion de la entrada seleccionada 
        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedEstudiante = currentSelected.FirstOrDefault() as EstEntrenador;
            ced = selectedEstudiante.Cedula;
            equipo = Int32.Parse(selectedEstudiante.IdEquipo);
            Debug.WriteLine(selectedEstudiante.NombreCompleto);
        }

    }
}