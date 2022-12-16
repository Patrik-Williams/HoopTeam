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
    public partial class EditEquipo : ContentPage
    {
        //lista de entrenadores no estaticos, permite ser actualizada 
        List<EntrenadorNO_Estatico> entrenadores = new List<EntrenadorNO_Estatico>();

        //referencias a los entrenadores y equipo
        EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
        ClienteAdmin clienteAdm = new ClienteAdmin();
        Equipos equipo = new Equipos();
        static int cedEnt { get; set; }
        static string gen { get; set; }
        public EditEquipo(Equipos eq)
        {
            InitializeComponent();
            equipo = eq;
            txtCate.Text = eq.categoria;
            txtCupo.Text = eq.cupo.ToString();

            //se llena la lista de entrenadores 
            entrenadores = clienteAdm.GetEntrenadores();

            //se llena el picker con los entrenadores de la lista 
            foreach (EntrenadorNO_Estatico et in entrenadores)
            {
                cbEntrenador.Items.Add(et.Cedula);
            }

        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEquipos(), true);
        }


        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        async void Volver()
        {
            await Navigation.PushModalAsync(new TodosEquipos(), true);
        }

        private void btnEditar(object sender, EventArgs e)
        {
            //si los campos necesarios estan vacios 
            if (cbEntrenador.SelectedItem == null || cbGenero.SelectedItem == null || txtCate.Text == "" || txtCupo.Text == "" )
            {
                //avisa
                DisplayAlert("Alerta", "Debe seleccionar un Entrenador, genero y categoria", "Aceptar");
            }
            else
            {
                int cupo = Int32.Parse(txtCupo.Text);
                string cate = txtCate.Text;
                //metodo de update
                clienteAdm.EditarEquipo(cate, gen[0].ToString(), cedEnt, cupo, equipo.idEquipo);

                Volver();
            }
        }

        private async void ShowExitDialog()
        {
            //aviso antes de eliminar, pide confirmacion 
           var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea eliminar este equipo?", "Sí", "No");
            //si la respuesta es si
            if (answer)
            {
                //se llama al metodo de eliminar un equipo 
                clienteAdm.EliminarEquipo(equipo.idEquipo);
                DisplayAlert("Información", "Equipo eliminado", "Ok");
                Volver();
            }
        }

        private void btnEliminar(object sender, EventArgs e)
        {
            ShowExitDialog();
        }

        private void OnPickerSelectedIndexChangedGenero(object sender, EventArgs e)
        {
            //consigue el elemento seleccionado en el picker de genero
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            gen = selectedItem.ToString();

            //si el genero seleccionado es igual al que tenia antes, se escribe ACTUAL en el titulo del picker
            if(gen[0].ToString() == equipo.genero)
            {
                cbGenero.Title = "(ACTUAL)";
            }
            else
            {
                cbGenero.Title = "";
            }
        }

        private void OnPickerSelectedIndexChangedEntrenador(object sender, EventArgs e)
        {
            //consigue el elemento seleccionado del picker de entrenador
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cedEnt = Int32.Parse(selectedItem.ToString());

            //se cicula la lista de entrenadores
            foreach (EntrenadorNO_Estatico et in entrenadores)
            {
                //si la cedula seleccionada es igual a una cedula en la lista 
                if (Int32.Parse(et.Cedula) == cedEnt)
                {   
                    //si la seleccionada es igual el equipo ya tenia, es escribe ACTUAL en el titulo del picker
                    if(cedEnt == equipo.cedEntrenador)
                    {
                        cbEntrenador.Title = et.NombreCompleto + " (ACTUAL)";
                    }
                    else
                    {
                        cbEntrenador.Title = et.NombreCompleto;
                    }   
                }
                
            }
        }
    }
}