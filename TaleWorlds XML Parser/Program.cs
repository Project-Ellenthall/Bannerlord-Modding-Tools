using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Security;

namespace TaleWorldsXMLParser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OpenFolderDialogForm());
        }
    }

    public class BannerlordXMLParsers
    {
        private Characters.CharacterParser characterParser;

        public BannerlordXMLParsers()
        {

        }

        public void startCharacterParser(string filepath) // Given the modified Bannerlord path, open the relevant file
        {
            characterParser = new Characters.CharacterParser(filepath, Characters.CharacterParser.CHARACTER_XML);
        }
    }

    public class OpenFolderDialogForm : Form
    {
        private Button selectButton;
        private FolderBrowserDialog openFolderDialog1;
        private TextBox textBox1;
        private BannerlordXMLParsers xmlParse; // Relevant for parsing all XMLs for bannerlord


        public OpenFolderDialogForm()
        {
            xmlParse = new BannerlordXMLParsers();
            openFolderDialog1 = new FolderBrowserDialog();
            selectButton = new Button
            {
                Size = new Size(100, 100),
                Location = new Point(15, 15),
                Text = "Select Bannerlord Game Directory"
            };
            selectButton.Click += new EventHandler(SelectButton_Click);
            textBox1 = new TextBox
            {
                Size = new Size(300, 300),
                Location = new Point(15, 115),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            ClientSize = new Size(330, 360);
            Controls.Add(selectButton);
            Controls.Add(textBox1);
        }
        private void SetText(string text)
        {
            textBox1.Text = text;
        }
        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (openFolderDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = openFolderDialog1.SelectedPath;
                    SetText(sr);
                    this.xmlParse.startCharacterParser(sr);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}

