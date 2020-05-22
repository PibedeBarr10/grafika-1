using System;
using System.Windows.Forms;

namespace PiotrWojtasiewicz_GK_Projekt1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            draw_Control1.StartSFML();
            rBtn1.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Rysowanie odcinka";
        }

        private void rBtn1_CheckedChanged(object sender, EventArgs e)
        {
            draw_Control1.algorithm = 1;
        }

        private void rBtn2_CheckedChanged(object sender, EventArgs e)
        {
            draw_Control1.algorithm = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (draw_Control1.ProcessIsBusy)
            {
                // pętla trwa dopóki proces rysowania trwa, 
                // wstrzymuje czyszczenie, dopóki nie skończy się proces rysowania
            }
            draw_Control1.shapes.Clear();
        }

        private void draw_Control1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = draw_Control1.location;
        }
    }
}
