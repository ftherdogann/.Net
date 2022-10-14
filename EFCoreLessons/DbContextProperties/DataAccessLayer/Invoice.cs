using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextProperties.DataAccessLayer
{
    public class Invoice
    {
        public int Invoice_Id { get; set; }
        public string InvoiceNumber { get; set; }

        public DateTime TranDate { get; set; }

        public string Description { get; set; }
    }
}
