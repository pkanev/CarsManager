using System;
using System.Collections.Generic;
using Client.Core.Data;

namespace Client.Core.Models
{
    public class ExportModel<T>
    {
        public IList<string> Properties { get; set; }
        public IList<T> Items { get; set; }
        public ExportType ExportType { get; set; }
    }
}
