using HoopTeam.Implementacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo
{
    class EstudiantePerfilViewModel
    
        {
        EstudianteEstatico estP = new EstudianteEstatico();

        public List<Estudiante> EstudiantesPerfil { get; set; }

        public EstudiantePerfilViewModel()
        {
            EstudiantesPerfil = new EstudianteService().GetEstudiantes();
        }
    
}
}
