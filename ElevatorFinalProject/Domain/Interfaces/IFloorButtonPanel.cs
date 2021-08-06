using System.Threading.Tasks;

namespace ElevatorFinalProject.Domain.Interfaces
{
    public interface IFloorButtonPanel
    {
        public string Display { get; set; }
        public int FloorId { get; set; }
        public Task UpButtonAsync();
        public Task DownButtonAsync();
        public string GetDisplay(int FloorId);
    }
}