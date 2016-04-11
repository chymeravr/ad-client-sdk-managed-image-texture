using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace co.chimeralabs.ads.managed
{
    public static class PlatformObjectsFactoryHolder
    {
        private static IPlatformObjectsFactory platformObjectsFactory;
        public static void SetFactory(IPlatformObjectsFactory factory)
        {
            platformObjectsFactory = factory;
        }
        public static IPlatformObjectsFactory GetFactory()
        {
            return platformObjectsFactory;
        }
    }
}