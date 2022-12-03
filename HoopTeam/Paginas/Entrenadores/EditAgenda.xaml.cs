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
        ClienteAgenda clienteA = new ClienteAgenda();


        public EditAgenda( Agenda agenda)
        {

            this.agn = agenda;
            InitializeComponent();


            
            txtEqp.Text = agn.Equipo;
            txtCn.Text = agn.Cancha;
            txtFechaHora.Text = agn.FechaHora;
            txtDesc.Text = agn.Descripcion;

          

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
  
        private void btnEditarA(object sender, EventArgs e)
        {
            ClienteAgenda clienteA = new ClienteAgenda();
                
           
            string Agenda = agn.idAgenda;
            string Equipo = txtEqp.Text;
            string Cancha = txtCn.Text;
            string FechaHora = txtFechaHora.Text;
            string Descripcion = txtDesc.Text;

            try
            {

                clienteA.EditarAgenda(Agenda, Equipo, Cancha, FechaHora, Descripcion);

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