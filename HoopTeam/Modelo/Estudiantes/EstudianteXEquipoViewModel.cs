using HoopTeam.Implementacion;
using HoopTeam.Modelo.Entrenadores;
using System;
using System.Collections.Generic;
using System.Text;



namespace HoopTeam.Modelo.Estudiantes
{
    //clase que permite ver el o los equipos del estudiante
    class EstudianteXEquipoViewModel
    {
        //referencia a la sesion del estudiante
        EstudianteEstatico est = new EstudianteEstatico();
        //lista de tipo estudiante definido en el entrenador
        public List<EstEntrenador> EstudiantesPorEquipo { get; set; }

        //llena la lista con la informacion de los equipos del estudiante segun la cedula
        public EstudianteXEquipoViewModel()
        {
            EstudiantesPorEquipo = new EstudianteService().GetEstEquipo(est.getCedula());
        }
    }
}
