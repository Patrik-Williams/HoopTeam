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
        //referencia al cliente de administrador
        ClienteAdmin clienteAdm = new ClienteAdmin();
        public AgregarEntrenador()
        {
            InitializeComponent();
        }

        //vuelve al la pagina de todos los entrenadores
        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEntrenadores(), true);
        }

        //llama al metodo sett
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void btnAgregar(object sender, EventArgs e)
        {
            // si los campos requeridos estan vacios 
            if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtCorreo.Text == "" || txtContraseña.Text == "")
            {
                //avisa
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            else
            { 
                //llena los campos con los txt de la pagina 
                int ced = Int32.Parse(txtCedula.Text);
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string correo = txtCorreo.Text;
                string contra = txtContraseña.Text;

                //llama al metodo que agrega un entrenador
                clienteAdm.AgregarEntrenador(ced, nom, ap1, ap2, correo, contra);
                DisplayAlert("Información", "Entrenador agregado", "Ok");
                Sett();
            }

        }
    }
}