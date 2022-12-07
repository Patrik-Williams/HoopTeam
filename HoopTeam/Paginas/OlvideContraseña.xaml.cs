using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OlvideContraseña : ContentPage
    {
        int num { get; set; }
        int cont { get; set; }
        string tabla { get; set; }
        string correo { get; set; }

        Cliente cliente = new Cliente();
        public OlvideContraseña(int verif, string tab, string correo)
        {
            InitializeComponent();
            tabla = tab;
            num = verif;
            this.correo = correo;
            cont = 1;
            lbContra.IsVisible = false;
            lbVerContra.IsVisible = false;
            txtConta.IsVisible = false;
            txtVerContra.IsVisible = false;
            btnCambiarCont.IsVisible = false;
            mostrarContras.IsVisible = false;

            Debug.WriteLine(tab);

        }

        public void enviarEmail(string correo, string nombre)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");

                mail.From = new MailAddress("hoopteamapp@outlook.com");
                mail.To.Add(correo);
                mail.Subject = "Cambio de contraseña";

                mail.Body = "Hola "+ nombre+", su contraseña de HoopTeam App ha sido cambiada";

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp-mail.outlook.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hoopteamapp@outlook.com", "hoopteam123");

                SmtpServer.Send(mail);


            }
            catch (Exception ex)
            {
                DisplayAlert("Faild", ex.Message, "OK");


            }
        }


        private void ver_codigo(object sender, EventArgs e)
        {

            if (cont < 3) {
                if(!string.IsNullOrEmpty(txtVerCodigo.Text))
                {
                    if (Int32.Parse(txtVerCodigo.Text) == num)
                    {
                        DisplayAlert("Información", "Código correcto", "Continuar");
                        lbVerCodigo.IsVisible = false;
                        txtVerCodigo.IsVisible = false;
                        btnVerCod.IsVisible = false;

                        lbContra.IsVisible = true;
                        lbVerContra.IsVisible = true;
                        txtConta.IsVisible = true;
                        txtVerContra.IsVisible = true;
                        btnCambiarCont.IsVisible = true;
                        mostrarContras.IsVisible = true;

                    }
                    else
                    {
                        cont += 1;
                        DisplayAlert("Información", "Código incorrecto", "Continuar");
                    }
                }
                else
                {
                    DisplayAlert("Alerta", "Debe ingresar un código", "OK");
                }
                
            }
            else
            {
                DisplayAlert("Alerta", "Código incorrecto, Ha exedido el número maximo de intentos", "Ok");
                Sett();
            }

        }

        private void cambiar_contra(object sender, EventArgs e)
        {
         
            if (!string.IsNullOrEmpty(txtVerContra.Text) && !string.IsNullOrEmpty(txtConta.Text))
            {
                if (txtConta.Text == txtVerContra.Text)
                {
                    //consulta a sql
                    string cedula = cliente.GetCedula(correo, tabla);
                    Debug.WriteLine(cedula);
                    enviarEmail(correo, cliente.GetPersona(correo, tabla));
                    cliente.CambiarContrasenna(cedula, txtConta.Text, tabla);
                    DisplayAlert("Información", "El cambio de contraseña se ha realizado correctamente", "Continuar");
                    Sett();
                }
                else
                {
                    DisplayAlert("Alerta", "Ambas contraseñas deben ser iguales", "OK");
                }
            }
            else
            {
                DisplayAlert("Alerta", "Por Favor llene los datos", "OK");
            }
            

        }

        private void verPass_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (txtConta.IsPassword == true || txtVerContra.IsPassword == true)
            {
                txtConta.IsPassword = false;
                txtVerContra.IsPassword = false;
            }
            else if (txtConta.IsPassword == false || txtVerContra.IsPassword == false)
            {
                txtConta.IsPassword = true;
                txtVerContra.IsPassword = true;
            }
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new MainPage(), true);
        }


        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

    }
}