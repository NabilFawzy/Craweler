using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myIRproj
{
    class Token
    {
        public string mToken { get; set; }
        public int docID { get; set; }
        public int Freq { get; set; }
        public List<int> docPositions { get; set; }


    }
}
