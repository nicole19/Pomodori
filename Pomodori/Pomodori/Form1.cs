using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pomodori
{
    public partial class Form1 : Form
    {

        private int tarefas = 1, i = 0, descanso = 6, atividade = 25, z= 0;
        private int pomodoris = 0;
        private List<Control> listaText = new List<Control>();
        private List<Control> listaNum = new List<Control>();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Botao iniciar
        private void tarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while(true){
                EsconderComponentes();
                if (dataGridView1.Rows[z].Cells[3].Value.Equals("Não"))
                {
                    label19.Text = dataGridView1.Rows[z].Cells[1].Value.ToString();

                    pictureBox1.Visible = true;
                    lblStatus.Visible = true;
                    lblTempo.Visible = true;
                    label19.Visible = true;
                    lblTempo.BringToFront();
                    timer1.Start();
                    break;
                }
                z++;
            }
        }

        private void EsconderComponentes()
        {
            foreach (Control c in Controls)
            {
                c.Visible = false;
            }
            menuStrip1.Visible = true;
        }
        
        private void ExibirComponentes()
        {
            foreach (Control c in Controls)
            {
                c.Visible = true;
            }
            dataGridView1.Visible = false;
        }

        private void tarefasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EsconderComponentes();
            dataGridView1.Visible = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            CriarListas();
            for(int j = listaText.Count - 1; j >= 0 ; j-- )
            {
                if (listaText[j].Text != "")
                {
                    dataGridView1.Rows.Insert(i++, tarefas++, listaText[j].Text, listaNum[j].Text , "Não");
                }
            }
            limparCampos();
        }

        private void limparCampos()
        {
            foreach(TextBox t in listaText)
            {
                t.Text = "";
            }
            foreach (NumericUpDown t in listaNum)
            {
                t.Text = "0";
            }
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExibirComponentes();
            lblStatus.Visible = false;
            lblTempo.Visible = false;
            label19.Visible = false;
            pictureBox1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = "REALIZANDO TAREFA";
            atividade--;
            lblTempo.Text = Convert.ToString(atividade);
            if(atividade == 0)
            {
                timer2.Start();
                timer1.Stop();
                atividade = 26;
                descanso = 6;
                pomodoris++;
                playSound();
                if(pomodoris == 4)
                {
                    descanso = 20;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = "DESCANSO!!";
            descanso--;
            lblTempo.Text = Convert.ToString(descanso);
            if(descanso == 0)
            {   
                if(pomodoris < Convert.ToInt32(dataGridView1.Rows[z].Cells[2].Value.ToString()))
                {
                    timer1.Start();
                    timer2.Stop();
                    playSound();
                    descanso = 6;
                }
                else
                {
                    dataGridView1.Rows[z].Cells[3].Value = "Sim";
                    timer1.Stop();
                    timer2.Stop();
                    playSound();
                    pomodoris = 0;
                    MessageBox.Show("TAREFA REALIZADA!!");
                }
            }
        }

        private void CriarListas()
        {
            foreach(Control t in Controls)
            {
                if(t is TextBox)
                    listaText.Add(t);
                if(t is NumericUpDown)
                    listaNum.Add(t);
            }
        }

        private void playSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\img\Alarme_Despertador.wav");
            simpleSound.Play();
        }
    }
}
