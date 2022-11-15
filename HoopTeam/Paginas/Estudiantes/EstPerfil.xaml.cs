using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using HoopTeam.Paginas.Estudiantes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace HoopTeam.Paginas.Estudiantes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntEstt : ContentPage
    {
        ClienteEstudiante objClienteE = new ClienteEstudiante();
        EstudianteEstatico estudiante = new EstudianteEstatico();
        public EntEstt()
        {
          
            InitializeComponent();


            txtNombre.Text = estudiante.getNombre();
            txtApellido1.Text = estudiante.getApellido1();
            txtApellido2.Text = estudiante.getApellido2();
            txtCorreo.Text =  estudiante.getCorreo();
            txtContraseña.Text = estudiante.getContrasenna();


        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        private void ver_Cambios(object sender, EventArgs e)
        {
            //al hacer click hace el cambio de datos
            ClienteEstudiante objClienteE = new ClienteEstudiante();

            string nom = txtNombre.Text;
            string ap1 = txtApellido1.Text;
            string ap2 = txtApellido2.Text;
            string correo = txtCorreo.Text;
            string con = txtContraseña.Text;
            string ced = estudiante.getCedula();

            try
            {
                objClienteE.actualizarEstudiante(nom, ap1, ap2, correo, con, ced);
                DisplayAlert("Información: ", "Datos actualizados", "OK");
                Sett();
            }
            catch (Exception ex)
            {
                DisplayAlert("Información Actualizada", "Perfil", "OK");
            }

        }


    }
}