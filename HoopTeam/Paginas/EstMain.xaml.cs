using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoopTeam.Implementacion;
using HoopTeam;

namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstMain : ContentPage
    {
        Cliente objCliente = new Cliente();
        Datos datos = new Datos();
        public EstMain()
        {
            InitializeComponent();
            
            lbInfo.Text = objCliente.MostrarNombre(datos.getCedula(), "Estudiantes");
        }


    }
}