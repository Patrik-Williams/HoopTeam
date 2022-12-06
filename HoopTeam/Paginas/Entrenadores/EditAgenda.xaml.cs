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
        AgendaEstatico agnE = new AgendaEstatico();
        Entrenador ent = new Entrenador();
        Equipos eq = new Equipos();
        List<Agenda> agnd = new List<Agenda>();
        List<Equipos> equipos = new List<Equipos>();
        static int eqNuevo { get; set; }
        static int eqViejo { get; set; }


        ClienteAgenda clienteA = new ClienteAgenda();


        public EditAgenda(Agenda agenda, string idA, int idE)
        {

          
            

            InitializeComponent();
            this.agn = agenda;

            //agn = clienteA.AgendaE(idA);
            equipos = clienteA.GetEquiposA(Int32.Parse(ent.getCedula()));

            eqViejo = idE;

            
           //txtEqp.Text = agn.Equipo;
           txtCn.Text = agn.Cancha;
           txtFechaHora.Text = agn.FechaHora;
           txtDesc.Text = agn.Descripcion;

           foreach(Equipos eq in equipos)
            {
                
                
               cbEquipo.Items.Add(eq.idEquipo.ToString());
            }

            Debug.WriteLine("Equipo Viejo " + eqViejo);
            Debug.WriteLine(idE);

         

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
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            eqNuevo = Int32.Parse(selectedItem.ToString());

            foreach(Equipos eq in equipos)
            {
                if(eq.idEquipo == eqNuevo)
                {
                    if(eqNuevo==eqViejo)
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString() + "(Actual)";

                    }
                    else
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString();
                    }
                }
            }

            Debug.WriteLine(selectedItem.ToString() + "Selected");
            Debug.WriteLine("Nuevo " + eqNuevo);
            Debug.WriteLine("Equipo Viejo " + eqViejo);

        }


        private void btnEditarA(object sender, EventArgs e)
        {
            ClienteAgenda clienteA = new ClienteAgenda();
                
           
            string Agenda = agn.idAgenda;
           // string Equipo = txtEqp.Text;
            string Cancha = txtCn.Text;
            string FechaHora = txtFechaHora.Text;
            string Descripcion = txtDesc.Text;
            //int equipoViejo = eqViejo;
            //int equipoNuevo = eqNuevo;

            try
            {

              clienteA.EditarAgenda(Agenda, Cancha, Descripcion);

                VolverA();
            }
            catch(Exception ex)
            {
                DisplayAlert("Información Actualizada", "Agenda", "OK");
            }
           

        }
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar al agenda?", "Si", "NO");
            if (answer)
            {
                clienteA.EliminarAgenda(agn.idAgenda);
                DisplayAlert("Informacion", "Agenda eliminado", "Ok");
                VolverA();
            }
        }
        private void btnEliminarA(object sender, EventArgs e)
        {
            ShowExitDialog();
        }
    }
}