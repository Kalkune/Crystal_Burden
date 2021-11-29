using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Burden
{
    class ConfigChanges : Crystal_Burden
    {
        public static void Init()
        {
            if (Hbversion.Value == "1.5.0")
            {
                Hbpul.Value = true;
                Hbversion.Value = "1.5.1";
            }
        }
    }
}
