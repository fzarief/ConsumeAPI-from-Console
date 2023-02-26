using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeAPI
{
    public class KopiMenu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        //public string Kode { get; set; }

        private string _kode;
        public string Kode
        {
            get
            {
                return _kode;
            }
            set
            {
                _kode = Id.ToString().PadLeft(4, '0');
            }
        }
    }
}
