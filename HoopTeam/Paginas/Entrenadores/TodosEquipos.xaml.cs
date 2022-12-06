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
    public partial class TodosEquipos : ContentPage
    {
        Administrador adm = new Administrador();

        static Equipos equipo = new Equipos();
        public TodosEquipos()
        {
            InitializeComponent();
            if (adm.getSuperUser())
            {
                agregar.IsVisible = true;
            }
            else
            {
                agregar.IsVisible = false;
            }

        }

        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedEquipo = currentSelected.FirstOrDefault() as Equipos;
            equipo = selectedEquipo;
            Debug.WriteLine(selectedEquipo.categoria);
        }

        async void Sett()
        {
            if (adm.getSuperUser())
            {
                await Navigation.PushModalAsync(new EntMain(), true);
            }
            else
            {
                await Navigation.PushModalAsync(new EntEquipos(), true);
            }
            
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private async void agregarEquipos_Tapped(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new AgregarEquipos(), true);
        }

        async private void verEditEquipos_Tapped(object sender, EventArgs e)
        {
     
            await Navigation.PushModalAsync(new EditEquipo(equipo), true);
        }

    }
}