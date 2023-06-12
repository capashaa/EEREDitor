using System;
using System.Drawing;
using System.Windows.Forms;

namespace EEditor
{
    public partial class random : Form
    {
        public random()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            for (int x = 0;x < MainForm.editArea.Frames[0].Width;x++)
            {
                for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
                {
                    if (MainForm.editArea.Frames[0].Foreground[y, x] == 385)
                    {
                        if (!string.IsNullOrWhiteSpace(textBox1.Text)) {
                            if (MainForm.editArea.Frames[0].BlockData3[y, x].ToLower().Contains(textBox1.Text))
                            {
                                richTextBox1.SelectionColor = Color.Green;
                                richTextBox1.AppendText(MainForm.editArea.Frames[0].BlockData3[y, x] + "\n\n");
                            }
                        }
                        else
                        {
                            richTextBox1.SelectionColor = Color.Purple;
                            richTextBox1.AppendText(MainForm.editArea.Frames[0].BlockData3[y, x] + "\n\n");
                        }
                    }
                }
            }
        }

        private void random_Load(object sender, EventArgs e)
        {

        }
    }
}
