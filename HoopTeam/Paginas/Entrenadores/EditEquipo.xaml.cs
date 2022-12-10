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
        List<EntrenadorNO_Estatico> entrenadores = new List<EntrenadorNO_Estatico>();

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

            entrenadores = clienteAdm.GetEntrenadores();

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
            if (cbEntrenador.SelectedItem == null || cbGenero.SelectedItem == null)
            {
                DisplayAlert("Alerta", "Debe seleccionar un entrenador y el género", "Aceptar");
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
           var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea eliminar este equipo?", "Sí", "No");
            if (answer)
            {
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
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            gen = selectedItem.ToString();
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
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cedEnt = Int32.Parse(selectedItem.ToString());
            foreach (EntrenadorNO_Estatico et in entrenadores)
            {
                if (Int32.Parse(et.Cedula) == cedEnt)
                {
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