using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMauiApp.Models
{
    public class Filter
    {
        public string Group { get; set; }
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
        public bool IsSelected { get; set; }
    }
}
