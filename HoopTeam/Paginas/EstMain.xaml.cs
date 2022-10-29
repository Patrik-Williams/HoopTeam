using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstMain : ContentPage
    {
        Cliente objCliente = new Cliente();
        
        Estudiante estudiante = new Estudiante();
        public EstMain()
        {
            InitializeComponent();

            //lbInfo.Text = objCliente.MostrarNombre(datos.getCedula(), "Estudiantes");
            lbInfo.Text = estudiante.getNombre() + " " + estudiante.getApellido1() + " " + estudiante.getApellido2();
            lbCorreo.Text = estudiante.getCorreo();
            lbCedula.Text = estudiante.getCedula();
        }

        async void LogOut()
        {
            estudiante.setCedula("");
            estudiante.setNombre("");
            estudiante.setApellido1("");
            estudiante.setApellido2("");
            estudiante.setCorreo("");
            estudiante.setContrasenna("");
            estudiante.setGenero("");
            estudiante.setNacimiento("");
            await Navigation.PushModalAsync(new MainPage(), true);
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            LogOut();
        }
    }
}