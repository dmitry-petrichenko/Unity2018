using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface IMoveStepByStepController
    {
        void MoveTo(List<IntVector2> path);
        void Cancel();
    }
}