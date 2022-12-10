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
        static int equipoNuevo { get; set; }
        static int equipoViejo { get; set; }

        static int pago { get; set; }


        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        public EditEstudiante(string ced, int eqAc)
        {
            InitializeComponent();
            est = clienteEnt.GetEstudiante(ced);
            equipos = clienteEnt.GetEquiposGenero(est.Genero);

            equipoViejo = eqAc;

            foreach (Equipos e in equipos)
            {   


                cbEquipo.Items.Add(e.idEquipo.ToString());
            }
            Debug.WriteLine("Equipo Viejo " + equipoViejo);
            Debug.WriteLine(eqAc);

            txtNombre.Text = est.Nombre;
            txtApellido1.Text = est.Apellido1;
            txtApellido2.Text = est.Apellido2;
            //txtNacimiento.Text = est.Nacimiento.ToString();
            txtCorreo.Text = est.Correo;
            txtContraseña.Text = est.Contrasenna;
            fechaNacimiento.Date = est.Nacimiento;
            //DisplayAlert("Info", est.Nacimiento.ToString("yyyy-MM-dd"), "OK");

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
            equipoNuevo = Int32.Parse(selectedItem.ToString());

            foreach (Equipos eq in equipos)
            {
                if (eq.idEquipo == equipoNuevo)
                {
                    if(equipoNuevo == equipoViejo)
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString() + " (ACTUAL)";

                    }else
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString();
                    }
                    
                }
                
            }

            Debug.WriteLine(selectedItem.ToString() + " Selected");
            Debug.WriteLine("Nuevo " + equipoNuevo);
            Debug.WriteLine("Equipo Viejo " + equipoViejo);
        }

        private void OnPicker2SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            if (selectedItem.ToString().Equals("Realizado"))
            {
                pago = 1;
            }
            if (selectedItem.ToString().Equals("Por Realizar"))
            {
                pago = 0;
            }
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(pago);
            Debug.WriteLine("Pago " + pago);
        }

        private void btnEditar(object sender, EventArgs e)
        {
            if (cbEquipo.SelectedItem == null || cbPago.SelectedItem == null)
            {
                DisplayAlert("Alerta", "Debe seleccionar un equipo y Estado de pago", "Aceptar");
            }
            else
            {
                int ced = Int32.Parse(est.Cedula);
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string correo = txtCorreo.Text;
                string contra = txtContraseña.Text;
                int eqViejo = equipoViejo;
                int eqNuevo = equipoNuevo;
                DateTime date = fechaNacimiento.Date;

                clienteEnt.EditarInfoEst(ced, nom, ap1, ap2, date, correo, contra, eqNuevo, eqViejo, pago);

                Volver();
            }

            
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar al estudiante?", "Si", "No");
            if (answer)
            {
                clienteEnt.EliminarEstudiante(Int32.Parse(est.Cedula));
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