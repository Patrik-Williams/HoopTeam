using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoopTeam.Modelo;
using HoopTeam.Implementacion;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntEditar : ContentPage
    {
        //referencia al cliente entrenador
        ClienteEntrenador cEnt = new ClienteEntrenador();
        Entrenador ent = new Entrenador();

        public EntEditar()
        {
            InitializeComponent();

            //llena los campos de la pagina con la informacion del entrenador
            lblN.Text = ent.getNombre();
            lbA1.Text = ent.getApellido1();
            lbA2.Text = ent.getApellido2();
            lbCor.Text = ent.getCorreo();
            lbCon.Text = ent.getContrasenna();

        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EntMain(), true);
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void do_Change(object sender, EventArgs e)
        {

            ClienteEntrenador entC = new ClienteEntrenador();

            //si los campos estan vacios
            if (lblN.Text == "" || lbA1.Text == "" || lbA2.Text == "" || lbCor.Text == "" || lbCon.Text == "")
            {
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            else
            {
                //llena las variables con la informacion de los campos de la pagina
                string nomE = lblN.Text;
                string ApE = lbA1.Text;
                string Ap2E = lbA2.Text;
                string Cor = lbCor.Text;
                string Con = lbCon.Text;
                string ced = ent.getCedula();


                //se llama al metodo de editar entrenador
                entC.actualizarEntrenador(nomE, ApE, Ap2E, Cor, Con, ced);
                DisplayAlert("Información: ", "Datos actualizados", "OK");
                Sett();
            }
        }
    }
}