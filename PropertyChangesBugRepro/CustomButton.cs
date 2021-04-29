using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Layout;

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
}