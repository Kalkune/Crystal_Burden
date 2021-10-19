using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Burden
{
    class SoftDependencies : Crystal_Burden
    {
        public static void Init()
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.OkIgotIt.Her_Burden"))
                HerBurdenInstalled = true;
        }
    }
}
