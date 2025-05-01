using System.Windows;
using System.Windows.Controls;
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
            amountHours.Add(((CardActivity)control).Time);
        }

        label_AmountHoursValue.Content = amountHours.ToString(@"hh\:mm");
    }


    private void border_add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        UpdateCardActivity updateCardActivity = new UpdateCardActivity();

        try
        {
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
            updateCardActivity = null;
        }

    }

    private void border_export_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {

    }


    private void cardActivity_update_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        UpdateCardActivity updateCardActivity = new UpdateCardActivity();

        try
        {
            updateCardActivity.ShowDialog();

            if (updateCardActivity.updateComponent)
            {
                ((CardActivity)sender).Insert_Informations(updateCardActivity.Description, updateCardActivity.time);

                UpdateAmountHours();
            }
        }
        catch (Exception ex)
        {
            //NEED IMPLEMENTS EXCEPTIONS
        }
        finally
        {
            updateCardActivity = null;
        }

    }

    private void cardActivity_remove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            stack_content.Children.Remove((CardActivity)sender);
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
}
