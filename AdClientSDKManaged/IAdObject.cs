using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace co.chimeralabs.ads.managed
{
    public interface IAdObject
    {
        void Update();
        float AdObjectX();
        float AdObjectY();
        float AdObjectZ();
        float CameraX();
        float CameraY();
        float CameraZ();
        float CameraLA_X();
        float CameraLA_Y();
        float CameraLA_Z();
    }
}
