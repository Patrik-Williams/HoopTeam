using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using HoopTeam.Paginas.Estudiantes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Estudiantes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstEditar : ContentPage
    {
        //referencias a la sesion del estudiantes y a los clientes
        ClienteEntrenador cEnt = new ClienteEntrenador();
        ClienteEstudiante cEst = new ClienteEstudiante();
        EstudianteEstatico estudiante = new EstudianteEstatico();
        public EstEditar()
        {
            InitializeComponent();

            //se llenan los campos de la pagina con la informacion del objeto estudiante
            txtNombre.Text = estudiante.getNombre();
            txtApellido1.Text = estudiante.getApellido1();
            txtApellido2.Text = estudiante.getApellido2();
            txtCorreo.Text = estudiante.getCorreo();
            txtContraseña.Text = estudiante.getContrasenna();
            cbGenero.SelectedItem = estudiante.getGenero();
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        async void Volver()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }
        private void ver_Cambios(object sender, EventArgs e)
        {
            //si los campos estan vacios
            if (txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || cbGenero.SelectedItem == null || txtCorreo.Text == "" || txtContraseña.Text == "")
            {
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }

            else
            {
                //se llenan las variables con los datos de los campos de la pagina
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string gen = cbGenero.SelectedItem.ToString();
                string correo = txtCorreo.Text;
                string con = txtContraseña.Text;
                string ced = estudiante.getCedula();

                //llamada al metodo editar estudiante
                cEst.actualizarEstudiante(nom, ap1, ap2, gen, correo, con, ced);
                DisplayAlert("Información: ", "Datos actualizados", "OK");
                Volver();

            }
        }
    }
}