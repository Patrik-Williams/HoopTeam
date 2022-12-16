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
    public partial class AgregarEst : ContentPage
    {
        //lista de tipo equipos
        List<Equipos> equipos = new List<Equipos>();
        static int equipo { get; set; }

        Entrenador ent = new Entrenador();
        static string genero { get; set; }

        //referencia al clinete entrenador
        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        Administrador adm = new Administrador();

        

        public AgregarEst()   
        {
            InitializeComponent();
            
        }

        //vuelve a la pagina de Todos los estudiantes
        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEstudiantes(), true);  
        }

        async void Volver()
        {
            //si el usuario es un super admin, lo envia directo al menu principal
            if (adm.getSuperUser())
            {
                await Navigation.PushModalAsync(new TodosEstudiantes(), true);
            }
            else
            {
                await Navigation.PushModalAsync(new EntEstudiantes(), true);
            }
            
        }
        //llama al metodo Sett
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }


        private void btnAgregar(object sender, EventArgs e)
        {
            //si los campos necesarios estan vacios
            if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtCorreo.Text == "" || txtContraseña.Text == "" || cbEquipo.SelectedItem == null || cbGenero.SelectedItem == null)
            {
                //avisa
                DisplayAlert("Alerta", "Debe llenar todos los campos", "Aceptar");
            }
            else
            {
                //llena las variables con los datos en la pagina
                int ced = Int32.Parse(txtCedula.Text);
                string nom = txtNombre.Text;
                string ap1 = txtApellido1.Text;
                string ap2 = txtApellido2.Text;
                string correo = txtCorreo.Text;
                string contra = txtContraseña.Text;
                DateTime fecha = fechaNacimiento.Date;

                //envia los datos al metodo de agregar en el cliente entrenador
                clienteEnt.AgregarEstudiante(ced, nom, ap1, ap2, fecha, genero[0].ToString(), correo, contra, equipo);
                DisplayAlert("Información", "Estudiante agregado", "Ok");
                Volver();
            }

            

        }
        private void OnPickerSelectedIndexChangedGenero(object sender, EventArgs e)
        {
            //consigue el genero seleccionado en el picker y lo coloca en una variable
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            genero = selectedItem.ToString();

            //si es un administrador, llena la lista de equipos con todos los equipos existentes
            if (adm.getSuperUser())
            {
                equipos = clienteEnt.GetEquiposGenero(genero[0].ToString());
            }
            //si no es un administrador, llena la lista de equipos con los equipos que tiene el entrenador pasando por parametro la cedula de el al metodo 
            else
            {
                equipos = clienteEnt.GetEquiposGen_Ent(genero[0].ToString(), Int32.Parse(ent.getCedula()));
            }
            
            //limpia el picker de equipos
            cbEquipo.Items.Clear();
            //lo vuelve a llenar con la nueva lista de equipos
            //se hace asi para que el picker cambie los equipos que tiene segun el genero seleccionado
            foreach (Equipos eq in equipos)
            {
                cbEquipo.Items.Add(eq.idEquipo.ToString());    
            }

        }

        private void OnPickerSelectedIndexChangedEquipo(object sender, EventArgs e)
        {
            //consigue el equipo seleccionado en el picker y lo coloca en una variable
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            equipo = Int32.Parse(selectedItem.ToString());

            //compara el id del equipo con los de la lista para conseguir toda la informacion 
            foreach (Equipos eq in equipos)
            {
                if (eq.idEquipo == equipo)
                {
                    //concatena la informacion del equipo y la pone en el titulo del picker
                    cbEquipo.Title = eq.idEquipo.ToString() + " " + eq.categoria.ToString();
                }
            }
        }
    }
}