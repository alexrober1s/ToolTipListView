using Android.Content;
using BaseXamarin.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using static Android.Views.View;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Content.Res;
using System.Threading;
using System.Threading.Tasks;
using BaseXamarin.Custom;
using Com.Tomergoldst.Tooltips;
using static Com.Tomergoldst.Tooltips.ToolTipsManager;

[assembly: ExportRenderer(typeof(BaseFrame), typeof(BaseFrameRenderer))]
namespace BaseXamarin.Droid.CustomRenderers
{
    public class BaseFrameRenderer : FrameRenderer, IOnTouchListener
    {
        ToolTipsManager _toolTipsManager;
        public BaseFrameRenderer(Context context) : base(context)
        {
            var listener = new TipListener();
            _toolTipsManager = new ToolTipsManager(listener);
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            Clickable = true;
            Focusable = true;
            this.SetOnTouchListener(this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateBackgroundDrawable()
        {
            var backgroundDrawable = new RippleDrawable(ColorStateList.ValueOf(Element.BackgroundColor.ToAndroid()), null, null);
            Background = backgroundDrawable;
        }
      
        public void OnClick()
        {
            // do nothing
        }

        bool _longclickHandled;
        void OnLongClick()
        {
            if (Element != null)
            {
                var el = Element as BaseFrame;
                if (!string.IsNullOrEmpty(el.ToolTipText))
                {
                    _longclickHandled = true;
                    DisplayToolTip(el.ToolTipText);
                }
            }
        }

        void DisplayToolTip(string text)
        {
            ToolTip.Builder builder;
            _toolTipsManager?.FindAndDismiss(this);
            builder = new ToolTip.Builder(Context, this, this.RootView as ViewGroup, text.PadRight(500, ' '), ToolTip.PositionAbove);
            builder.SetAlign(ToolTip.AlignCenter);
            builder.SetBackgroundColor(((BaseFrame)Element).ToolTipBackgroundColor.ToAndroid());
            builder.SetTextColor(Color.White.ToAndroid());
            var toolTipView = builder.Build();
            _toolTipsManager?.Show(toolTipView);
        }

        CancellationTokenSource cancellationToken;
        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        async void StartTimerForLongClickAsync()
        {

            if (cancellationToken != null)
            {
                cancellationToken.Cancel();
            }

            await semaphoreSlim.WaitAsync();
            cancellationToken = new CancellationTokenSource();
            try
            {
                await Task.Delay(500);
            }
            finally
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    OnLongClick();
                }

                semaphoreSlim.Release();
            }
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            
            switch(e.Action)
            {
                case MotionEventActions.Down:
                    StartTimerForLongClickAsync();
            
                    break;
                case MotionEventActions.Up:
                    if (!_longclickHandled)
                    {   
                        cancellationToken?.Cancel();
                        OnClick();
                    } else
                    {
                        _toolTipsManager.FindAndDismiss(this);
                        _longclickHandled = false;
                    }
                    break;
                case MotionEventActions.Cancel:
                    cancellationToken?.Cancel();
                    _toolTipsManager.FindAndDismiss(this);
                    _longclickHandled = false;
                    break;
            }

            return false;
        }

        class TipListener : Java.Lang.Object, ITipListener
        {
            public void OnTipDismissed(Android.Views.View p0, int p1, bool p2)
            {

            }
        }

    }
}