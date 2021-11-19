using System.Collections.Generic;
using Data.Entities;

namespace Data.Commands
{
    public class SplitTransactionCommand
    {
        public List<SingleCategorySplit> Splits { get; set; }
    }
}