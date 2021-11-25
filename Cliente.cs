using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSimulacion
{
    public class Cliente
    {
        int id { get; set; }
        public int turno { get; set; }
        public string horaEntrada { get; set; }
        public string horaSalida { get; set; }
        public string status { get; set; }
        public string ubicacion { get; set; }
        public string identificador { get; set; }
        public int lugarTabla { get; set; }
        int ventanillaAsignada { get; set; }
        bool atendido { get; set; }



      
    }
}
