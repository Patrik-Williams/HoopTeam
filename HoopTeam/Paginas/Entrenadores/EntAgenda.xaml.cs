using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using System.Diagnostics;
using HoopTeam.Modelo.Entrenadores;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntAgenda : ContentPage
    {
        Entrenador entA = new Entrenador();
        Agenda agn = new Agenda();

        string idA { get; set; }
        int idEqp { get; set; }
        public EntAgenda()
        {
            InitializeComponent();
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EntSettings(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        async private void verEditAgn_Tapped(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new EditAgenda(agn,idA,idEqp), true);
        }
        public void CVCollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        void updateSelectionData(IReadOnlyList<Object> previousSelected, IReadOnlyList<Object> currentSelected)
        {
            var selectedAgenda = currentSelected.FirstOrDefault() as Agenda;
            agn = selectedAgenda;

            idEqp = selectedAgenda.idEquipo;

            Debug.WriteLine(selectedAgenda.Descripcion);
            Debug.WriteLine(selectedAgenda.Equipo);
            Debug.WriteLine(selectedAgenda.Cancha);
            Debug.WriteLine(selectedAgenda.FechaHora);
            Debug.WriteLine(selectedAgenda.idAgenda);
        }
        async void Agenda()
        {
            await Navigation.PushModalAsync(new AgregarAgenda(), true);
        }

        private void agregarAgenda_Tapped(object sender, EventArgs e)
        {
            Agenda();
        }

        async void verAgenda_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntMain(),true);
        }
    }
}