using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace co.chimeralabs.ads.managed
{
    public interface IPlatform
    {
        void Initialize();
        String GetOS();
        String GetDevice();
        String GetUserId();
        String GetLocation();
    }
}
