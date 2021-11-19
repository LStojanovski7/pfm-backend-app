using System.Collections.Generic;
using Data.Entities;

namespace API.Commands
{
    public class SplitTransactionCommand
    {
        public List<SingleCategorySplit> Splits { get; set; }
    }
}