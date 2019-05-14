using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public static class ShadowAdornerAssist
    {
        public static DependencyProperty ShadowDepthProperty = DependencyProperty.RegisterAttached("ShadowDepth", typeof(ShadowDepth), typeof(ShadowAdornerAssist),
            new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ShadowDepthProprtyChangedCallback)));

        public static ShadowDepth GetShadowDepth(DependencyObject obj)
        {
            return (ShadowDepth)obj.GetValue(ShadowDepthProperty);
        }

        public static void SetShadowDepth(DependencyObject obj, ShadowDepth value)
        {
            obj.SetValue(ShadowDepthProperty, value);
        }

        private static void ShadowDepthProprtyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateShadowDepth((UIElement)d, (ShadowDepth)e.NewValue);
        }

        public static DependencyProperty ShadowAdornerProperty = DependencyProperty.RegisterAttached("ShadowAdorner", typeof(ShadowAdorner), typeof(ShadowAdornerAssist));

        public static ShadowAdorner GetShadowAdorner(DependencyObject obj)
        {
            return (ShadowAdorner)obj.GetValue(ShadowAdornerProperty);
        }

        public static void SetShadowAdorner(DependencyObject obj, ShadowAdorner value)
        {
            obj.SetValue(ShadowAdornerProperty, value);
        }

        private static void UpdateShadowDepth(UIElement element, ShadowDepth shadowDepth, bool loaded = false)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(element);

            if (adornerLayer == null)
            {
                if (!loaded)
                    element.Dispatcher.InvokeAsync(() => UpdateShadowDepth(element, shadowDepth, true), DispatcherPriority.Loaded);

                return;
            }

            var shadowAdorner = GetShadowAdorner(element);

            if (shadowDepth == ShadowDepth.Depth0)
            {
                if (shadowAdorner != null)
                {
                    SetShadowAdorner(element, null);
                    adornerLayer.Remove(shadowAdorner);
                }
            }
            else
            {
                if (shadowAdorner == null)
                {
                    shadowAdorner = new ShadowAdorner(element);
                    MaterialDesignThemes.Wpf.ShadowAssist.SetShadowDepth(shadowAdorner.Shadow, shadowDepth);
                    adornerLayer.Add(shadowAdorner);
                    SetShadowAdorner(element, shadowAdorner);
                }
                else
                {
                    MaterialDesignThemes.Wpf.ShadowAssist.SetShadowDepth(shadowAdorner.Shadow, shadowDepth);
                }
            }
        }
    }
}
