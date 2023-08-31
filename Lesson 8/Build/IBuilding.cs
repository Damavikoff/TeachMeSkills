using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build
{
    public interface IBuilding
    {
        void BuildFoundament();
        void BuildWalls();
        void BuildRoof();
        void BuildFloor();
    }
}
