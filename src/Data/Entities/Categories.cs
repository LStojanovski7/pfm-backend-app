using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Category
    {
         public string Code { get; set; }
         public string Name { get; set; }
         public string ParrentCode { get; set; }
         public virtual ICollection<Transaction> Transactions { get; set; }
    }
}