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
    public partial class AgregarCancha : ContentPage
    {
        //referncia al cliente entrenador
        ClienteEntrenador clienteEnt = new ClienteEntrenador();
        public AgregarCancha()
        {
            InitializeComponent();
        }

        //devuelve a la pagina de canchas
        async void verCanchas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntCanchas(), true);
        }
        async void Volver()
        {
            await Navigation.PushModalAsync(new EntCanchas(), true);
        }

        private void btnAgregarCancha(object sender, EventArgs e)
        {
            //si el txt de cancha esta vacio
            if (txtUbicacion.Text == "")
            {
                //avisar
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            else
            {
                //si no esta vacio
                string ubicacion = txtUbicacion.Text;

                //llama al metodo que agrega la cancha a la base de datos 
                clienteEnt.AgregarCancha(ubicacion);
                DisplayAlert("Información", "Cancha agregada", "Aceptar");
                Volver();
            }
        }
    }
}