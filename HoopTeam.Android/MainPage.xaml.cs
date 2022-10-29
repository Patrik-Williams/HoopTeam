using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HoopTeam.Paginas;


namespace HoopTeam
{
    public partial class MainPage : ContentPage
    {
        Cliente objetocliente = new Cliente();
        public MainPage()
        {
            InitializeComponent();
            ;
        }

        async void Est()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }
        async void Ent()
        {
            await Navigation.PushModalAsync(new EntMain(), true);
        }

        private void ver_clicked(object sender, EventArgs e)
        {

            Cliente objetocliente = new Cliente();

            string correo = "";
            string contra = "";

            if(!string.IsNullOrEmpty(txtCorreo.Text) && !string.IsNullOrEmpty(txtContra.Text))
            {
                correo = txtCorreo.Text;
                contra = txtContra.Text;
                
                if(objetocliente.LogIn(correo, contra).Equals("Est"))
                {
                    DisplayAlert("Información: ", "Bienvenido Estudiante", "OK");
                    Est();                
                }else if (objetocliente.LogIn(correo, contra).Equals("Ent")) {
                    DisplayAlert("Información: ", "Bienvenido Entrenador", "OK");
                    Ent();

                }
                else
                {
                    DisplayAlert("Error: ", objetocliente.LogIn(correo, contra), "OK");
                }
                    

            }
            else
            {
                DisplayAlert("Datos errones", "Por favor, llena toda la información", "Ok");
                
            }
            


        }

        private void verPass_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (txtContra.IsPassword == true)
            {
                txtContra.IsPassword = false;
            }else if (txtContra.IsPassword == false)
            {
                txtContra.IsPassword = true;
            }
        }
	private void ButtonEst_Clicked(object sender, EventArgs e)
        {
            Est();
        }
    }
}
