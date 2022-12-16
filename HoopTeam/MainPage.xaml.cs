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

        //envia el email con el codigo de verificacion
        public void enviarEmail(string correo, int num)
        {
            try
            {

                //servicios
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");

                //correo de la empresa
                mail.From = new MailAddress("hoopteamapp@outlook.com");
                //correo del destinatario
                mail.To.Add(correo);
                //titulo
                mail.Subject = "Código de verificación";
                //mensaje
                mail.Body = "Su código de verificación es: "+num;
                //puerto 
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp-mail.outlook.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                //credenciales
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
            //llamda al cliente principal
            Cliente objetocliente = new Cliente();

            string correo = "";
            string contra = "";

            //si los campos no estan vacios
            if (!string.IsNullOrEmpty(txtCorreo.Text) && !string.IsNullOrEmpty(txtContra.Text))
            {
                //se llenan las variable con la informacion de los campos
                correo = txtCorreo.Text;
                contra = txtContra.Text;

                //si el cliente login devuelve EST entra a la pagina pincipal del estudiante
                if (objetocliente.LogIn(correo, contra).Equals("Est"))
                {
                    DisplayAlert("Información: ", "Bienvenido estudiante", "OK");
                    Est();
                }
                //si el cliente login devuelve ENT entra a la pagina pincipal del etrenador
                else if (objetocliente.LogIn(correo, contra).Equals("Ent"))
                {
                    DisplayAlert("Información: ", "Bienvenido entrenador", "OK");
                    Ent();
                }
                //si el cliente login devuelve SUP entra a la pagina pincipal del admin
                else if (objetocliente.LogIn(correo, contra).Equals("Sup"))
                {
                    DisplayAlert("Información: ", "Bienvenido administrador", "OK");
                    Ent();
                }
                // si devuleve cualquier otra cosa, dira que hay un error
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
            //pestaña para ingresar el correo en caso de olvidar la contra
            string result = await DisplayPromptAsync("Olvidé mi contraseña", "Ingrese su correo electrónico", maxLength: default, keyboard: Keyboard.Default);
            //si es diferente a nulo
            if (result != "")
            {
                //se verifica que el correo exista en la base
                string verificacion = objetocliente.verEmail(result);
                //si existe
                if (verificacion!="")
                {
                    //se genera un numero aleatorio entre cien mil y casi un millon
                    Random rnd = new Random();
                    int num = rnd.Next(100000, 999999);

                    //se envia el correo
                    enviarEmail(result, num);
                   
                    DisplayAlert("Información", "Hemos enviado un código de verificación a su correo electrónico", "ok");
                    //abre la pagina de olvide contra
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
