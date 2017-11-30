using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XF.Recursos.CustomControl;
using XF.Recursos.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FiapButton), typeof(FiapButtonRenderer))]
namespace XF.Recursos.Droid {
    class FiapButtonRenderer : ButtonRenderer {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e) {
            base.OnElementChanged(e);

            FiapButton fiapButton = (FiapButton)Element;
            //TODO: Create a Native Button, set it's text and Redraw the object

            if (Control != null) {
                Control.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
        }
    }
}
