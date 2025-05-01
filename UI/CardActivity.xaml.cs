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

    public TimeSpan ActivityTime { get; private set; }

    public void Insert_Informations(string activityDescription, 
                                    TimeSpan activityTime)
    {
        ActivityDescription = activityDescription;
        ActivityTime = activityTime;

        Update_Controller();
    }

    public void Update_Controller()
    {
        Activity_Description.Text = ActivityDescription;
        Activity_Time.Text = ActivityTime.ToString(@"hh\:mm");
    }
}
