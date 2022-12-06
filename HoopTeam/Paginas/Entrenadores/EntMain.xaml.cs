using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using HoopTeam.Paginas.Entrenadores;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace HoopTeam.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntMain : ContentPage
    {
        Cliente objCliente = new Cliente();

        Entrenador entrenador = new Entrenador();

        Administrador adm = new Administrador();
        public EntMain()
        {
            InitializeComponent();

            if(adm.getSuperUser())
            {
                lbInfo.Text = "ADMINISTRADOR";
                lbCorreo.Text = adm.getCorreo();
                lbCedula.Text = adm.getIdAdmin().ToString();
                perfil.BackgroundColor = Color.Green;

                TodosEstudiantes.IsVisible = true;
                entEstudiantes.IsVisible = false;

                todosEquipos.IsVisible = true;
                entEquipos.IsVisible = false;

                todaAgenda.IsVisible = true;
                agenda.IsVisible = false;

                Entrenadores.IsVisible = true;
                Canchas.IsVisible = true;
            }
            else if (adm.getSuperUser()==false)
            {
                lbInfo.Text = entrenador.getNombre() + " " + entrenador.getApellido1() + " " + entrenador.getApellido2();
                lbCorreo.Text = entrenador.getCorreo();
                lbCedula.Text = entrenador.getCedula();

                TodosEstudiantes.IsVisible = false;
                entEstudiantes.IsVisible = true;

                todosEquipos.IsVisible = false;
                entEquipos.IsVisible = true;

                todaAgenda.IsVisible = false;
                agenda.IsVisible = true;

                Entrenadores.IsVisible = false;
                Canchas.IsVisible = false;
            }

           
        }

        async private void verPerfil_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntEditar(), true);
        }


        async void Sett()
        {
            await Navigation.PushModalAsync(new EntSettings(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        async private void verEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntEstudiantes(), true);
        }

        async private void verTodosEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodosEstudiantes(), true);
        }

        async private void verEquipos_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntEquipos(), true);
        }

        async private void verTodosEquipos_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodosEquipos(), true);
        }

        async private void verEntrenadores_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new TodosEquipos(), true);
        }

        //METODOS JOSE 

        async private void verAgenda_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntAgenda(), true);
        }

        async private void verTodaAgenda_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodaAgenda(), true);
        }

        async private void verCanchas_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntCanchas(), true);
        }


  
    }
}