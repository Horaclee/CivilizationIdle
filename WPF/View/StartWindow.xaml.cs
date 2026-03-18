using System.Windows;

namespace WPF.View;

public partial class StartWindow
{
    public StartWindow() => InitializeComponent();
    

    private void StartGame_Click(object sender, RoutedEventArgs e)
    {
        var window = new MainWindow(loadGame: false);
        window.Show();
        Close();
    }

    private void LoadGame_Click(object sender, RoutedEventArgs e)
    {
        var window = new MainWindow(loadGame: true);
        window.Show();
        Close();
    }

    private void Exit_Click(object sender, RoutedEventArgs e) => Close();
    
}
