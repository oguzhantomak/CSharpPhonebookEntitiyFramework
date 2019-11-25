using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitiyFrameworkApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();
        private void BtnKategori_Click(object sender, EventArgs e)
        {
            dgvKategoriler.DataSource = db.Categories.ToList();
        }

        private void BtnPersonel_Click(object sender, EventArgs e)
        {
            dgvPersoneller.DataSource = db.Employees.ToList();
        }
    }
}
