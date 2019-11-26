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
                lvi.Tag = person.PersonelID; // liste içerisindeki her bir satırın arka planında gizli olarak Id değerini tutuyoruz.
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
            MessageBox.Show(result ? "Kayıt Eklendi" : "Kayıt Eklenemedi");
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

        private void TsmSil_Click(object sender, EventArgs e)
        {


            //listview üzerinden seçtiğimiz her bir satırdaki gizli olarak yer alan Id değerini alıyoruz.

            if (lviPeople.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen silme işlemi için bir eleman seçiniz !");
                return;
            }
            DialogResult dr = MessageBox.Show("Kayıt kalıcı olarak silinecektir.\n İşleme devam etmek istiyor musunuz ?", "Kayıt silme bildirimi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {


                int id = (int)lviPeople.SelectedItems[0].Tag;

                //FirstOrDefault() => parametrede verdiğiniz koşula gçre size bulduğu ilk nesneyi teslim eder, eğer nesne bulamazsa default olarak null döner
                Person person = db.People.FirstOrDefault(x => x.PersonelID == id);
                if (person != null)
                {
                    db.People.Remove(person);
                    db.SaveChanges();
                    GetList();
                }

            }
        }
        #region KisiDuzenleme
        Person guncellenecek;
        private void TsmDuzenle_Click(object sender, EventArgs e)
        {
            if (lviPeople.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen güncelleme işlemi için bir eleman seçiniz!");
                return;
            }
            int id = (int)lviPeople.SelectedItems[0].Tag;
            guncellenecek = db.People.FirstOrDefault(x => x.PersonelID == id);

            if (guncellenecek != null)
            {
                txtName.Text = guncellenecek.FirstName;
                txtLastname.Text = guncellenecek.LastName;
                txtMail.Text = guncellenecek.Mail;
                txtPhone.Text = guncellenecek.PhoneNumber;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (guncellenecek == null)
            {
                MessageBox.Show("Bu kaydı güncelleyemezsiniz!");
                return;
            }
            guncellenecek.FirstName = txtName.Text;
            guncellenecek.LastName = txtLastname.Text;
            guncellenecek.Mail = txtMail.Text;
            guncellenecek.PhoneNumber = txtPhone.Text;

            db.SaveChanges(); // Güncelleme işlemi için bir metot yer almaktadır. savechange yapmamız yeterlidir.
            GetList();
        }
        #endregion

        #region AramaYapma
        void GetList(string param) // Kişi listesini getir.
        {
            lviPeople.Items.Clear();
            var people = db.People.Where(x => x.FirstName.Contains(param) || x.LastName.Contains(param) || x.PhoneNumber.Contains(param) || x.Mail.Contains(param)).ToList();
            foreach (Person person in people)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = person.FirstName;
                lvi.SubItems.Add(person.LastName);
                lvi.SubItems.Add(person.PhoneNumber);
                lvi.SubItems.Add(person.Mail);
                lvi.Tag = person.PersonelID; // liste içerisindeki her bir satırın arka planında gizli olarak Id değerini tutuyoruz.
                lviPeople.Items.Add(lvi);
            }
        }
        private void TxSearch_TextChanged(object sender, EventArgs e)
        {
            GetList(txSearch.Text);
        } 
        #endregion
    }
}
