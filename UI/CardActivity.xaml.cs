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

    public int Id { get; set; }

    public string ActivityDescription { get; set; }

    public TimeSpan Time { get;  set; }
    
    public DateTime Day { get; set; }  

    public void Insert_Informations(int id, 
                                    string activityDescription, 
                                    TimeSpan time, 
                                    DateTime day)
    {
            Id = id;
            ActivityDescription = activityDescription;
            Time = time;
            Day = day;
    }

    public void Update_Controller()
    {
        Activity_Description.Text = ActivityDescription;
        Activity_Time.Content = Time.ToString(@"hh\:mm");
    }
}
