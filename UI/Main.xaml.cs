using System.Windows;
using System.Windows.Input;

namespace PE_AtividadesDiárias.UI;

/// <summary>
/// Lógica interna para Main.xaml
/// </summary>
public partial class Main : Window
{
    public Main()
    {
        InitializeComponent();

        InitializeActions();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        this.DragMove();
    }

    private void InitializeActions()
    {
        label_Date.Content = DateTime.Now.ToString("dd/MM/yyyy");

        label_AmountHoursValue.Content = "00:00";
    }

    private void border_add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void border_export_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {

    }
}
