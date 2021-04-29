using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PropertyChangesBugRepro
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var button = this.FindControl<CustomButton>("CustomButton");

            button.PropertyChanged += (sender, args) =>
            {
                if (args.Property == CustomButton.CustomPropertyProperty)
                {
                    Console.WriteLine($"CustomPropertyProperty {args.NewValue}");
                }
                
                if (args.Property == CustomButton.IndicatorOrientationProperty)
                {
                    Console.WriteLine($"IndicatorOrientationProperty {args.NewValue}");
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}