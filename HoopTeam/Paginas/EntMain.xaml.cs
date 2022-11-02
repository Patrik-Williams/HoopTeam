﻿using HoopTeam.Implementacion;
using HoopTeam.Modelo;
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
        public EntMain()
        {
            InitializeComponent();

            lbInfo.Text = entrenador.getNombre() + " " + entrenador.getApellido1() + " " + entrenador.getApellido2();
            lbCorreo.Text = entrenador.getCorreo();
            lbCedula.Text = entrenador.getCedula();
        }

        private void verPerfil_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Informacion", "PERFIL", "Ok");
        }


        async void Sett()
        {
            await Navigation.PushModalAsync(new EstSettings(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
    }
}