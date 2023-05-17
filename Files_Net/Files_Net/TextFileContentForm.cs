using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Files_Net
{
    public partial class TextFileContentForm : Form
    {
        public string FileContent
        {
            get { return textBoxContent.Text; }
            set { textBoxContent.Text = value; }
        }


        public TextFileContentForm()
        {
            InitializeComponent();
        }

        
    }
}
