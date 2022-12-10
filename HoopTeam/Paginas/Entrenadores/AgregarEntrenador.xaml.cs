using HoopTeam.Implementacion;
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
    public partial class AgregarEntrenador : ContentPage
    {
        ClienteAdmin clienteAdm = new ClienteAdmin();
        public AgregarEntrenador()
        {
            InitializeComponent();
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEntrenadores(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void btnAgregar(object sender, EventArgs e)
        {
            if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtCorreo.Text == "" || txtContraseña.Text == "")
            {
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            else
            { 
                int ced = Int32.Parse(txtCedula.Text);
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string correo = txtCorreo.Text;
                string contra = txtContraseña.Text;

                //clienteEnt.AgregarEstudiante(ced, nom, ap1, ap2, genero[0].ToString(), correo, contra, equipo);
                clienteAdm.AgregarEntrenador(ced, nom, ap1, ap2, correo, contra);
                DisplayAlert("Información", "Entrenador agregado", "Ok");
                Sett();
            }

        }
    }
}