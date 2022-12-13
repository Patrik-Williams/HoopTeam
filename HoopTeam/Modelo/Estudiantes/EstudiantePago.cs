using System;
using System.Collections.Generic;
using System.Text;

namespace HoopTeam.Modelo.Estudiantes
{
    class EstudiantePago
    {
        //campos del objeto pago, permite ver el estado de pago del estudiantes
        private static string IdPago;
        private static string Cedula;
        private static string FechaPago;
        private static int PagoRealizado;
        private static string Monto;

        //getters and setters
        public String getIdPago()
        {
            return IdPago;
        }

        public void setIdPago(String idPago)
        {
            IdPago = idPago;
        }

        public String getCedula()
        {
            return Cedula;
        }

        public void setCedula(String ced)
        {
            Cedula = ced;
        }

        public String getFechaPago()
        {
            return FechaPago;
        }

        public void setFechaPago(String fechaPago)
        {
            FechaPago = fechaPago;
        }

        public int getPagoRealizado()
        {
            return PagoRealizado;
        }

        public void setPagoRealizado(int pagoRealizado)
        {
            PagoRealizado = pagoRealizado;
        }

        public String getMonto()
        {
            return Monto;
        }

        public void setMonto(String monto)
        {
            Monto = monto;
        }

    }
}