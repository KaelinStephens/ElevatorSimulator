using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorFinalProject.Domain.Interfaces
{
    public interface IElevatorButtonPanel
    {
        public Dictionary<string, int> ButtonDictionary { get; }
        public Task PushButtonAsync(string buttonName);
    }
}
