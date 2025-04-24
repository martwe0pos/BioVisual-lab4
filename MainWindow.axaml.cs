using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace lab4;

public partial class MainWindow : Window
{
    private int deg;
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public async void load(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        var file = await openFileDialog.ShowAsync(this);
        var bitmap = new Bitmap(file[0]);
        image.Source = bitmap;
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
            var rt = new RotateTransform(deg);
            image.RenderTransform = rt;
        }
    }
}