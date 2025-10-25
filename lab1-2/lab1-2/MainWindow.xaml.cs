using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace CalendarApp
{
    public partial class MainWindow : Window
    {
        private DateTime currentDate;

        public MainWindow()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            InitializeControls();
            UpdateCalendar();
        }

        private void InitializeControls()
        {
            
            cmbMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                cmbMonth.Items.Add(new ComboBoxItem { Content = monthName, Tag = i });
            }

            
            cmbYear.Items.Clear();
            for (int year = DateTime.Now.Year - 10; year <= DateTime.Now.Year + 10; year++)
            {
                cmbYear.Items.Add(year);
            }

            
            cmbMonth.SelectedIndex = currentDate.Month - 1;
            cmbYear.SelectedItem = currentDate.Year;
        }

        private void UpdateCalendar()
        {
            calendarGrid.Children.Clear();

            int year = currentDate.Year;
            int month = currentDate.Month;

            
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            
            DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            
            int firstDayWeekDay = ((int)firstDayOfMonth.DayOfWeek == 0) ? 7 : (int)firstDayOfMonth.DayOfWeek;

            
            int daysFromPreviousMonth = firstDayWeekDay - 1;

            
            DateTime displayDate = firstDayOfMonth.AddDays(-daysFromPreviousMonth);

            
            for (int i = 0; i < 42; i++)
            {
                Border dayBorder = new Border
                {
                    BorderBrush = SystemColors.ActiveBorderBrush,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(2),
                    Background = SystemColors.WindowBrush
                };

                TextBlock dayText = new TextBlock
                {
                    Text = displayDate.Day.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5),
                    FontSize = 14
                };

               
                if (displayDate.Month != month)
                {

                    dayText.Foreground = SystemColors.GrayTextBrush;
                    dayBorder.Background = SystemColors.ControlBrush;
                }
                else if (displayDate.DayOfWeek == DayOfWeek.Saturday || displayDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    
                    dayText.Foreground = System.Windows.Media.Brushes.Red;
                    dayBorder.Background = System.Windows.Media.Brushes.LightYellow;
                }
                else
                {
                    
                    dayText.Foreground = SystemColors.WindowTextBrush;
                }

               
                if (displayDate.Date == DateTime.Now.Date && displayDate.Month == month)
                {
                    dayBorder.BorderBrush = System.Windows.Media.Brushes.Blue;
                    dayBorder.BorderThickness = new Thickness(2);
                }

                dayBorder.Child = dayText;
                calendarGrid.Children.Add(dayBorder);

                displayDate = displayDate.AddDays(1);
            }
        }

        private void DateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMonth.SelectedItem != null && cmbYear.SelectedItem != null)
            {
                int month = (cmbMonth.SelectedItem as ComboBoxItem).Tag as int? ?? currentDate.Month;
                int year = (int)cmbYear.SelectedItem;

                
                if (month == 2 && year == currentDate.Year && currentDate.Month == 2)
                {
                    if (currentDate.Day > DateTime.DaysInMonth(year, month))
                    {
                        currentDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    }
                    else
                    {
                        currentDate = new DateTime(year, month, currentDate.Day);
                    }
                }
                else
                {
                    currentDate = new DateTime(year, month, Math.Min(currentDate.Day, DateTime.DaysInMonth(year, month)));
                }

                UpdateCalendar();
            }
        }

       
        private void BtnPrevMonth_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            UpdateComboBoxes();
            UpdateCalendar();
        }

        private void BtnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            UpdateComboBoxes();
            UpdateCalendar();
        }

        private void BtnPrevYear_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddYears(-1);
            UpdateComboBoxes();
            UpdateCalendar();
        }

        private void BtnNextYear_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddYears(1);
            UpdateComboBoxes();
            UpdateCalendar();
        }

        private void UpdateComboBoxes()
        {
            cmbMonth.SelectedIndex = currentDate.Month - 1;
            cmbYear.SelectedItem = currentDate.Year;
        }
    }
}