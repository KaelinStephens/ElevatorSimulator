using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public interface IElevator
    {
        Task MoveUpAsync();
        Task MoveDownAsync();
    }
}
