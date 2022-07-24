using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Printer
{
    class Program
    {
        static void Main(string[] args)
        {
            float sin18 = (float)(Math.Sin(dtor(18)));
            float cos18 = (float)(Math.Cos(dtor(18)));
            float sin81 = (float)(Math.Sin(dtor(81)));
            float cos81 = (float)(Math.Cos(dtor(81)));

            string DirectoryName = "images";
            if (Directory.Exists(DirectoryName))
            {
                for (int i = 0; i < 50; i++)
                {
                    if (!Directory.Exists($"{DirectoryName}_{i:00}"))
                    {
                        DirectoryName = $"{DirectoryName}_{i:00}";
                        break;
                    }
                }
            }

            Directory.CreateDirectory(DirectoryName);

            for (int i = 1; i <= 100; i++)
            {
                try
                {
                    PointF[] vertices = new PointF[5];
                    float rate = i / 20.0f;
                    float ratio = 1.0f / (sin18 + rate * sin81);
                    float ratioRate = ratio * rate;

                    vertices[1].X = ratio * cos18;
                    vertices[1].Y = ratio * sin18;
                    vertices[2].X = ratioRate * cos81 + vertices[1].X;
                    vertices[2].Y = 1.0f;
                    vertices[3].X = -vertices[1].X;
                    vertices[3].Y = vertices[1].Y;
                    vertices[4].X = -vertices[2].X;
                    vertices[4].Y = 1.0f;

                    PointF[] points = new PointF[10];
                    float UVRate = 0.5f / (0.3f + vertices[2].X * 2);

                    points[0] = new PointF(0.25f, 0);
                    points[1] = new PointF(0.25f + vertices[1].X * UVRate, vertices[1].Y * UVRate);
                    points[2] = new PointF(0.5f - 0.15f * UVRate, UVRate);
                    points[3] = new PointF(0.25f - vertices[1].X * UVRate, points[1].Y);
                    points[4] = new PointF(0.15f * UVRate, UVRate);
                    points[5] = new PointF(0.75f, 0);
                    points[6] = new PointF(0.5f + points[1].X, points[1].Y);
                    points[7] = new PointF(0.5f + points[2].X, UVRate);
                    points[8] = new PointF(0.5f + points[3].X, points[1].Y);
                    points[9] = new PointF(0.5f + 0.15f * UVRate, UVRate);

                    for (int j = 0; j < points.Length; j++)
                    {
                        points[j].X *= 1024;
                        points[j].Y *= 1024;
                    }

                    Bitmap bitmap = new Bitmap(1024, (int)(UVRate * 1024.0f));
                    Graphics graphics = Graphics.FromImage(bitmap);

                    graphics.DrawLine(Pens.Black, points[0], points[1]);
                    graphics.DrawLine(Pens.Black, points[1], points[2]);
                    graphics.DrawLine(Pens.Black, points[0], points[3]);
                    graphics.DrawLine(Pens.Black, points[3], points[4]);
                    graphics.DrawLine(Pens.Black, points[5], points[6]);
                    graphics.DrawLine(Pens.Black, points[6], points[7]);
                    graphics.DrawLine(Pens.Black, points[5], points[8]);
                    graphics.DrawLine(Pens.Black, points[8], points[9]);
                    graphics.DrawLine(Pens.Black, points[1], points[8]);
                    graphics.DrawLine(Pens.Black, points[3], new PointF(0, points[3].Y));
                    graphics.DrawLine(Pens.Black, points[6], new PointF(1024, points[6].Y));
                    graphics.Dispose();

                    bitmap.Save($"{DirectoryName}/image_{rate:0.00}.png", System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static double dtor(int degree)
        {
            return Math.PI * degree / 180.0f;
        }
    }
}
