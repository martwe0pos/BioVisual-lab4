using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaPixelSnoop;

namespace lab4;

public partial class MainWindow : Window
{
    WriteableBitmap bitmap;
    private int temp_deg = 0;
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
    
    public void green(object sender, RoutedEventArgs e)
    {
        using (BmpPixelSnoop snoop = new(bitmap))
        {
            Color color, grn;
            for (var i = 0; i < bitmap.PixelSize.Height; i++)
            {
                for (int j = 0; j < bitmap.PixelSize.Width; j++)
                {
                    color = snoop.GetPixel(i, j);
                    grn = Color.FromArgb(color.A, 0, color.G, 0);
                    snoop.SetPixel(i, j, grn);
                }
            }
            img.RenderTransform = new RotateTransform(temp_deg);
        }
    }
}