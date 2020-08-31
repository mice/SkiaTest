using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkiaImageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            __DrawGfx();
            //DrawText();
            //DrawDoubleText();
            Console.ReadKey();
        }

        private static void __DrawGfx()
        {
            var rotated = new SKBitmap(1024, 1024);
            var info = new SKRect(0,0, 1024, 1024);
            using (SKCanvas canvas = new SKCanvas(rotated)) { 
                using (SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Red,
                    StrokeWidth = 25,
                    IsAntialias = true
                })
                {
                    canvas.RotateDegrees(2);
                    canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = SKColors.Blue;
                    canvas.DrawCircle(info.Width / 2, info.Height / 2, 75, paint);

                    canvas.RotateDegrees(20);
                    canvas.DrawCircle(info.Width / 2, info.Height / 2, 50, paint);
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = SKColors.Yellow;
                    canvas.DrawCircle(info.Width / 2, info.Height / 2, 25, paint);

                    paint.Color = SKColors.Red;
                    canvas.ResetMatrix(); 
                    canvas.DrawOval(info.Width / 4, info.Height / 2, 25, 50, paint);
                }
            }
            using (var filstream = File.Open("xsj02_039_13_Gfx.png", FileMode.OpenOrCreate))
            {
                rotated.Encode(filstream, SKEncodedImageFormat.Png, 1);
            }
        }


        private static void DrawEmojii()
        {
            int GrinningFaceEmoji = 0x1f600;
            var rotated = new SKBitmap(256, 256);
            using (SKCanvas canvas = new SKCanvas(rotated)) 
            {
                string emoji; int x; int y;
                SKPaint paint = new SKPaint();
                paint.Typeface = SKFontManager.CreateDefault().MatchCharacter(GrinningFaceEmoji);

                //canvas.DrawText(emoji, x, y, vtsm.paint);
            }
        }

        private static void DrawDoubleText()
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();

           
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var timer2 = System.Diagnostics.Stopwatch.StartNew();
                var rotated = new SKBitmap(256, 256);
                Console.WriteLine(":::::1111:::" + timer2.ElapsedMilliseconds);
                using (SKCanvas canvas = new SKCanvas(rotated))
                {
                    canvas.Clear(new SKColor(19, 139, 4));
                    string text = "614";
                    Console.WriteLine(":::::2222:::" + timer2.ElapsedMilliseconds);
                    var paint = new SKPaint
                    {
                        Color = SKColors.Black,
                        IsAntialias = true,
                        Style = SKPaintStyle.Fill,
                        TextAlign = SKTextAlign.Center,
                        TextSize = 12,
                        Typeface = SKTypeface.FromFamilyName("Terminal", SKTypefaceStyle.Bold)
                    };
                    var info = SKRect.Create(0, 0);
                    paint.MeasureText(text, ref info);

                    var coord = new SKPoint(info.Width / 2, (info.Height + paint.TextSize) / 2);
                    canvas.DrawText("614", coord, paint);

                    paint.Color = new SKColor(0, 255, 0);
                    coord.Offset(1, -1);
                    canvas.DrawText("614", coord, paint);
                }
                Console.WriteLine(":::::3333:::" + timer2.ElapsedMilliseconds);
                using (var filstream = File.Open("xsj02_039_13_Dtext.png", FileMode.OpenOrCreate))
                {
                    rotated.Encode(filstream, SKEncodedImageFormat.Png, 1);
                }
            });
           
            Console.WriteLine(":::" + timer.ElapsedMilliseconds);
           
          
        }

        private static void DrawText()
        {
            using (var bitmap = SKBitmap.Decode("xsj02_039_13.png"))
            {
                var rotated = new SKBitmap(bitmap.Height, bitmap.Width);
                string text = "Hello";
                using (SKPaint textPaint = new SKPaint { TextSize = 48 })
                {
                    SKRect bounds = new SKRect();
                    textPaint.MeasureText(text, ref bounds);
                   

                    using (SKCanvas bitmapCanvas = new SKCanvas(rotated))
                    {
                        bitmapCanvas.Clear();
                        bitmapCanvas.DrawBitmap(bitmap, SKRect.Create(bitmap.Height, bitmap.Width), textPaint);
                        //bitmap.CopyTo(rotated);
                        bitmapCanvas.DrawText(text, 0, -bounds.Top, textPaint);
                    }
                }


                using (var filstream = File.Open("xsj02_039_13_text.png", FileMode.OpenOrCreate))
                {
                    rotated.Encode(filstream, SKEncodedImageFormat.Png, 1);
                }
            }
        }


        private static void RotateImage()
        {
            using (var bitmap = SKBitmap.Decode("xsj02_039_13.png"))
            {
                var rotated = new SKBitmap(bitmap.Height, bitmap.Width);

                using (var surface = new SKCanvas(rotated))
                {
                    surface.Translate(rotated.Width, 0);
                    surface.RotateDegrees(90);
                    surface.DrawBitmap(bitmap, 0, 0);
                }

                using (var filstream = File.Open("xsj02_039_13_rot.png", FileMode.OpenOrCreate))
                {
                    rotated.Encode(filstream, SKEncodedImageFormat.Png, 1);
                }
            }
        }
    }
}
