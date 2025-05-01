using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PE_AtividadesDiárias.UI;

/// <summary>
/// Lógica interna para Main.xaml
/// </summary>
public partial class Main : Window
{
    public Main()
    {
        InitializeComponent();

        InitialActions();
    }


    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        string source = e.Source.ToString();

        if(!e.Source.ToString().Contains("Label"))
        {
            this.DragMove();
        }
    }

    
    private void InitialActions()
    {
        label_Date.Content = DateTime.Now.ToString("dd/MM/yyyy");

        label_AmountHoursValue.Content = "00:00";
    }

    private void UpdateAmountHours()
    {
        TimeSpan amountHours = new TimeSpan(0, 0, 0);

        foreach(UserControl control in stack_content.Children)
        {
            amountHours += ((CardActivity)control).ActivityTime;
        }

        label_AmountHoursValue.Content = amountHours.ToString(@"hh\:mm");
    }


    private void label_Date_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }

    private void border_add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        UpdateCardActivity updateCardActivity = new UpdateCardActivity();

        try
        {
            this.Visibility = Visibility.Hidden;

            updateCardActivity.ShowDialog();

            if (updateCardActivity.updateComponent)
            {
                CardActivity cardActivity = new CardActivity();
                
                cardActivity.Insert_Informations(updateCardActivity.Description, updateCardActivity.time);

                cardActivity.border_update.MouseLeftButtonDown += cardActivity_update_MouseLeftButtonDown;
                cardActivity.border_remove.MouseLeftButtonDown += cardActivity_remove_MouseLeftButtonDown;

                stack_content.Children.Add(cardActivity);

                UpdateAmountHours();
            }
        }
        catch (Exception ex)
        {
            //NEED IMPLEMENTS EXCEPTIONS
        }
        finally
        {
            this.Visibility = Visibility.Visible;

            updateCardActivity = null;
        }

    }

    private void border_export_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Data da Atividade - {label_Date.Content}");
        stringBuilder.AppendLine(string.Empty);

        foreach (CardActivity card in stack_content.Children)
        {
            stringBuilder.AppendLine(card.ActivityDescription);
            stringBuilder.AppendLine(card.ActivityTime.ToString(@"hh\:mm"));
            stringBuilder.AppendLine(string.Empty);
        }

        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                   $"Tasks_{label_Date.Content.ToString().Replace("/", "-")}.txt");

        File.WriteAllText(path,
                          stringBuilder.ToString());
    }


    private void cardActivity_update_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.Visibility = Visibility.Hidden;

        UpdateCardActivity updateCardActivity;

        try
        {
            getActivity_Description(sender, out string activity_description);
            getActivity_Time(sender, out TimeSpan activity_time);

            updateCardActivity = new UpdateCardActivity(activity_description, activity_time);

            updateCardActivity.ShowDialog();

            if (updateCardActivity.updateComponent)
            {
                getActivity_Card(sender, out DependencyObject parent);

                ((CardActivity)parent).Insert_Informations(updateCardActivity.Description, updateCardActivity.time);

                UpdateAmountHours();
            }
        }
        catch (Exception ex)
        {
            //NEED IMPLEMENTS EXCEPTIONS
        }
        finally
        {
            this.Visibility = Visibility.Visible;

            updateCardActivity = null;
        }

    }

    private void cardActivity_remove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            getActivity_Card(sender, out DependencyObject parent);

            stack_content.Children.Remove((CardActivity)parent);
        }
        catch (Exception ex)
        {
            //NEED IMPLEMENTS EXCEPTIONS
        }
        finally
        {
            UpdateAmountHours();
        }

    }


    private void getActivity_Card(object sender, out DependencyObject parent)
    {
        parent = VisualTreeHelper.GetParent((Border)sender);

        while (parent != null && !(parent is UserControl))
        {
            parent = VisualTreeHelper.GetParent(parent);
        }
    }

    private void getActivity_Description(object sender, out string activity_description)
    {
        activity_description = string.Empty;

        var grid = VisualTreeHelper.GetParent((Border)sender) as Grid;

        foreach (UIElement element in grid.Children)
        {
            if (element is TextBlock textblock &&
               textblock.Name == "Activity_Description")

            {
                activity_description = textblock.Text;
                break;
            }
        }
    }

    private void getActivity_Time(object sender, out TimeSpan activity_time)
    {
        activity_time = new TimeSpan();

        var grid = VisualTreeHelper.GetParent((Border)sender) as Grid;

        foreach (UIElement element in grid.Children)
        {
            if (element is TextBlock textblock &&
               textblock.Name == "Activity_Time")

            {
                TimeSpan result;

                if (textblock.Text.Contains(":"))
                    TimeSpan.TryParse(textblock.Text, out result);
                else
                    TimeSpan.TryParse(textblock.Text.Insert(2, ":"), out result);

                activity_time = result;
                break;
            }
        }
    }    
}
