using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaPixelSnoop;
using Color = Avalonia.Media.Color;

namespace lab4;

public partial class MainWindow : Window
{
    private int deg, temp_deg;
    private WriteableBitmap bitmap;
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public async void load(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        var file = await openFileDialog.ShowAsync(this);
        FileStream fs = File.OpenRead(file[0]);
        bitmap = WriteableBitmap.Decode(fs);
        img.Source = bitmap;
    }

    public void degrees(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton rb)
        {
            deg = int.Parse(rb.Tag.ToString());
        }
    }
    public void rotate(object sender, RoutedEventArgs e)
    {
        if (sender is Button bt)
        {
            temp_deg = deg;
            img.RenderTransform = new RotateTransform(deg);
        }
    }

    public void invert(object sender, RoutedEventArgs e)
    {
        if (sender is Button bt && bitmap != null)
        {
            using (BmpPixelSnoop snoop = new(bitmap))
            {
                for (var i = 0; i < bitmap.PixelSize.Height; i++)
                {
                    for (var j = 0; j < bitmap.PixelSize.Width; j++)
                    {
                        var orgCol = snoop.GetPixel(i, j);
                        var invCol = Color.FromArgb(orgCol.A, (byte)(255 - orgCol.R), (byte)(255 - orgCol.G), (byte)(255 - orgCol.B));
                        snoop.SetPixel(i ,j ,invCol);
                    }
                }
                img.RenderTransform = new RotateTransform(temp_deg);
            }
        }
    }

    public void upside(object sender, RoutedEventArgs e)
    {
        if (sender is Button bt && bitmap != null)
        {
            using (BmpPixelSnoop snoop = new(bitmap))
            {
                Color x, y;
                for (var i = 0; i < bitmap.PixelSize.Height / 2; i++)
                {
                    for (var j = 0; j < bitmap.PixelSize.Width; j++)
                    {
                        x = snoop.GetPixel(j, i);
                        y = snoop.GetPixel(j, bitmap.PixelSize.Height - i - 1);
                        snoop.SetPixel(j, i, y);
                        snoop.SetPixel(j, bitmap.PixelSize.Height - i - 1, x);
                    }
                }
                img.RenderTransform = new RotateTransform(temp_deg);
            }
        }
        
    }
}