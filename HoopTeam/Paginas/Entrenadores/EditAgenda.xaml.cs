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
        //referencia a los entrenadores, agenda y equipos
        Agenda agn = new Agenda();
        AgendaEstatico agnE = new AgendaEstatico();
        Entrenador ent = new Entrenador();
        Equipos eq = new Equipos();

        //listas de agenda, canchas y de equipos
        List<Agenda> agnd = new List<Agenda>();
        List<Equipos> equipos = new List<Equipos>();
        List<Cancha> canchas { get; set; }
        //definicion de variables
        static int eqNuevo { get; set; }
        static int eqViejo { get; set; }
        static int cNueva { get; set; }
        static int cVieja { get; set; }
        static string cancha { get; set; }
        static string descripcion { get; set; }

        //referencia al cliente agenda
        ClienteAgenda clienteA = new ClienteAgenda();


        public EditAgenda(Agenda agenda, string idA, int idE)
        {
            InitializeComponent();

            this.agn = agenda;

            //trae los equipos y las canchas desde la base de datos
            equipos = clienteA.GetEquiposA(Int32.Parse(ent.getCedula()));
            canchas = clienteA.GetCanchasA();

            eqViejo = idE;
          
            fechaAgenda.Date = DateTime.Parse(agn.FechaHora.ToShortDateString());
            aDescripcion.SelectedItem = agn.Descripcion;

            //llena el picker de equipos
           foreach(Equipos eq in equipos)
            {
               cbEquipo.Items.Add(eq.idEquipo.ToString());
            }

           //llena el picker de canchas 
            foreach (Cancha cn in canchas)
            {
                cbCancha.Items.Add(cn.idCancha.ToString());
            }
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

        private void OnPickerASelectedIndexChanged(object sender, EventArgs e)
        {
            //consigue el equipo seleccionado en el picker
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            eqNuevo = Int32.Parse(selectedItem.ToString());

            //recorre la lista de equipos
            foreach(Equipos eq in equipos)
            {
                if(eq.idEquipo == eqNuevo)
                {
                    //si el equipo seleccionado es igual al de antes, el titulo dira ACTUAL 
                    if(eqNuevo==eqViejo)
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString() + "(Actual)";
                    }
                    //si no es igual, solo agrega la informacion del equipo al titulo 
                    else
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString();
                    }
                }
            }
        }

        private void OnPickerSelectedIndexChangedDescripcion(object sender, EventArgs e)
        {
            //consigue la descripcion de la agenda seleccionada en el picker
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            descripcion = selectedItem.ToString();
        }

        private void OnPickerSelectedIndexChangedCanchas(object sender, EventArgs e)
        {
            //consigue cancha seleccionada en el picker
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cancha = selectedItem.ToString();
        }


        private void btnEditarA(object sender, EventArgs e)
        {
            ClienteAgenda clienteA = new ClienteAgenda();
            if (aDescripcion.SelectedIndex == null )
            {
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            else
             {
                string Agenda = agn.idAgenda;

                DateTime fecha = fechaAgenda.Date;
                string hora = horaAgenda.Time.ToString();

                // string Cancha = txtCn.Text;
                // string FechaHora = txtFechaHora.Text;
                string txt = fecha.ToString("yyyy-MM-dd") + " " + hora;


                //Variable numérica para revisar si existe un evento a la misma hora en la misma cancha
                int x = clienteA.VerificarAgenda(cancha, fecha.ToString("yyyy-MM-dd"), hora);
                if (x == 0)
                {
                    //llama al método de editar y envia los datos necesarios
                    clienteA.EditarAgenda(Agenda, descripcion, txt);
                    DisplayAlert("Información", "Agenda editada", "Aceptar");
                    VolverA();
                }
                if (x != 0)
                {//Mensaje de error
                    DisplayAlert("Información", "Alerta! No puede agendar a esta hora, ya está reservada la cancha.", "Aceptar");
                }
            }
        

        }
        private async void ShowExitDialog()
        {
            //si se presiona el boton de eliminar despliega una advertencia
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea eliminar la agenda?", "Sí", "NO");
            //si responde que si
            if (answer)
            {
                //llama al metodo eliminar
                clienteA.EliminarAgenda(agn.idAgenda);
                DisplayAlert("Información", "Agenda eliminada", "Ok");
                VolverA();
            }
        }
        private void btnEliminarA(object sender, EventArgs e)
        {
            ShowExitDialog();
        }
    }
}