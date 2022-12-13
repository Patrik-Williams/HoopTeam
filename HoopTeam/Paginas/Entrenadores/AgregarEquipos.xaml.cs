using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
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
    public partial class AgregarEquipos : ContentPage
    {
        //lista de entrenadores no estaticos
        List<EntrenadorNO_Estatico> entrenadores = new List<EntrenadorNO_Estatico>();

        //referencia al entrenador
        EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
        static int cedEnt { get; set; }

        ClienteAdmin clienteAdm = new ClienteAdmin();

        static string gen { get; set; }
        public AgregarEquipos()
        {
            InitializeComponent();
            //llena la lista de entrenadores 
            entrenadores = clienteAdm.GetEntrenadores();

            //llena el picker con los objetos de la lista 
            foreach(EntrenadorNO_Estatico et in entrenadores)
            {
                cbEntrenador.Items.Add(et.Cedula);
            }
        }

        //devuelve a la pagina de todos los equipos
        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEquipos(), true);
        }

        //llama al metodo sett
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void btnAgregar(object sender, EventArgs e)
        {
            //si la categoria está vacia 
            if (txtCate.Text == "" )
            {
                //avisa
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }

            else {
                //si no está vacia 
                string categoria = txtCate.Text;
                int cupo = Int32.Parse(txtCupo.Text);

                //llama al metodo que agrega un equipo a la base de datos 
                clienteAdm.AgregarEquipo(categoria, gen[0].ToString(), cedEnt, cupo);
                DisplayAlert("Informacion", "Equipo agregado", "Ok");
                Sett();
            }
        }

        //obtiene el item seleccionado en el picker de genero
        private void OnPickerSelectedIndexChangedGenero(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;

            //carga la variable de genero 
            gen = selectedItem.ToString();
        }

        //obtiene el item del picker de entrenador
        private void OnPickerSelectedIndexChangedEntrenador(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cedEnt = Int32.Parse(selectedItem.ToString());

            //circula la lista de entrenadores
            foreach (EntrenadorNO_Estatico et in entrenadores)
            {
                //si la cedula de la lista coincide con la de del picker
                if(Int32.Parse(et.Cedula) == cedEnt)
                {
                    //pone el nombre completo del entrenador en el titulo del picker
                    cbEntrenador.Title = et.NombreCompleto;
                }
            }
        }
    }
}