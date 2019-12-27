using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectAlexVlad.MenuLogic
{
    //This class will be used to pass data
    class PassAccount
    {
        public GameAccountManager AccountManager { get; set; }

        public Account GameUser { get; set; }

        public string ParameterSTR { get; set; }

        public int ParameterINT { get; set; }

        public string ParameterFLT { get; set; }

    }
}
