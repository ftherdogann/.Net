using iTextSharp.text;
using iTextSharp.text.pdf;
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

namespace PDFSharpMergePdf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {

            string pathfile1 = @"D:\Akit.pdf";
            byte[] file1Content = File.ReadAllBytes(pathfile1);
            string pathfile2 = @"D:\AkitEkImza.pdf";
            byte[] file2Content = File.ReadAllBytes(pathfile2);

            string outputfilepath = @"D:\deneme.pdf";

            List<byte[]> files = new List<byte[]>();
            files.Add(file1Content);
            files.Add(file2Content);
            CombinePDFsByByteContents(files, outputfilepath);

            string output2filepath = @"D:\deneme2.pdf";

            List<string> filesPathList = new List<string>();
            filesPathList.Add(pathfile1);
            filesPathList.Add(pathfile2);
            CombinePDFsByFilePath(filesPathList, output2filepath);

        }
        public void CombinePDFsByFilePath(List<string> srcPDFs, string outputFile)
        {

            using (FileStream stream = new FileStream(outputFile, FileMode.Create))
            using (Document doc = new Document())
            using (PdfCopy pdf = new PdfCopy(doc, stream))
            {
                doc.Open();

                PdfReader reader = null;
                PdfImportedPage page = null;
                srcPDFs.ForEach(file =>
                {
                    reader = new PdfReader(file);
                    for (int i = 0; i < reader.NumberOfPages; i++)
                    {
                        page = pdf.GetImportedPage(reader, i + 1);
                        pdf.AddPage(page);
                    }

                    pdf.FreeReader(reader);
                    reader.Close();
                });
            }
        }
        public void CombinePDFsByByteContents(List<byte[]> srcPDFs, string outputFile)
        {

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var copy = new PdfSmartCopy(doc, ms))
                    {
                        doc.Open();
                        foreach (var p in srcPDFs)
                        {
                            using (var reader = new PdfReader(p))
                            {
                                copy.AddDocument(reader);
                            }
                        }
                        doc.Close();
                    }
                }
                var pdfByteArray = ms.ToArray();
                File.WriteAllBytes(outputFile, pdfByteArray);


            }
        }
    }
}
