using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Skia_Sharp_Prueba.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimpleCircle : ContentView
	{
		public SimpleCircle ()
		{
			InitializeComponent ();

            //PODEMOS DEFINIR EL CanvasView en C#, pero es mejor hacerlo en la vista XAML, ya que es mas claro todo y permite definir el TAP para controlar los clicks sobre el objeto SkiaSharp
            /*SKCanvasView canvasView = new SKCanvasView();//Objeto para hospedar los gráficos creados con SkiaSharp
            canvasView.PaintSurface += OnCanvasViewPaintSurface;//Evento controlador PaintSurface, que es donde se ćrearán los gráficos a pintar
            Content = canvasView*/;
        }

        //Evento donde se realiza todo el dibujo. Puede llamarse varias veces, por lo que debe mantener toda la información necesaria para volver a mostrar los gráficos
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;//Estructura que contiene información sobre la superficie de dibujo (COMO EL ANCHO Y ALTO DE PIXELES)
            SKSurface surface = args.Surface;//OBjeto que representa la superficie de dibujo. Su propiedad mas importante es Canvas de tipo SKCanvas (linea abajo)
            SKCanvas canvas = surface.Canvas;//Contexto gráfico que usamos para realizar los dibujos. Encapsula un estado de gráficos, que incluye transformaciones y el recorte

            canvas.Clear();//Método usado para limpiar el canvas con un color Transparente. Podemos pasarle como parámetro un color que será con el que limpiará, y lo establecerá
            //como fondo: canvas.Clear(SKColors.Yellow);

            //Se crea el contorno (borde) rojo del circulo
            SKPaint paint = new SKPaint//SKPaint es una colección de propiedades de debujo de gráficos.
            {
                Style = SKPaintStyle.Stroke,//Trazo de linea. Podemos establecer Stroke, fill (POR DEFECTO), o StrokeandFill (contorno y relleno del mismo color)
                Color = Color.Red.ToSKColor(),//Color del trazo. A DISTINGUIR entre colores de SkiaSharp (SKColors.Blue) y colores de xamarin (Color.Red). Se puede convertir los colores
                //de Xamarin a SkiaSharp con la propiedad ToSKColor (como en este ejemplo)
                StrokeWidth = 25//Pixeles del trazo
            };
            paint.IsAntialias = true;//Antialiasing a true para suavizart los bordes
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);//Las coordenadas se toman como referencia desde la esquina superior izquierda. 
            //Pintamos el borde círculo (coordenada X pantalla, coordenada Y pantalla, radio del círculo, objeto SkPain)

            //Pintamos el interior del circulo, creando otro circulo sobre el mismo punto del anterior. PINTA EL ANTERIOR, Y SOBRE EL SU RELLENO PARA QUE PUEDAN TENER 2 COLORES DISTINTOS
            if (showFill)//Togle que pinta y quita el relleno cuando hacemos tap (click) sobre el circulo
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = SKColors.Blue;
                canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
            }
        }

        bool showFill = true;
        //Función llamada cada vez que se hace click sobre el objeto SkiaSharp definido en el XAML
        void OnCanvasViewTapped(object sender, EventArgs args)
        {
            showFill ^= true;
            (sender as SKCanvasView).InvalidateSurface();//Genera una llamada PaintSurface para que vuelva a entrar a PaintSurface y pinte de nuevo
        }
    }    
}