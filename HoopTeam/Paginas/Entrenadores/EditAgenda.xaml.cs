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
    public partial class EditAgenda : ContentPage
    {
        Agenda agn = new Agenda();
        Equipos eqp = new Equipos();
        List<Equipos> equipos = new List<Equipos>();
        //  static string eqpN { get; set; }
        //  static string eqpV { get; set; }
        List<Agenda> agendas { get; set; }
        static string descripcion { get; set; }

        ClienteAgenda clienteA = new ClienteAgenda();


        public EditAgenda(string ent)
        {
            agn = clienteA.GetAgendaE(ent);


            InitializeComponent();

            txtEqp.Text = agn.Equipo;
            txtCn.Text = agn.Cancha;
            txtFechaHora.Text = agn.FechaHora;

        }
        async void Sett()
        {
            await Navigation.PushModalAsync(new EntAgenda(), true);
        }


        private void settings_clicked(object sender, EventArgs e)
        {
            Sett();
        }
        async void VolverA()
        {
            await Navigation.PushModalAsync(new EntAgenda(), true);
        }
        private void OnPickerSelectedIndexChangedDescripcion(object sender, EventArgs e)
            {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            descripcion = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(descripcion);
            
            }

        private void btnEditarA(object sender, EventArgs e)
        {
            if(aDescripcion.SelectedItem == null )
            {
                DisplayAlert("Alerta", "Seleccion una descripcion", "OK");
            }
                else
           {
            int Agenda = Int32.Parse(agn.idAgenda);
            string Equipo = txtEqp.Text;
            string Cancha = txtCn.Text;
            string FechaHora = txtFechaHora.Text;

                clienteA.EditarAgenda(Agenda, Equipo, Cancha, FechaHora, descripcion);

                VolverA();

           }

        }
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar al agenda?", "Si", "NO");
            if (answer)
            {
                clienteA.EliminarAgenda(Int32.Parse(agn.idAgenda));
                DisplayAlert("Informacion", "Estudiante eliminado", "Ok");
                VolverA();
            }
        }
        private void btnEliminarA(object sender, EventArgs e)
        {
            ShowExitDialog();
        }
    }
}