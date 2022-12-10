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
        List<EntrenadorNO_Estatico> entrenadores = new List<EntrenadorNO_Estatico>();

        EntrenadorNO_Estatico ent = new EntrenadorNO_Estatico();
        static int cedEnt { get; set; }

        ClienteAdmin clienteAdm = new ClienteAdmin();

        static string gen { get; set; }
        public AgregarEquipos()
        {
            InitializeComponent();
            entrenadores = clienteAdm.GetEntrenadores();

            foreach(EntrenadorNO_Estatico et in entrenadores)
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

        private void btnAgregar(object sender, EventArgs e)
        {
            if (txtCate.Text == "" )
            {
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            /*int ced = Int32.Parse(txtCedula.Text);
            string nom = txtNombre.Text;
            string ap1 = txtApellido1.Text;
            string ap2 = txtApellido2.Text;
            string correo = txtCorreo.Text;
            string contra = txtContraseña.Text;
            clienteEnt.AgregarEstudiante(ced, nom, ap1, ap2, genero[0].ToString(), correo, contra, Int32.Parse(equipo[0].ToString()));
            DisplayAlert("Informacion", "Estudiante agregado", "Ok");*/
            else {
                string categoria = txtCate.Text;
                int cupo = Int32.Parse(txtCupo.Text);

                clienteAdm.AgregarEquipo(categoria, gen[0].ToString(), cedEnt, cupo);
                DisplayAlert("Informacion", "Equipo agregado", "Ok");
                Sett();
            }
        }
        private void OnPickerSelectedIndexChangedGenero(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            gen = selectedItem.ToString();

        }

        private void OnPickerSelectedIndexChangedEntrenador(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            cedEnt = Int32.Parse(selectedItem.ToString());
            foreach (EntrenadorNO_Estatico et in entrenadores)
            {
                if(Int32.Parse(et.Cedula) == cedEnt)
                {
                    cbEntrenador.Title = et.NombreCompleto;
                }
            }
        }
    }
}