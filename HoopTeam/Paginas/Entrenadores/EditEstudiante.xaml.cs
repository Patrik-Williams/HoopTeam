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
        //referencia a estudiantes y equipos 
        Estudiante est = new Estudiante();
        Equipos eq = new Equipos();

        //lista de equipos
        List<Equipos> equipos = new List<Equipos>();
        static int equipoNuevo { get; set; }
        static int equipoViejo { get; set; }
        static int pago { get; set; }

        //referencia al cliente entrenador
        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        public EditEstudiante(string ced, int eqAc)
        {
            InitializeComponent();
            //se trae la informacion del estudiante
            est = clienteEnt.GetEstudiante(ced);

            //se llena la lista de los equipos segun el genero del estudiante
            equipos = clienteEnt.GetEquiposGenero(est.Genero);

            equipoViejo = eqAc;

            //se llena el picker de quipos con la informacion en la lista
            foreach (Equipos e in equipos)
            {   
                cbEquipo.Items.Add(e.idEquipo.ToString());
            }

            //se llenan los campos de la pagina con la informacion del estudiante
            txtNombre.Text = est.Nombre;
            txtApellido1.Text = est.Apellido1;
            txtApellido2.Text = est.Apellido2;
            txtCorreo.Text = est.Correo;
            txtContraseña.Text = est.Contrasenna;
            fechaNacimiento.Date = est.Nacimiento;
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
            //consigue el elemento en el picker de equipos
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            equipoNuevo = Int32.Parse(selectedItem.ToString());

            //circula la lista de equipos
            foreach (Equipos eq in equipos)
            {
                //si el equipo seleccionado coincide con uno en la lista 
                if (eq.idEquipo == equipoNuevo)
                {
                    //si el id del equipo seleccionado es igual al que ya tenia, el titulo dira actual
                    if(equipoNuevo == equipoViejo)
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString() + " (ACTUAL)";

                    }else
                    {
                        cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString();
                    }
                }
            }
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
        }

        private void btnEditar(object sender, EventArgs e)
        {
            //si los campos necesarios estan vacios
            if (cbEquipo.SelectedItem == null || cbPago.SelectedItem == null || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtCorreo.Text == "" ||txtContraseña.Text=="")
            {
                DisplayAlert("Alerta", "Debe seleccionar un equipo y Estado de pago", "Aceptar");
            }
            else
            {
                //llena las variables con la informacion de los campos en la pagina
                int ced = Int32.Parse(est.Cedula);
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string correo = txtCorreo.Text;
                string contra = txtContraseña.Text;
                int eqViejo = equipoViejo;
                int eqNuevo = equipoNuevo;
                DateTime date = fechaNacimiento.Date;

                //llama al metodo de editar estudiante
                clienteEnt.EditarInfoEst(ced, nom, ap1, ap2, date, correo, contra, eqNuevo, eqViejo, pago);

                Volver();
            }

            
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("¡ALERTA!", "¿Seguro que desea Eliminar al estudiante?", "Sí", "No");
            if (answer)
            {
                clienteEnt.EliminarEstudiante(Int32.Parse(est.Cedula));
                DisplayAlert("Información", "Estudiante eliminado", "Ok");
                Volver();
            }
        }

        private void btnEliminar(object sender, EventArgs e)
        {
            ShowExitDialog();
        }


    }
}