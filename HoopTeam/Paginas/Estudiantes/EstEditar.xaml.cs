﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoopTeam.Implementacion;
using HoopTeam.Modelo;
using HoopTeam.Paginas.Estudiantes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Estudiantes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstEditar : ContentPage
    {

        ClienteEstudiante cEnt = new ClienteEstudiante();
        EstudianteEstatico estudiante = new EstudianteEstatico();
        public EstEditar()
        {
            InitializeComponent();

            txtNombre.Text = estudiante.getNombre();
            txtApellido1.Text = estudiante.getApellido1();
            txtApellido2.Text = estudiante.getApellido2();
            txtCorreo.Text = estudiante.getCorreo();
            txtContraseña.Text = estudiante.getContrasenna();
            txtFecha.Text = estudiante.getNacimiento();
            txtGenero.Text = estudiante.getGenero();


        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EstMain(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        private void ver_Cambios(object sender, EventArgs e)
        {
            //al hacer click hace el cambio de datos
            ClienteEstudiante objClienteE = new ClienteEstudiante();

            string nom = txtNombre.Text;
            string ap1 = txtApellido1.Text;
            string ap2 = txtApellido2.Text;
            string correo = txtCorreo.Text;
            string con = txtContraseña.Text;
            string fecha = txtFecha.Text;
            string gen = txtGenero.Text;
            string ced = estudiante.getCedula();

            try
            {
                objClienteE.actualizarEstudiante(nom, ap1, ap2, correo, con, gen, ced);
                DisplayAlert("Información: ", "Datos actualizados", "OK");
                Sett();
            }
            catch (Exception ex)
            {
                DisplayAlert("Información Actualizada", "Perfil", "OK");
            }

        }
    }
}