using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void GetList() // Kişi listesini getir.
        {
            lviPeople.Items.Clear();
            var people = db.People.ToList();
            foreach (Person person in people)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = person.FirstName;
                lvi.SubItems.Add(person.LastName);
                lvi.SubItems.Add(person.PhoneNumber);
                lvi.SubItems.Add(person.Mail);

                lviPeople.Items.Add(lvi);
            }
        }

        PhonebookEntities1 db = new PhonebookEntities1();
        private void Form1_Load(object sender, EventArgs e)
        {
            GetList();
        }

        void Clean()
        {
            foreach (Control item in grpKisiEkleme.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Person person = new Person();
            person.FirstName = txtName.Text;
            person.LastName = txtLastname.Text;
            person.PhoneNumber = txtPhone.Text;
            person.Mail = txtPhone.Text;

            db.People.Add(person);
            //Yapılan işlemin sonunda db.SaveChanges ile paketlenmiş bilgileri/komutları execute ediyoruz.
            bool result = db.SaveChanges() > 0;
            MessageBox.Show(result?"Kayıt Eklendi":"Kayıt Eklenemedi");
            GetList();
            Clean();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            txtName.Text = FakeData.NameData.GetFirstName();
            txtLastname.Text = FakeData.NameData.GetSurname();
            txtPhone.Text = FakeData.PhoneNumberData.GetPhoneNumber();
            txtMail.Text = FakeData.NetworkData.GetEmail().ToLower();
        }
    }
}
