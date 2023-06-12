using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace EEditor
{
    public partial class Label : Form
    {
        public Point sz { get; set; }
        public string labelText { get; set; }
        public int labelWrap { get; set; }

        public string labelColor { get; set; }

        public bool loading = false;
        public bool acceptWrap = false;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();
        PrivateFontCollection bfont = new PrivateFontCollection();

        public Label()
        {
            InitializeComponent();
            this.BackColor = MainForm.themecolors.background;

            foreach (Control cntr in this.Controls)
            {
                if (cntr.GetType() == typeof(Button))
                {
                    ((Button)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((Button)cntr).BackColor = MainForm.themecolors.accent;
                    ((Button)cntr).FlatStyle = FlatStyle.Flat;
                }
                if (cntr.GetType() == typeof(TextBox))
                {
                    ((TextBox)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((TextBox)cntr).BackColor = MainForm.themecolors.accent;
                }
                if (cntr.GetType() == typeof(NumericUpDown))
                {
                    ((NumericUpDown)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((NumericUpDown)cntr).BackColor = MainForm.themecolors.accent;
                }
            }
        }

        private void Label_Load(object sender, EventArgs e)
        {
            loading = true;
            byte[] fontData = Properties.Resources.nokiafc22;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.nokiafc22.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.nokiafc22.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            bfont = fonts;
            if (labelWrap >= 4 && labelWrap <= 200)
            {
                acceptWrap = true;
                nupdWrap.Value = labelWrap;
                txtbText.Text = labelText;
                this.txtbText.Select(txtbText.Text.Length, txtbText.Text.Length - 1);
                UpdateText(labelText, labelWrap, labelColor);
            }
            else
            {
                if (MessageBox.Show("Your Wrap is too big, Do you want to change that?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    acceptWrap = true;
                    nupdWrap.Value = 200;
                    txtbText.Text = labelText;
                    this.txtbText.Select(txtbText.Text.Length, txtbText.Text.Length - 1);
                    UpdateText(labelText, labelWrap, labelColor);
                }
                else
                {
                    acceptWrap = false;
                    this.Close();
                }
            }
            loading = false;

        }

        private void Label_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (acceptWrap) this.DialogResult = DialogResult.OK;
        }
        private void UpdateText(string text, int wrap, string color = "#FFFFFF")
        {
            var fnt = new Font(bfont.Families[0], 12, FontStyle.Regular, GraphicsUnit.Pixel);
            var size = TextRenderer.MeasureText(text, fnt, new Size(wrap, 12));
            RectangleF rectf1 = new RectangleF(0, 0, wrap, size.Height * 7 - 7);
            Bitmap bmp = new Bitmap(wrap, size.Height * 7 - 7);

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                using (var solidBrush = new SolidBrush(ColorTranslator.FromHtml(color)))
                {
                    gr.Clear(GetContrastColor(ColorTranslator.FromHtml(color)));
                    gr.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    gr.DrawString(text, fnt, solidBrush, rectf1);

                    Image prev = pictureBox1.Image;
                    pictureBox1.Image = bmp;
                    pictureBox1.Width = wrap;
                    if (prev != null) prev.Dispose();
                    if (fnt != null) fnt.Dispose();
                }


            }

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                labelColor = ColorTranslator.ToHtml(cd.Color);
                UpdateText(labelText, labelWrap, labelColor);
            }
        }

        private Color GetContrastColor(Color color)
        {
            return (color.R * 0.299M) + (color.G * 0.587M) + (color.B * 0.114M) > 149 ?
                Color.Black :
                Color.White;
        }

        private void txtbText_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                if (txtbText.Text.Length > 0)
                {
                    labelText = txtbText.Text;
                    UpdateText(labelText, labelWrap, labelColor);
                }
            }
        }

        private void nupdWrap_ValueChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                if (nupdWrap.Value >= 4 && nupdWrap.Value <= 200)
                {
                    labelWrap = Convert.ToInt32(nupdWrap.Value);
                    UpdateText(labelText, labelWrap, labelColor);
                }
            }
        }
    }
}