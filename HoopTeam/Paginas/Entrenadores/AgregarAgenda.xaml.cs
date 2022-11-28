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
        List<Equipos> equipos = new List<Equipos>();
        static string equipo { get; set; }

        Entrenador ent = new Entrenador();
        static string genero { get; set; }

        List<Cancha> canchas { get; set; }
        static string cancha { get; set; }

        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        ClienteAgenda clienteAgn = new ClienteAgenda();
        public AgregarAgenda()
        {
            InitializeComponent();
        }
        async void Sett()
        {
            await Navigation.PushModalAsync(new EntSettings(), true);
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
            int idAgenda = Int32.Parse(txtAgenda.Text);
            int idEquipo = Int32.Parse(txtEqp.Text);
            int idCancha = Int32.Parse(txtCn.Text);
            string fechaH = txtFechaHora.Text;
            string descripcion = txtDescripcion.Text;


            clienteAgn.AgregarAgenda(idAgenda, idEquipo,idCancha,fechaH,descripcion);
            DisplayAlert("Información", "Agenda agregada", "Aceptar");
            Volver();

        }
        

    }
}