using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System;


namespace HeatingOptimizer.UI
{
    // adding a field for add data window fields to add them dinamically
    public class FieldDefinition : ObservableObject
    {
        public string? Label { get; set; }
        public string? Value { get; set; }
    }
    // adding ta view model for the add data window
    public class AddDataViewModel : ObservableObject
    {
        private ObservableCollection<FieldDefinition>? _machineFields;
        public ObservableCollection<FieldDefinition>? MachineFields
        {
            get => _machineFields;
            set => SetProperty(ref _machineFields, value);
        }
        private object ProductionUnit { get; set; } // Replace 'object' with the actual type of ProductionUnit

        public AddDataViewModel()
        {
            ProductionUnit = new ProductionUnit(); // Replace 'ProductionUnit' with the actual class name if different
            MachineFields = new ObservableCollection<FieldDefinition>();

            foreach (var property in ProductionUnit.GetType().GetProperties())
            {
                var field = new FieldDefinition
                {
                    Label = property.Name,
                    Value = ""
                };
                MachineFields.Add(field);
            }
        }

        // // still need to make it save to a data base, but it shows the data user enters
        // public void AddMachineData()
        // {
        //     foreach (var field in MachineFields)
        //     {
        //         string? label = field.Label;
        //         string? value = field.Value;
        //         // Process the values as needed
        //         Console.WriteLine($"{label}: {value}");
        //     }
        // }
    }
}