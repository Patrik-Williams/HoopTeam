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
            //si es entrenador o estudiante
            tabla = tab;

            //numero de verificacion enviado al email
            num = verif;

            //el correo de la persona
            this.correo = correo;

            //contador de intentos
            cont = 1;

            //se deshabilitan las opciones de cambiar contraseña, se mostraran cuando se verifique el codigo
            lbContra.IsVisible = false;
            lbVerContra.IsVisible = false;
            txtConta.IsVisible = false;
            txtVerContra.IsVisible = false;
            btnCambiarCont.IsVisible = false;
            mostrarContras.IsVisible = false;
        }

        //envia un email que informa que el cambio fue exitoso
        public void enviarEmail(string correo, string nombre)
        {
            try
            {
                //servicios necesarios
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");

                //email de la empresa
                mail.From = new MailAddress("hoopteamapp@outlook.com");
                //correo de la persona
                mail.To.Add(correo);
                //el sujeto
                mail.Subject = "Cambio de contraseña";
                //el mensaje del correo
                mail.Body = "Hola "+ nombre+", su contraseña de HoopTeam App ha sido cambiada";

                //puerto y servicio
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp-mail.outlook.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                //datos de verificacion del correo de la empresa
                SmtpServer.Credentials = new System.Net.NetworkCredential("hoopteamapp@outlook.com", "hoopteam123");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                DisplayAlert("Failed", ex.Message, "OK");
            }
        }


        private void ver_codigo(object sender, EventArgs e)
        {

            //mientras el contador sea menor a 3
            if (cont < 3) {
                //si el campo no esta vacio
                if(!string.IsNullOrEmpty(txtVerCodigo.Text))
                {
                    //si el numero en el campo es igual al numero de verificacion
                    if (Int32.Parse(txtVerCodigo.Text) == num)
                    {
                        //se habilitan los campos para cambiar la contraseña y se ocultan los de verificar codigo
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
                        //si el codigo es incorrecto, el contador se incrementa
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
                //si el contador es igual a 3, se excede la cantidad maxima de intentos 
                DisplayAlert("Alerta", "Código incorrecto, Ha excedido el número maximo de intentos", "Ok");
                Sett();
            }

        }

        private void cambiar_contra(object sender, EventArgs e)
        {
         
            //si los campos no estan vacios
            if (!string.IsNullOrEmpty(txtVerContra.Text) && !string.IsNullOrEmpty(txtConta.Text))
            {
                // si la contraseña y la verificacion son iguales
                if (txtConta.Text == txtVerContra.Text)
                {
                    //consulta a sql
                    string cedula = cliente.GetCedula(correo, tabla);
                   
                    //se envia el correo 
                    enviarEmail(correo, cliente.GetPersona(correo, tabla));

                    //llamada al metodo de cambiar contraseña
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
            //si el atributo de es contraseña es verdadero
            if (txtConta.IsPassword == true || txtVerContra.IsPassword == true)
            {
                //lo deshabilita y deja ver lo que se está escribiendo 
                txtConta.IsPassword = false;
                txtVerContra.IsPassword = false;
            }
            // si el atributo es falso
            else if (txtConta.IsPassword == false || txtVerContra.IsPassword == false)
            {
               //habilita el es contrasña y oculta lo que se está escribiendo
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