using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SkiaSharp;
using SkiaSharp.Views.Forms;

/*Senciullo ejemplo que muestra como se pueden usar los elementos Xamarin.Forms junto a SkiaSharp (lo mismo de siempre), para crear una app que pinta el color seleccionado los CanvasViewPaintSueface*/
namespace Skia_Sharp_Prueba
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Color_SkiaSharp : ContentPage
	{
		public Color_SkiaSharp ()
		{
			InitializeComponent ();

            hueSlider.Value = 0;
            saturationSlider.Value = 100;
            lightnessSlider.Value = 50;
            valueSlider.Value = 100;
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            hslCanvasView.InvalidateSurface();
            hsvCanvasView.InvalidateSurface();
        }

        void OnHslCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKColor color = SKColor.FromHsl((float)hueSlider.Value,
                                            (float)saturationSlider.Value,
                                            (float)lightnessSlider.Value);
            args.Surface.Canvas.Clear(color);

            hslLabel.Text = String.Format(" RGB = {0:X2}-{1:X2}-{2:X2} ",
                                          color.Red, color.Green, color.Blue);
        }

        void OnHsvCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKColor color = SKColor.FromHsv((float)hueSlider.Value,
                                            (float)saturationSlider.Value,
                                            (float)valueSlider.Value);
            args.Surface.Canvas.Clear(color);

            hsvLabel.Text = String.Format(" RGB = {0:X2}-{1:X2}-{2:X2} ",
                                          color.Red, color.Green, color.Blue);
        }
    }
}