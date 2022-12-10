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

        static int cNueva { get; set; }
        static int cVieja { get; set; }

        List<Cancha> canchas { get; set; }
        static string cancha { get; set; }


        ClienteAgenda clienteA = new ClienteAgenda();


        public EditAgenda(Agenda agenda, string idA, int idE)
        {

          
            

            InitializeComponent();
            this.agn = agenda;

            //agn = clienteA.AgendaE(idA);
            equipos = clienteA.GetEquiposA(Int32.Parse(ent.getCedula()));
            canchas = clienteA.GetCanchasA();

            eqViejo = idE;

            
           //txtEqp.Text = agn.Equipo;
           //txtCn.Text = agn.Cancha;
           //txtFechaHora.Text = agn.FechaHora;
           txtDesc.Text = agn.Descripcion;

           foreach(Equipos eq in equipos)
            {
                
                
               cbEquipo.Items.Add(eq.idEquipo.ToString());
            }
            foreach (Cancha cn in canchas)
            {
                cbCancha.Items.Add(cn.idCancha.ToString());
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
        private void OnPickerSelectedIndexChangedCanchas(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cancha = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(cancha);

        }


        private void btnEditarA(object sender, EventArgs e)
        {
            ClienteAgenda clienteA = new ClienteAgenda();
            try

            {
                string Agenda = agn.idAgenda;
                // string Cancha = txtCn.Text;
                string FechaHora = txtFechaHora.Text;
                string Descripcion = txtDesc.Text;
                clienteA.EditarAgenda(Agenda, Descripcion);

                VolverA();
            }
            catch (Exception ex)
            {
                string txt = ex.Message;
            }

        }
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar la agenda?", "Si", "NO");
            if (answer)
            {
                clienteA.EliminarAgenda(agn.idAgenda);
                DisplayAlert("Informacion", "Agenda eliminada", "Ok");
                VolverA();
            }
        }
        private void btnEliminarA(object sender, EventArgs e)
        {
            ShowExitDialog();
        }
    }
}