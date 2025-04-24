using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace lab4;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public async void load(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        var result = await openFileDialog.ShowAsync(this);
        var bitmap = new Bitmap(result[0]);
        image.Source = bitmap;
    }
}