using HoopTeam.Implementacion;
using HoopTeam.Paginas;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Diagnostics;

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

        public void enviarEmail(string correo, int num)
        {
            try
            {


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");

                mail.From = new MailAddress("hoopteamapp@outlook.com");
                mail.To.Add(correo);
                mail.Subject = "Código de verificación";

                mail.Body = "Su código de verificación es: "+num;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp-mail.outlook.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hoopteamapp@outlook.com", "hoopteam123");

                SmtpServer.Send(mail);

                
            }
            catch (Exception ex)
            {
                DisplayAlert("Failed", ex.Message, "OK");

                
            }
        }

        private void ver_clicked(object sender, EventArgs e)
        {
            
            Cliente objetocliente = new Cliente();

            string correo = "";
            string contra = "";

            if (!string.IsNullOrEmpty(txtCorreo.Text) && !string.IsNullOrEmpty(txtContra.Text))
            {
                correo = txtCorreo.Text;
                contra = txtContra.Text;

                if (objetocliente.LogIn(correo, contra).Equals("Est"))
                {
                    DisplayAlert("Información: ", "Bienvenido estudiante", "OK");
                    Est();
                }
                else if (objetocliente.LogIn(correo, contra).Equals("Ent"))
                {
                    DisplayAlert("Información: ", "Bienvenido entrenador", "OK");
                    Ent();
                }
                else if(objetocliente.LogIn(correo, contra).Equals("Sup"))
                {
                    DisplayAlert("Información: ", "Bienvenido administrador", "OK");
                    Ent();
                }
                else
                {
                    DisplayAlert("Error: ", objetocliente.LogIn(correo, contra), "OK");
                }
            }
            else
            {
                DisplayAlert("Datos erróneos", "Por favor, llena toda la información", "Ok");

            }
        }

        private void verPass_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (txtContra.IsPassword == true)
            {
                txtContra.IsPassword = false;
            }
            else if (txtContra.IsPassword == false)
            {
                txtContra.IsPassword = true;
            }
        }

        private void olvidoContra(object sender, EventArgs e)
        {
            mensaje();
        }

        private async void mensaje()
        {
            string result = await DisplayPromptAsync("Olvidé mi contraseña", "Ingrese su correo electrónico", maxLength: default, keyboard: Keyboard.Default);
            if (result != "")
            {
                string verificacion = objetocliente.verEmail(result);
                if (verificacion!="")
                {
                    Random rnd = new Random();
                    int num = rnd.Next(100000, 999999);

                    enviarEmail(result, num);
                    Debug.WriteLine(num);
                    DisplayAlert("Información", "Hemos enviado un código de verificación a su correo electrónico", "ok");
                    olv(num, verificacion, result);

                }
                else
                {
                    DisplayAlert("Alerta", "El correo ingresado no está registrado", "ok");
                }
   

            }
        }

        private void ButtonEst_Clicked(object sender, EventArgs e)
        {
            Est();
        }

        async void olv(int num, string tab, string correo)
        {    
            await Navigation.PushModalAsync(new OlvideContraseña(num, tab, correo), true);
        }

    }
}
