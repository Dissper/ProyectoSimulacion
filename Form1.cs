using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ProyectoSimulacion
{
    
    //Dylan Valdez Garcia 
  

    public partial class Form1 : Form
    {
        private delegate void Delegate(System.Windows.Forms.PictureBox img);
        List<Cliente> lista = new List<Cliente>();
        List<Cliente> cola = new List<Cliente>();
        List<Cliente> ventanilla = new List<Cliente>();
        Conexion c = new Conexion();


        int contadorTurno = 0;
        int contadorAtendidos = 0;
        int contadorPerdidos = 0;
        int nombres;
        int siguiente = 1;
        public Form1()
        {
            nombres = 1;
            InitializeComponent();
        }

        //****************************************************BOTONES********************************************************//
        private void button1_Click(object sender, EventArgs e) //BOTON DE INICIAR
        {
            timer1.Start();
            button1.Enabled = false;
           
            
        }


        private void button2_Click(object sender, EventArgs e) //BOTON DE DETENER
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e) //BOTON DE INICIAR ATENCION A VENTANILLA
        {
            label1.Text = "No. Turno";
            label11.Text = "Ventanilla";

            timer2.Start();
            timer3.Start();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            

            Form2 f2 = new Form2(contadorAtendidos, contadorPerdidos, contadorTurno);

            f2.Show();


            c.insertar(contadorAtendidos, contadorTurno, contadorPerdidos);
            this.Hide();
        }


        //*************************************************TIMERS****************************************************************//

        private void timer1_Tick(object sender, EventArgs e)
        {
            var numero = new Random();
            if (numero.Next(0, 100) >= 50 && numero.Next(0, 100) <= 65)
            {

                //Inicializacion del cliente
                Cliente cliente = new Cliente();

                

                cliente.horaEntrada = DateTime.Now.ToShortTimeString();
                
                cliente.status = "espera";
                cliente.ubicacion = "espera";
                cliente.identificador = "Cliente" + nombres.ToString();


                //Animacion del cliente hacia la fila


                for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
                {
                    Control c = tableLayoutPanel1.GetControlFromPosition(0, x);
                    if (c == null)
                    {
                        cliente.lugarTabla = x;
                        break;
                    }

                }



                moverACola(pictureBox1);

                ColocarEnCola(cliente);

                if ( cola.Count < trackBar2.Value)
                {
                    //Se agrega al cliente a la lista

                    lista.Add(cliente);

                    contadorTurno++;

                    cliente.turno = contadorTurno;

                    cola.Add(cliente);

                }
                //El numero del cliente es equivalente al turno
                label2.Text = lista.Count.ToString();
            }
        }



        private void timer2_Tick(object sender, EventArgs e)
        {

            VentanillaAtentionLabel.Text = ventanilla.Count.ToString();

            var numero = new Random();
            if (numero.Next(0, 100) >= 50 && numero.Next(0, 100) <= 65)
            {

                PasarAVentanilla();

            }

        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            var numero = new Random();
            if (numero.Next(0, 100) >= 50 && numero.Next(0, 100) <= 60)
            {


                if (ventanilla.Count > 1)
                {
                    ventanilla[0].status = "atendido";
                    removerDeVentanilla();
                    
                }
                
                

            }
        }


        //********************************************TRACKBARS**********************************************************//

        private void clientesTrackBar_Scroll(object sender, EventArgs e)
        {
            //Velocidad de atencion a clientes es proporcional a la llegada  de ellos

            timer1.Interval = (11 - clientesTrackBar.Value) * 1000;

            timer2.Interval = (11 - clientesTrackBar.Value) * 1000;

            timer3.Interval = (11 - clientesTrackBar.Value) * 500;


            label4.Text = clientesTrackBar.Value.ToString();

        }


        private void trackBar1_Scroll(object sender, EventArgs e)  //TRACKBAR DE VENTANILLAS
        {
            label6.Text = trackBar1.Value.ToString();
            tableLayoutPanel2.RowCount = trackBar1.Value;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
        }




        private void trackBar2_Scroll(object sender, EventArgs e)  //TRACKBAR DE COLA
        {
            label5.Text = trackBar2.Value.ToString();
            tableLayoutPanel1.RowCount = trackBar2.Value;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

        }


      //*****************************************************FUNCIONES********************************************************//


        public void moverACola(System.Windows.Forms.PictureBox img)
        {
            

            if (img.InvokeRequired)
            {

                this.Invoke(new Delegate(moverACola), new Object[] { img });

            }
            else
            {
                img.Visible = true;

                for (int x = 12; x <= 200; x+=1)
                {
                    img.Location = new Point(x, 250);

                    //Task delay = Task.Delay(1);

                   // delay.Wait();
                }

                img.Visible = false;

                                    
            }


        }

        public void ColocarEnCola(Cliente cliente)
        {
            bool bandera = false;
            for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
            {
                Control c = tableLayoutPanel1.GetControlFromPosition(0, x);
                if (c == null)
                {
                    bandera = true;
                    Random r = new Random();//si se puede repetir
                    var picture = new PictureBox
                    {
                        Name = "pictureBox" + nombres.ToString(),
                        Size = new Size(50, 50),
                        Location = new Point(100, 100),
                       SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                        
                    };

                   switch (r.Next(1, 7))
                    {
                        case 1: picture.Image = global::ProyectoSimulacion.Properties.Resources.P1; break;
                        case 2: picture.Image = global::ProyectoSimulacion.Properties.Resources.P2; break;
                        case 3: picture.Image = global::ProyectoSimulacion.Properties.Resources.P3; break;
                        case 4: picture.Image = global::ProyectoSimulacion.Properties.Resources.P4; break; 
                        case 5: picture.Image = global::ProyectoSimulacion.Properties.Resources.P5; break;
                        case 6: picture.Image = global::ProyectoSimulacion.Properties.Resources.P6; break;
                    }
                    nombres++;
                    this.Controls.Add(picture);
                    tableLayoutPanel1.Controls.Add(picture, 0, x);
                    break;
                }

                
            }
            if (!bandera)
                contadorPerdidos++;
                label8.Text = (contadorPerdidos).ToString();
        }
      
       

        public void PasarAVentanilla()
        {
            
            for (int x = 0; x < tableLayoutPanel2.RowCount; x++)
            {
                Control c = tableLayoutPanel2.GetControlFromPosition(0, x);
                if (c == null)
                {

                    
                    foreach (Cliente a in cola)
                    {
                        if (ventanilla.Count < trackBar1.Value)
                        {
                            if (a.turno == siguiente)
                            {
                                tableLayoutPanel2.Controls.Add(tableLayoutPanel1.GetControlFromPosition(0, a.lugarTabla), 0, x);
                                tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(0, a.lugarTabla));
                                lista.Remove(a);
                                siguiente++;
                                label1.Text = "No. turno " + siguiente.ToString();
                                label11.Text = "Ventanilla " + (x + 1).ToString();

                            }
                        }


                       
                        
                    }

                    colocarEnVentanilla();
                }

                
              
            }
        }

        //METODO PARA COLOCAR A LOS CLIENTES EN LA LISTA DE VENTANILLA

        public void colocarEnVentanilla()
        {

            while (ventanilla.Count < trackBar1.Value)
            {

                for (int i = 0; i < cola.Count; i++)
                {

               
                    ventanilla.Add(cola[0]);
                    cola.RemoveAt(0);


                    

                }
                break;
            }

           
        }


        public void removerDeVentanilla()
        {

            for (int i = 0; i < ventanilla.Count(); i++)
            {

                if (ventanilla[0].status == "atendido")
                {

                    tableLayoutPanel2.Controls.Remove(tableLayoutPanel2.GetControlFromPosition(0, ventanilla[0].lugarTabla));

                    ventanilla.RemoveAt(0);

                    contadorAtendidos++; 

                    
                }
                

            }
           

        }


       
    }
}
