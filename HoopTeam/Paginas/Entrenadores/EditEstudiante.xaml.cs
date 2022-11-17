using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEstudiante : ContentPage

    {
        Estudiante est = new Estudiante();
        Equipos eq = new Equipos();
        List<Equipos> equipos = new List<Equipos>();
        static string equipoNuevo { get; set; }
        static string equipoViejo { get; set; }


        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        public EditEstudiante(string ced, string eqAc)
        {
            InitializeComponent();
            est = clienteEnt.GetEstudiante(ced);
            equipos = clienteEnt.GetEquiposGenero(est.Genero);

            equipoViejo = eqAc;

            foreach (Equipos e in equipos)
            {   
                if(e.idEquipo == Int32.Parse(eqAc[0].ToString()))
                {
                    cbEquipo.Items.Add(e.idEquipo.ToString() + " " + e.categoria.ToString() + " (ACTUAL)");
                }
                else
                {
                    cbEquipo.Items.Add(e.idEquipo.ToString() + " " + e.categoria.ToString());
                }
                
            }
            Debug.WriteLine("Equipo Viejo " + equipoViejo);
            Debug.WriteLine(eqAc);
            Debug.WriteLine(eqAc[0]);

            txtNombre.Text = est.Nombre;
            txtApellido1.Text = est.Apellido1;
            txtApellido2.Text = est.Apellido2;
            txtNacimiento.Text = est.Nacimiento;
            txtCorreo.Text = est.Correo;
            txtContraseña.Text = est.Contrasenna;
        }
        async void Sett()
        {
            await Navigation.PushModalAsync(new EntEstudiantes(), true);
        }

       
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        } 
        async void Volver()
        {
            await Navigation.PushModalAsync(new EntEstudiantes(), true);
        }
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            equipoNuevo = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(equipoNuevo);
            Debug.WriteLine("Equipo Viejo " + equipoViejo);
        }

        private void btnEditar(object sender, EventArgs e)
        {
            int ced = Int32.Parse(est.Cedula);
            string nom = txtNombre.Text;
            string ap1 = txtApellido1.Text;
            string ap2 = txtApellido2.Text;
            string correo = txtCorreo.Text;
            string contra = txtContraseña.Text;
            int eqViejo = Int32.Parse(equipoViejo[0].ToString());
            int eqNuevo = Int32.Parse(equipoNuevo[0].ToString());
            clienteEnt.EditarInfoEst(ced, nom, ap1, ap2, correo, contra, eqNuevo, eqViejo);

            Volver();
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar al estudiante?", "Si", "No");
            if (answer)
            {
                //clienteEnt.EliminarEstudiante(Int32.Parse(est.Cedula));
                DisplayAlert("Informacion", "Estudiante eliminado", "Ok");
                Volver();
            }
        }

        private void btnEliminar(object sender, EventArgs e)
        {
            ShowExitDialog();
        }
    }
}