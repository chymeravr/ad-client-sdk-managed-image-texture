using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using co.chimeralabs.ads.managed;
using co.chimeralabs.ads.managed.Models;

namespace AdClientSDKManagedTestConsole
{
    class PlatformVariableTest
    {
        public static void Test1()
        {
            AppParams appParams = AdConfigurer.GetAppParams();
            Console.WriteLine("UserID:"+appParams.userId);
            Console.WriteLine("StartTime:" + appParams.startTime);
        }
    }
}
