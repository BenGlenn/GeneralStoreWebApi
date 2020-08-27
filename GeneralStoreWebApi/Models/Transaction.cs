using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralStoreWebApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer{ get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ItemCount { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}