﻿using HoopTeam.Implementacion;
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
        List<Equipos> equipos = new List<Equipos>();
        static string equipo { get; set; }

        Entrenador ent = new Entrenador();
        static string genero { get; set; }

        ClienteEntrenador clienteEnt = new ClienteEntrenador();

        

        public AgregarEst()   
        {
            InitializeComponent();
            
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new TodosEstudiantes(), true);  
        }

        async void Volver()
        {
            await Navigation.PushModalAsync(new EntEstudiantes(), true);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }

        private void btnAgregar(object sender, EventArgs e)
        {
            int ced = Int32.Parse(txtCedula.Text);
            string nom = txtNombre.Text;
            string ap1 = txtApellido1.Text;
            string ap2 = txtApellido2.Text;
            string correo = txtCorreo.Text;
            string contra = txtContraseña.Text;

            clienteEnt.AgregarEstudiante(ced, nom, ap1, ap2, genero[0].ToString(), correo, contra, Int32.Parse(equipo[0].ToString()));
            DisplayAlert("Informacion", "Estudiante agregado", "Ok");
            Volver();

        }
        private void OnPickerSelectedIndexChangedGenero(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            genero = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(genero);
            Debug.WriteLine(genero[0].ToString());

            equipos = clienteEnt.GetEquiposGen_Ent(genero[0].ToString(), Int32.Parse(ent.getCedula()));
            cbEquipo.Items.Clear();
            foreach (Equipos eq in equipos)
            {

              cbEquipo.Items.Add(eq.idEquipo.ToString() + " " + eq.categoria.ToString());
            }

        }

        private void OnPickerSelectedIndexChangedEquipo(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            equipo = selectedItem.ToString();
            Debug.WriteLine(selectedItem.ToString());
            Debug.WriteLine(equipo);

        }


    }
}