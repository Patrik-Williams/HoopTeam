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
        //definicion de variables
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

            //carga las listas
            equipos = clienteAgn.GetEquiposA(Int32.Parse(ent.getCedula()));
            canchas = clienteAgn.GetCanchasA();

            //carga el picker con la informacion de la lista
            foreach (Equipos eq in equipos)
            {
                cbEquipo.Items.Add(eq.idEquipo.ToString());
            }
            //carga el picker con la informacion de la lista 
            foreach (Cancha cn in canchas)
            {
                cbCancha.Items.Add(cn.idCancha.ToString());
            }
        }

        async void Sett()
        {
            //metodo para devolverse a la agenda
            await Navigation.PushModalAsync(new EntAgenda(), true);
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            //llama al metodo sett
            Sett();
        }

        private void btnAgregarAgn(object sender, EventArgs e)
        {
            //si los picker  estan vacios
            if(cbEquipo.SelectedItem == null || cbCancha.SelectedItem == null || aDescripcion.SelectedItem == null)
            {
                //avisa
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");   
            }
            else
            {
                //si no estan vacios, llena los datos 

                DateTime fecha = fechaAgenda.Date;
                string hora =  horaAgenda.Time.ToString();
                string txt = fecha.ToString("yyyy-MM-dd") + " " +hora;

                //referencia al metodo que agrega a la base de datos la agenda
                clienteAgn.AgregarAgenda( idEquipo,cancha,txt,descripcion);
                DisplayAlert("Información", "Agenda agregada", "Aceptar");
                Sett();
            } 
        }
        //obtiene la informacion del picker de descripcion
        private void OnPickerSelectedIndexChangedDescripcion(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            descripcion = selectedItem.ToString();
        }

        //obtiene la informacion del picker de equipo
        private void OnPickerSelectedIndexChangedEquipo1(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            idEquipo = selectedItem.ToString();
        }

        //obtiene la informacion del picker de canchas
        private void OnPickerSelectedIndexChangedCanchas(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cancha = selectedItem.ToString();
        }
    }
}