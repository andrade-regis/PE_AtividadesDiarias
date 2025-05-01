using System.Windows.Controls;

namespace PE_AtividadesDiárias.UI;

/// <summary>
/// Interação lógica para Activity.xam
/// </summary>
public partial class CardActivity : UserControl
{
    public CardActivity()
    {
        InitializeComponent();
    }

    public string ActivityDescription { get; private set; }

    public TimeSpan Time { get; private set; }

    public void Insert_Informations(string activityDescription, 
                                    TimeSpan time)
    {
        ActivityDescription = activityDescription;
        Time = time;

        Update_Controller();
    }

    public void Update_Controller()
    {
        Activity_Description.Text = ActivityDescription;
        Activity_Time.Content = Time.ToString(@"hh\:mm");
    }
}
