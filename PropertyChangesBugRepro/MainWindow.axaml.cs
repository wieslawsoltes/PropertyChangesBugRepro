using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace PropertyChangesBugRepro
{
    [PseudoClasses(":horizontal", ":vertical")]
    public class CustomButton : Button
    {
        public static readonly StyledProperty<bool> CustomPropertyProperty = 
            AvaloniaProperty.Register<CustomButton, bool>(nameof(CustomProperty));

        public static readonly StyledProperty<Orientation> IndicatorOrientationProperty =
            AvaloniaProperty.Register<CustomButton, Orientation>(nameof(IndicatorOrientation), Orientation.Vertical);
        
        public bool CustomProperty
        {
            get => GetValue(CustomPropertyProperty);
            set => SetValue(CustomPropertyProperty, value);
        }
        
        public Orientation IndicatorOrientation
        {
            get => GetValue(IndicatorOrientationProperty);
            set => SetValue(IndicatorOrientationProperty, value);
        }
        
        public CustomButton()
        {
            UpdatePseudoClasses(IndicatorOrientation);
        }
        
        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == IndicatorOrientationProperty)
            {
                UpdatePseudoClasses(change.NewValue.GetValueOrDefault<Orientation>());
            }
        }
        
        private void UpdatePseudoClasses(Orientation orientation)
        {
            PseudoClasses.Set(":horizontal", orientation == Orientation.Horizontal);
            PseudoClasses.Set(":vertical", orientation == Orientation.Vertical);
        }
    }

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