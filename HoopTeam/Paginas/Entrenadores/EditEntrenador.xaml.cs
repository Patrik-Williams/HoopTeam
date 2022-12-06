using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEntrenador : ContentPage
    {
        EntrenadorNO_Estatico entrenador = new EntrenadorNO_Estatico();
        ClienteAdmin clienteAdm = new ClienteAdmin();
        public EditEntrenador(EntrenadorNO_Estatico ent)
        {
            InitializeComponent();
            entrenador = ent;
            txtNombre.Text = ent.Nombre;
            txtApellido1.Text = ent.Apellido1;
            txtApellido2.Text = ent.Apellido2;
            txtCorreo.Text = ent.Correo;
            txtContraseña.Text = "ent2022";
        }
        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEntrenadores(), true);
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void btnEditar(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtCorreo.Text == "" || txtContraseña.Text == "")
            {
                DisplayAlert("Alerta", "Debe llenar todos los datos", "Aceptar");
            }
            else
            {
                int ced = Int32.Parse(entrenador.Cedula);
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string correo = txtCorreo.Text;
                string contra = txtContraseña.Text;
                clienteAdm.EditarEntrenador(ced, nom, ap1, ap2, correo, contra);
                //clienteEnt.EditarInfoEst(ced, nom, ap1, ap2, correo, contra, eqNuevo, eqViejo, pago);
                Sett();
            }
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar al estudiante?", "Si", "No");
            if (answer)
            {
                if (clienteAdm.verificarEquipoEnt(Int32.Parse(entrenador.Cedula)) > 0) 
                {
                    DisplayAlert("Alerta", "El entrenador tiene un equipo a su nombre, cambie el equipo de entrenador para eliminar", "Ok");
                }else
                {
                    clienteAdm.EliminarEntrenador(Int32.Parse(entrenador.Cedula));
                    DisplayAlert("Informacion", "Entrenador eliminado", "Ok");
                    Sett();
                }
                //clienteEnt.EliminarEstudiante(Int32.Parse(est.Cedula));
                ;
            }
        }

        private void btnEliminar(object sender, EventArgs e)
        {
            ShowExitDialog();
        }
    }
}