using Infragistics.Documents.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportOrExportDataExcelWithInfragistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btn_ReadFromExcel_Click(object sender, EventArgs e)
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append("OrnekExcel");
            fileName.Append(".xls");
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName.ToString());
            var responseList = ReadFromExcel(filePath);
            foreach (var item in responseList)
            {
                listBox1.Items.Add(item.EmployeeNumber + "-" + item.FullName);
            }
        }
        public List<Person> ReadFromExcel(string filePath)
        {
            List<Person> tempPersonList = new List<Person>();
            try
            {
                Workbook itemWorkbook;
                Worksheet itemWorksheet;
                itemWorkbook = Workbook.Load(filePath);
                itemWorksheet = itemWorkbook.Worksheets[0];

                foreach (WorksheetRow row in itemWorksheet.Rows)
                {
                    if (row.Index == 0)
                        continue; //Header(kolon başlıkları var ise ilk satırı okumuyoruz.)

                    Person person = new Person();
                    person.EmployeeNumber = Convert.ToInt32(row.Cells[0].Value);
                    person.FullName = Convert.ToString(row.Cells[1].Value);
                    tempPersonList.Add(person);

                }

                return tempPersonList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void btn_CreateExcel_Click(object sender, EventArgs e)
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append("OrnekExcel");
            fileName.Append(".xls");
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName.ToString());
            List<Person> lst = new List<Person>();
            lst.Add(new Person { EmployeeNumber = 9145, FullName = "Fatih" });
            lst.Add(new Person { EmployeeNumber = 9144, FullName = "Emrah" });
            var response = CreateExcel(filePath, lst);
        }
        public static string CreateExcel(string filePath, List<Person> personList)
        {
            try
            {
                var workbook = new Workbook();
                var workSheet = workbook.Worksheets.Add("SheetName");//yeni bir sheet oluşturulur.

                #region Kolon adları oluşturulur varsa
                int rowNumber = 0;
                workSheet.Rows[rowNumber].Cells[0].Value = "Sicil Numarası";
                workSheet.Rows[rowNumber].Cells[1].Value = "Adı Soyadı";
                #endregion

                #region  Satırlar doldurulur.
                rowNumber = 1;
                foreach (var item in personList)
                {
                    workSheet.Rows[rowNumber].Cells[0].Value = Convert.ToString(item.EmployeeNumber);
                    workSheet.Rows[rowNumber].Cells[1].Value = item.FullName;
                    rowNumber++;
                }
                #endregion

                workbook.Save(filePath);// dosya yoluna oluştulan excel kaydedilir.
                return filePath;

            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
    public class Person
    {

        #region EmployeeNumber
        private int employeeNumber;
        public int EmployeeNumber
        {
            get { return employeeNumber; }
            set
            {
                if (employeeNumber != value)
                {
                    employeeNumber = value;

                }
            }
        }
        #endregion EmployeeNumber

        #region FullName
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;

                }
            }
        }
        #endregion FullName

    }
}
