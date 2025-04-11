using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using System;
using System.Globalization;

namespace HeatingOptimizer.UI
{
    public partial class AddDataWindow : Window
    {
    
        // This is the constructor for the AddDataWindow class. It initializes the window and its components.
        public AddDataWindow()
        {
            InitializeComponent();
            DataContext = new AddDataViewModel(); // Set the DataContext to a AddDataViewModel
            CreateFields();
        }
        // a function for adding the fields to the window dynamically
        private void CreateFields()
        {
            var viewModel = (AddDataViewModel)DataContext;

            for (int i = 0; i < viewModel.MachineFields.Count; i++)
            {
                // Create RowDefinition
                MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                MainGrid.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
                MainGrid.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;

                // Create Label
                Label label = new()
                {
                    Content = viewModel.MachineFields[i].Label + ":",
                    Margin = new Thickness(10),
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                    Width = 200
                };
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                MainGrid.Children.Add(label);

                // Create TextBox
                TextBox textBox = new()
                {
                    Margin = new Thickness(10),
                    Width = 200,
                    DataContext = viewModel.MachineFields[i]
                };
                textBox.Bind(TextBox.TextProperty, new Binding("Value"));
                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, 1);
                MainGrid.Children.Add(textBox);
            }
        }
        // end of the function
        // private void AddMachine(object sender, RoutedEventArgs e)
        // {
        //     var viewModel = (AddDataViewModel?)DataContext;
        //     viewModel?.AddMachineData(); // Call the method in the view model
        // }
    }
}