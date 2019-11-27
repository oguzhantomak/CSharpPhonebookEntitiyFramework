using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitiyFrameworkSorgular
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();
        private void BtnOrnek1_Click(object sender, EventArgs e)
        {   /* 
            select ProductID,ProductName,UnitPrice,UnitsInStock,CategoryID 
            from Products
            where UnitPrice between 20 and 50
            order by UnitPrice asc
            */


            // -------- Linq to Entity -------- \\

            /*
             * 
            dgv1.DataSource = db.Products
                .Where(x => x.UnitPrice >= 20 && x.UnitPrice <= 50)
                .Select(x=> new {
                    UrunNo = x.ProductID,
                    UrunAdi = x.ProductName,
                    Stok = x.UnitsInStock,
                    Fiyat = x.UnitPrice
                })// new oluşturmamızın nedeni elimizde var olmayan tipler olduğu için ve yeniden class yazmaktansa anonim tip olarak kullanıyoruz.
                .OrderBy(x=> x.Fiyat) // Fiyat bilgisine göre asc şekilde sıralar.
                .ToList(); // => Select * From Products demektir.

            */

            // -------- Linq to SQL -------- \\

            var result = from p in db.Products
                         where p.UnitPrice >= 20 && p.UnitPrice <= 50
                         orderby p.UnitPrice ascending
                         select new
                         {
                             UrunNo = p.ProductID,
                             UrunAdi = p.ProductName,
                             Stok = p.UnitsInStock,
                             Fiyat = p.UnitPrice

                         };
            dgv1.DataSource = result.ToList();

        }

        private void BtnOrnek2_Click(object sender, EventArgs e)
        {
            //            SELECT dbo.Customers.CompanyName, dbo.Employees.LastName, dbo.Employees.FirstName, dbo.Orders.OrderID, dbo.Shippers.CompanyName               AS Expr1, dbo.Orders.OrderDate
            //              FROM            dbo.Customers INNER JOIN
            //                         dbo.Orders ON dbo.Customers.CustomerID = dbo.Orders.CustomerID INNER JOIN
            //                         dbo.Employees ON dbo.Orders.EmployeeID = dbo.Employees.EmployeeID INNER JOIN
            //                         dbo.Shippers ON dbo.Orders.ShipVia = dbo.Shippers.ShipperID


            // -------- Linq to Entity -------- \\

            /*  
             dgv1.DataSource = db.Orders
                .Select(x => new
                {
                    Musteri = x.Customer.CompanyName,
                    Personel = x.Employee.FirstName + " " + x.Employee.LastName,
                    SiparisNo = x.OrderID,
                    SiparisTarihi = x.OrderDate,
                    KargoAdi = x.Shipper.CompanyName
                })
                .ToList();
                */

            // -------- Linq to SQL -------- \\

            var result = from o in db.Orders
                         select new
                         {
                             Musteri = o.Customer.CompanyName,
                             Personel = o.Employee.FirstName + " " + o.Employee.LastName,
                             SiparisNo = o.OrderID,
                             SiparisTarihi = o.OrderDate,
                             KargoAdi = o.Shipper.CompanyName

                         };
            dgv1.DataSource = result.ToList();
        }

        private void BtnOrnek3_Click(object sender, EventArgs e)
        {
            // select * from Customers where CompanyName like '%restaurant%'


            // -------- Linq to Entity -------- \\

            /*
             
            dgv1.DataSource = db.Customers.Where(x =>
              x.CompanyName.Contains("restaurant"))
            .ToList();

            */

            // -------- Linq to SQL -------- \\

            var result = from c in db.Customers
                         where c.CompanyName.Contains("restaurant")
                         select c;

            dgv1.DataSource = result.ToList();


        }

        private void BtnOrnek4_Click(object sender, EventArgs e)
        {
            //Kategorisi Beverages olan ve UrunAdi:Kola, FiyatI: 5.00, StokAdet: 500 olan ürün ekleyiniz.


            // -------- Yöntem 1 -------- \\


            //Category category = db.Categories.FirstOrDefault(x => x.CategoryName == "Beverages");
            //Product product = new Product();
            //product.ProductName = "Kola 1";
            //product.UnitPrice = 5.00m;
            //product.UnitsInStock = 500;
            //product.CategoryID = category.CategoryID;

            //db.Products.Add(product);
            //db.SaveChanges();

            //dgv1.DataSource = db.Products
            //    .Where(x => x.ProductName.StartsWith("Kola"))
            //    .Select(x => new
            //    {
            //        x.ProductName,
            //        x.UnitPrice,
            //        x.UnitsInStock,
            //        x.Category.CategoryName
            //    })
            //    .ToList();



            // -------- Yöntem 2 -------- \\

            db.Categories.FirstOrDefault(x => x.CategoryName == "Beverages").Products.Add(new Product
            {
                ProductName = "Kola 2",
                UnitPrice = 5.00m,
                UnitsInStock = 500,
            });
            db.SaveChanges();
        }

        private void BtnOrnek5_Click(object sender, EventArgs e)
        {
            //Çalışanların adını, soyadını, doğum tarihini ve yaşını getiren sorgu
            // select FirstName,LastName,BirthDate,YEAR(GETDATE()) - YEAR(BirthDate) as AGE from Employees veya
            // select FirstName,LastName,BirthDate,DATEDIFF(YEAR,BirthDate,GETDATE()) as AGE from Employees

            // -------- Linq to Entity -------- \\

            //              dgv1.DataSource = db.Employees
            //                  .Select(x => new
            //                  {
            //                      Personel = x.FirstName + " " + x.LastName,
            //                      DogumTarihi= x.BirthDate,
            //                      Yas= DateTime.Now.Year - x.BirthDate.Value.Year,
            //                  })
            //                  .ToList();

            // -------- Linq to SQL -------- \\

            var result = from x in db.Employees
                         select new
                         {
                             Personel = x.FirstName + " " + x.LastName,
                             DogumTarihi = x.BirthDate,
                             Yas = DateTime.Now.Year - x.BirthDate.Value.Year,
                         };
            dgv1.DataSource = result.ToList();




        }

        private void BtnOrnek6_Click(object sender, EventArgs e)
        {
            // Kategorilerine göre stoktaki ürün sayısını veren sorgu
            // select CategoryName, SUM(p.UnitsInStock) as toplam from Products as p join Categories as c on p.CategoryID = c.CategoryID group by CategoryName 


            // -------- Linq to Entity -------- \\

            //              dgv1.DataSource = db.Products
            //                  .GroupBy(x=>x.Category.CategoryName)
            //                  .Select(x => new
            //                  {
            //                      KategoriAdi = x.Key,
            //                      ToplamUrun = x.Sum(s=>s.UnitsInStock)
            //                  })
            //                  .ToList();


            // -------- Linq to SQL -------- \\


            //              var result = from p in db.Products
            //                           group p by p.Category.CategoryName into g
            //                           select new
            //                           {
            //                               KategoriAdi = g.Key,
            //                               Toplam = g.Sum(p => p.UnitsInStock)
            //                           };
            //              dgv1.DataSource = result.ToList();

            // Başka örnek
            // select c.CategoryID,c.CategoryName,c.Description from Products as p join Categories as c on p.CategoryID=c.CategoryID group by c.CategoryID, c.CategoryName, c.Description


            // -------- Linq to Entity -------- \\

            //          dgv1.DataSource = db.Products
            //              .GroupBy(x => new
            //              {
            //                  x.CategoryID,
            //                  x.Category.CategoryName,
            //                  x.Category.Description
            //              })
            //              .Select(x => new
            //              {
            //                  x.Key.CategoryID,
            //                  x.Key.CategoryName,
            //                  x.Key.Description,
            //                  Toplam = x.Sum(s => s.UnitsInStock)
            //              })
            //              .ToList();


            var result = from p in db.Products
                         group p by new {
                         p.CategoryID,
                         p.Category.CategoryName,
                         p.Category.Description
                         } into g
                         select new
                         {
                             KategoriId = g.Key.CategoryID,
                             KategoriAdi = g.Key.CategoryName,
                             Toplam = g.Sum(p => p.UnitsInStock),
                             Aciklama = g.Key.Description,
                         };
            dgv1.DataSource = result.ToList();


        }
    }
}
