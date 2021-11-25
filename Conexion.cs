using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSimulacion
{
    class Conexion
    {

        SqlConnection cn;
        SqlCommand cmd;
       

        public Conexion(){

            try
            {
                cn = new SqlConnection("Data Source=.;Initial Catalog=simulacion;Integrated Security=True");
                cn.Open();
                MessageBox.Show("Conexion Exitosa");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Conexion Erronea:"+ex.ToString());
            }


        }

        public string insertar(int atendidos, int turnos, int perdidos)
        {

            string salida = "Datos Insertados Correctamente";

            try
            {
                cmd = new SqlCommand("INSERT INTO banco(ATENDIDOS, TURNOS, PERDIDOS) values("+atendidos+", "+turnos+", "+perdidos+")", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                salida = "Conexion fallida: " +ex.ToString();

            }

            return salida;
        }

    }

    
}
