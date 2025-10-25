using System;

namespace lab3
{
    public class Room
    {
        
        private string name;
        private double temperature;
        private bool lightState;

        
        public event Action<bool> OnLightStateChanged;
        public event Action<double> OnTemperatureChanged;
        public event Action<string> OnNameChanged;

    
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя комнаты не может быть пустым");

                if (name != value)
                {
                    name = value;
                    OnNameChanged?.Invoke(value);
                }
            }
        }

        public double Temperature
        {
            get { return temperature; }
            set
            {
                if (temperature != value)
                {
                    temperature = value;
                    OnTemperatureChanged?.Invoke(value);
                }
            }
        }

        public bool LightState
        {
            get { return lightState; }
            set
            {
                if (lightState != value)
                {
                    lightState = value;
                    OnLightStateChanged?.Invoke(value);
                }
            }
        }

 
        public Room(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя комнаты не может быть пустым");

            this.name = name;
            this.temperature = 20;
            this.lightState = false;
        }

        
        public void TurnLightOn()
        {
            LightState = true;
        }

        public void TurnLightOff()
        {
            LightState = false;
        }

        public void LightSwitch()
        {
            LightState = !LightState;
        }

        
        public void SetTemperature(double newTemperature)
        {
            Temperature = newTemperature;
        }

        public void AddTemperature(double value)
        {
            Temperature += value;
        }
    }
}