using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_del_analizador_léxico
{

    public partial class Form1 : Form
    {
        static private List<Rutas> rutas;

        public String Path_actual;
        public String nombre_acual;

        static private List<Token> lis_toks;
        public Form1()
        {
            InitializeComponent();
            rutas = new List<Rutas>();
        }

        private void b_correr_Click(object sender, EventArgs e)
        {
            String texto = richTextBox1.Text;
            Analizador analiz = new Analizador();
            analiz.Analizador_cadena(texto);

            analiz.generarLista();
            comen.Text = analiz.getRetorno();


            lis_toks = new List<Token>();
            lis_toks = analiz.getListaTokens();

            for (int i = 0; i < lis_toks.Count; i++)
            {
                Token actual = lis_toks.ElementAt(i);
                String mensajeRespuesta = null;
                if (actual.getLexema() != null)
                {
                    mensajeRespuesta = "[Lexema: " + actual.getLexema();
                }
                if (actual.getIdToken() != null)
                {
                    mensajeRespuesta += ", Token: " + actual.getIdToken();
                }
                MessageBox.Show(mensajeRespuesta +
                ", Linea: " + actual.getLinea() +
                "]", "Listado Tokens");
            }


        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            guardarComo();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean existe = false;
            string path = "";
            for (int i = 0; i < rutas.Count; i++)
            {
                Rutas ru = rutas.ElementAt(i);
                if (Path_actual == ru.getPath())
                {
                    path = Path_actual;
                    existe = true;
                }
            }
            if (existe == false)
            {
                guardarComo();
            }
            else
            {
                guardar(path);
            }
        }

        private void guardar(string path)
        {
            try
            {

                string text = richTextBox1.Text;
                StreamWriter writer = new StreamWriter(path);
                writer.Write(text);
                writer.Flush();
                writer.Close();

                string nombre = Path.GetFileNameWithoutExtension(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void guardarComo()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "[LFP]|*.txt";
            saveFile.Title = "Guardar archivo";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)saveFile.OpenFile();
                fs.Close();
                string path = saveFile.FileName;
                guardar(path);
                string nombre = Path.GetFileNameWithoutExtension(path);
                Rutas path_r = new Rutas(path, nombre);
                rutas.Add(path_r);
                Path_actual = path;
                nombre_acual = nombre;
                this.Text = nombre_acual;

            }

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "[LFP]|*.txt";
            string texto = "";
            string fila = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string ruta1 = openFile.FileName;
                StreamReader streamReader = new StreamReader(ruta1, System.Text.Encoding.UTF8);
                string nombreC = Path.GetFileNameWithoutExtension(openFile.FileName);
                while ((fila = streamReader.ReadLine()) != null)
                {
                    texto += fila + System.Environment.NewLine;
                }
                richTextBox1.Text = texto;
                streamReader.Close();

                rutas.Clear();
                Rutas path = new Rutas(ruta1, nombreC);
                rutas.Add(path);
                Path_actual = ruta1;
                nombre_acual = nombreC;
                this.Text = nombre_acual;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            b_correr.FlatAppearance.BorderColor = Color.Empty;
            this.BackColor = Color.FromArgb(55, 137, 51);
            menuStrip1.BackColor = Color.FromArgb(55, 137, 65);
            menuStrip1.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(55, 137, 40);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String[] sintaxis;
            String recopilar = richTextBox1.Text;
            sintaxis = recopilar.Split(" ");
            if (richTextBox1.Text != "")
            {
                if (richTextBox1.SelectionColor == Color.Black)
                {
                    foreach (var palabrasReservadas in sintaxis)
                    {
                        switch (palabrasReservadas)
                        {
                            case "nose2":
                            case "nose3":
                                HighlightPhrase(richTextBox1, palabrasReservadas, Color.FromArgb(0, 232, 198));
                                button2.Text = "Quitar Color";
                                break;
                        }

                    }
                }
                else
                {
                    foreach (var palabrasReservadas in sintaxis)
                    {
                        switch (palabrasReservadas)
                        {
                            case "nose2":
                            case "nose3":
                                HighlightPhrase(richTextBox1, palabrasReservadas, Color.FromArgb(0, 0, 0));
                                button2.Text = "Colorear Sintaxis";
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese sintaxis", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        static void HighlightPhrase(RichTextBox box, string phrase, Color color)
        {
            int pos = box.SelectionStart;
            string s = box.Text;
            for (int ix = 0; ;)
            {
                int jx = s.IndexOf(phrase, ix, StringComparison.CurrentCultureIgnoreCase);
                if (jx < 0) break;
                box.SelectionStart = jx;
                box.SelectionLength = phrase.Length;
                box.SelectionColor = color;
                ix = jx + 1;
            }
            box.SelectionStart = pos;
            box.SelectionLength = 0;
        }

        private void analizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.b_correr_Click(sender, e);
        }

    }
}
