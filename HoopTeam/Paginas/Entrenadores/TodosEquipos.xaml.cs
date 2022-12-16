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
        //referencia al administrados
        Administrador adm = new Administrador();

        static Equipos equipo = new Equipos();
        public TodosEquipos()
        {
            InitializeComponent();
            //verifica si el usuario es un super administrador, si lo es, habilita la opcion de agregar equipos
            if (adm.getSuperUser())
            {
                agregar.IsVisible = true;
                equipoEn.IsEnabled = true;
            }
            else
            {
                agregar.IsVisible = false;
                equipoEn.IsEnabled = false;
            }

        }

        //captura el evento de presionar 
        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        //captura la informacion de la entrada seleccionada
        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedEquipo = currentSelected.FirstOrDefault() as Equipos;
            equipo = selectedEquipo;
        
        }

        async void Sett()
        {
            //si es super user, lo devuelve directo a la pagina principal
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