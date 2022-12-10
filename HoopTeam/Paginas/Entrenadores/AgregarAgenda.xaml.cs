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
    public partial class AgregarAgenda : ContentPage

    {
        Agenda agn = new Agenda();
        Equipos eqp = new Equipos();
        Entrenador ent = new Entrenador();
        List<Equipos> equipos = new List<Equipos>();
        static string idEquipo { get; set; }

        List<Cancha> canchas { get; set; }
        static string cancha { get; set; }

        List<Agenda> agendas { get;set; }
        static string descripcion { get; set; }

        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        ClienteAgenda clienteAgn = new ClienteAgenda();
        public AgregarAgenda()
        {
            InitializeComponent();

            equipos = clienteAgn.GetEquiposA(Int32.Parse(ent.getCedula()));
            canchas = clienteAgn.GetCanchasA();
            foreach (Equipos eq in equipos)
            {
                cbEquipo.Items.Add(eq.idEquipo.ToString());
            }

            foreach (Cancha cn in canchas)
            {
                cbCancha.Items.Add(cn.idCancha.ToString());
            }
        }
        async void Sett()
        {
            await Navigation.PushModalAsync(new EntAgenda(), true);
        }
        async void Volver()
        {
            await Navigation.PushModalAsync(new EntAgenda(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void btnAgregarAgn(object sender, EventArgs e)
        {

            //int idAgenda = Int32.Parse(txtAgenda.Text);
            //int idEquipo = Int32.Parse(txtEqp.Text);
            //int idCancha = Int32.Parse(txtCn.Text);
            DateTime fecha = fechaAgenda.Date;
            string hora =  horaAgenda.Time.ToString();
            //string descripcion = txtDescripcion.Text;
            string txt = fecha.ToString("yyyy-MM-dd") + " " +hora;

            clienteAgn.AgregarAgenda( idEquipo,cancha,txt,descripcion);
            DisplayAlert("Información", "Agenda agregada", "Aceptar");
            Volver();

        }
        private void OnPickerSelectedIndexChangedDescripcion(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            descripcion = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(descripcion);
            

        }
        private void OnPickerSelectedIndexChangedEquipo1(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            idEquipo = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(idEquipo);


        }
        private void OnPickerSelectedIndexChangedCanchas(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cancha = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(cancha);


        }

    }
}