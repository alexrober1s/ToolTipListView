using Xamarin.Forms;

namespace BaseXamarin.Custom
{
	public class BaseFrame : Frame
	{
        public static readonly BindableProperty ToolTipTextProperty =
            BindableProperty.Create(nameof(ToolTipText), typeof(string), typeof(BaseFrame), null);

        public string ToolTipText
        {
            get => (string)GetValue(ToolTipTextProperty);
            set => SetValue(ToolTipTextProperty, value);
        }

        public static readonly BindableProperty ToolTipBackgroundColorProperty =
            BindableProperty.Create(nameof(ToolTipBackgroundColor), typeof(Color), typeof(BaseFrame), Color.White);

        public Color ToolTipBackgroundColor
        {
            get => (Color)GetValue(ToolTipBackgroundColorProperty);
            set => SetValue(ToolTipBackgroundColorProperty, value);
        }
    }
}