using System;
using System.Collections.Generic;
using System.Timers;
using ElevatorFinalProject.Domain.Interfaces;

namespace ElevatorFinalProject.Infastructure
{
    public  class AI : IAI
    {
        private readonly IFloorButtonPanel _floorButtonPanel;
        private readonly IElevatorButtonPanel _elevatorButtonPanel;
        public Timer _timer = new Timer();
        private  Dictionary<int, System.Action> AIDictionary = new Dictionary<int, System.Action>();
        private  readonly Random _randomInterval = new Random();
        public void Actions()
        {
            AIDictionary[0] = LobbyUpCall;
            AIDictionary[1] = Floor1UpCall;
            AIDictionary[2] = Floor2UpCall;
            AIDictionary[3] = Floor3UpCall;
            AIDictionary[4] = Floor4UpCall;
            AIDictionary[5] = Floor5UpCall;
            AIDictionary[6] = Floor6UpCall;
            AIDictionary[7] = Floor7UpCall;
            AIDictionary[8] = Floor1DownCall;
            AIDictionary[9] = Floor2DownCall;
            AIDictionary[10] = Floor3DownCall;
            AIDictionary[11] = Floor4DownCall;
            AIDictionary[12] = Floor5DownCall;
            AIDictionary[13] = Floor6DownCall;
            AIDictionary[14] = Floor7DownCall;
            AIDictionary[15] = Floor8DownCall;
            AIDictionary[16] = LobbyInElevator;
            AIDictionary[17] = Floor1InElevator;
            AIDictionary[18] = Floor2InElevator;
            AIDictionary[19] = Floor3InElevator;
            AIDictionary[20] = Floor4InElevator;
            AIDictionary[21] = Floor5InElevator;
            AIDictionary[22] = Floor6InElevator;
            AIDictionary[23] = Floor7InElevator;
            AIDictionary[24] = Floor8InElevator;

        }

        public AI(IFloorButtonPanel floorButtonPanel, IElevatorButtonPanel elevatorButton)
        {
            _floorButtonPanel = floorButtonPanel;
            _elevatorButtonPanel = elevatorButton;
            SetupTimer();
            Actions();
        }

        private void SetupTimer()      // sets up timer
        {
            _timer.Interval = _randomInterval.Next(5500, 7000);
            _timer.Elapsed += ThrowCallCommandAsync;
            _timer.AutoReset = false;
        }

        public void ThrowCallCommandAsync(object sender, ElapsedEventArgs e)
        {
            Random randomCall = new Random(DateTime.Now.Millisecond);
            var r = randomCall.Next(AIDictionary.Count);
            AIDictionary[r]();
            _timer.Start();
        }

        public void LobbyUpCall()
        {
            _floorButtonPanel.FloorId = 0;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(0);
            _floorButtonPanel.UpButtonAsync();
        }

        public void Floor1UpCall()
        {
            _floorButtonPanel.FloorId = 1;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(1);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor2UpCall()
        {
            _floorButtonPanel.FloorId = 2;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(2);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor3UpCall()
        {
            _floorButtonPanel.FloorId = 3;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(3);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor4UpCall()
        {
            _floorButtonPanel.FloorId = 4;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(4);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor5UpCall()
        {
            _floorButtonPanel.FloorId = 5;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(5);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor6UpCall()
        {
            _floorButtonPanel.FloorId = 6;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(6);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor7UpCall()
        {
            _floorButtonPanel.FloorId = 7;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(7);
            _floorButtonPanel.UpButtonAsync();
        }
        public void Floor1DownCall()
        {
            _floorButtonPanel.FloorId = 1;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(1);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor2DownCall()
        {
            _floorButtonPanel.FloorId = 2;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(2);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor3DownCall()
        {
            _floorButtonPanel.FloorId = 3;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(3);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor4DownCall()
        {
            _floorButtonPanel.FloorId = 4;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(4);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor5DownCall()
        {
            _floorButtonPanel.FloorId = 5;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(5);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor6DownCall()
        {
            _floorButtonPanel.FloorId = 6;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(6);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor7DownCall()
        {
            _floorButtonPanel.FloorId = 7;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(7);
            _floorButtonPanel.DownButtonAsync();
        }
        public void Floor8DownCall()
        {
            _floorButtonPanel.FloorId = 8;
            _floorButtonPanel.Display = _floorButtonPanel.GetDisplay(8);
            _floorButtonPanel.DownButtonAsync();
        }

        public void LobbyInElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("Lobby");
        }

        public void Floor1InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("1");
        }
        public void Floor2InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("2");
        }
        public void Floor3InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("3");
        }
        public void Floor4InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("4");
        }
        public void Floor5InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("5");
        }
        public void Floor6InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("6");
        }
        public void Floor7InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("7");
        }
        public void Floor8InElevator()
        {
            _elevatorButtonPanel.PushButtonAsync("8");
        }

    }
}
