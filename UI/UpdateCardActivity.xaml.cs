using System.Windows;
using System.Windows.Input;

namespace PE_AtividadesDiárias.UI
{
    /// <summary>
    /// Lógica interna para UpdateCardActivity.xaml
    /// </summary>
    public partial class UpdateCardActivity : Window
    {
        public string Description { get; private set; } = string.Empty;
        public TimeSpan time { get; private set; } = TimeSpan.Zero;

        public bool updateComponent = false;

        public UpdateCardActivity()
        {
            InitializeComponent();
        }

        public UpdateCardActivity(string description, TimeSpan time)
        {
            InitializeComponent();

            this.Description = description;
            Textbox_Descrição.Text = this.Description;

            this.time = time;
            TextBox_Time.Text = time.ToString(@"hh\:mm");
        }

        private void border_update_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!ValidateFields())
                return;

            TimeSpan result;

            updateComponent = true;

            Description = Textbox_Descrição.Text;

            if(TextBox_Time.Text.Contains(":"))
                TimeSpan.TryParse(TextBox_Time.Text, out result);
            else
                TimeSpan.TryParse(TextBox_Time.Text.Insert(2, ":"), out result);


            time = result;

            this.Close();
        }

        private void border_cancel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void TextBox_Time_KeyDown(object sender, KeyEventArgs e)
        {
            if(TextBox_Time.Text.Length < 5)
            {
                bool isDigit = (e.Key >= Key.D0 &&
                                e.Key <= Key.D9) ||
                               (e.Key >= Key.NumPad0 &&
                                e.Key <= Key.NumPad9);

                if (!isDigit)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_Time_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBox_Time.Text.Replace(":", "").Length == 2)
            {
                TextBox_Time.KeyDown -= TextBox_Time_KeyDown;

                TextBox_Time.Text = TextBox_Time.Text.Replace(":", "");
                TextBox_Time.Text += ":";
                
                TextBox_Time.KeyDown += TextBox_Time_KeyDown;

                TextBox_Time.CaretIndex = 3;
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            string description = Textbox_Descrição.Text.Trim();
            string time = TextBox_Time.Text.Trim();

            if (string.IsNullOrEmpty(description))
            {
                isValid = false;
                return isValid;
            }

            if (string.IsNullOrEmpty(time))
            {
                isValid = false;
                return isValid;
            }

            if (time.Length < 4)
            {
                isValid = false;
                return isValid;
            }

            if (!TimeSpan.TryParse(time, out TimeSpan Result))
            {
                isValid = false;
                return isValid;
            }

            return isValid;
        }
    }
}
