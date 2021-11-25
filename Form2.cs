using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProyectoSimulacion
{
    public partial class Form2 : Form
    {
        int atendidos;
        int perdidos;
        int turnos;

       

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(int atendidos, int perdidos, int turnos)
        {

            InitializeComponent();

            this.atendidos = atendidos;

            this.perdidos = perdidos;

            this.turnos = turnos;


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] series = { "Atendidos", "Perdidos", "Turnos"};
            int[] datos = {atendidos, perdidos, turnos };

            chart1.Palette = ChartColorPalette.Pastel;

            chart1.Titles.Add("Clientes Banco");

            for (int i = 0; i < series.Length; i++)
            {
                //titulos
                Series serie = chart1.Series.Add(series[i]);

                //cantidades
                serie.Label = datos[i].ToString();

                serie.Points.Add(datos[i]);





            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
