using System;
using System.Windows;
using System.Windows.Controls;

namespace lab3
{
    public partial class MainWindow : Window
    {
        private Room currentRoom;
        private bool isUpdatingUI = false;

        public MainWindow()
        {
            InitializeComponent();

           
            currentRoom = new Room("Кухня");

          
            currentRoom.OnLightStateChanged += OnLightStateChangedHandler;
            currentRoom.OnTemperatureChanged += OnTemperatureChangedHandler;
            currentRoom.OnNameChanged += OnNameChangedHandler;

            
            UpdateInterface();
        }

       
        private void OnLightStateChangedHandler(bool state)
        {
            if (LightCheckBox != null && !isUpdatingUI)
            {
                isUpdatingUI = true;
                LightCheckBox.IsChecked = state;
                isUpdatingUI = false;
            }
        }

        
        private void OnTemperatureChangedHandler(double temperature)
        {
            if (!isUpdatingUI)
            {
                isUpdatingUI = true;

                if (TemperatureSlider != null)
                {
                    TemperatureSlider.Value = temperature;
                }

               
                if (TemperatureText != null)
                {
                    TemperatureText.Text = temperature.ToString("0");
                }

                isUpdatingUI = false;

             
                CheckTemperatureWarning(temperature);
            }
        }

 
        private void OnNameChangedHandler(string name)
        {
            if (RoomNameText != null)
            {
                RoomNameText.Text = name;
            }
        }

      
        private void CheckTemperatureWarning(double temperature)
        {
            if (temperature > 28)
            {
                MessageBox.Show("Внимание: Температура слишком высокая!", "Предупреждение",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (temperature < 10)
            {
                MessageBox.Show("Внимание: Температура слишком низкая!", "Предупреждение",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
        private void UpdateInterface()
        {
            isUpdatingUI = true;

            OnNameChangedHandler(currentRoom.Name);

            if (TemperatureSlider != null)
                TemperatureSlider.Value = currentRoom.Temperature;
            if (TemperatureText != null)
                TemperatureText.Text = currentRoom.Temperature.ToString("0");
            if (LightCheckBox != null)
                LightCheckBox.IsChecked = currentRoom.LightState;

            isUpdatingUI = false;
        }

        
        private void LightCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isUpdatingUI && currentRoom != null)
            {
                currentRoom.TurnLightOn();
            }
        }

        private void LightCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!isUpdatingUI && currentRoom != null)
            {
                currentRoom.TurnLightOff();
            }
        }

        private void TemperatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isUpdatingUI && currentRoom != null)
            {
                currentRoom.SetTemperature(e.NewValue);
            }
        }

        private void IncreaseTempButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom != null)
            {
                currentRoom.AddTemperature(1);
            }
        }

        private void DecreaseTempButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom != null)
            {
                currentRoom.AddTemperature(-1);
            }
        }
    }
}