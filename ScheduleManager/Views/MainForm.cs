using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleManager.Views
{
    
    public partial class MainForm : Form, IScheduleView
    {

        public event EventHandler AddTaskClicked;

        public event EventHandler ResetAllClicked;

        public event EventHandler<ScheduleSelectionEventArgs> DeleteTaskClicked;


        public MainForm()
        {
            InitializeComponent();
            this.addButton.Click += (s, e) => AddTaskClicked?.Invoke(this, EventArgs.Empty);
            this.resetButton.Click += (s, e) => ResetAllClicked?.Invoke(this, EventArgs.Empty);
            this.deleteButton.Click += OnDeleteButtonClicked;
        }


        public string SelectedDate => datePicker.Value.ToString("yyyy-MM-dd");

        public string TaskText { get => taskTextBox.Text; set => taskTextBox.Text = value; }

        public void DisplaySchedules(IReadOnlyDictionary<string, List<string>> schedules)
        {
            scheduleListBox.Items.Clear();
            foreach (var entry in schedules)
            {
                scheduleListBox.Items.Add($"--- {entry.Key} ---");
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    scheduleListBox.Items.Add($"{i + 1}. {entry.Value[i]}");
                }
                scheduleListBox.Items.Add("");
            }
        }
        public void Run()
        {
            Application.Run(this);
        }

        public void ShowMessage(string message, string caption, bool isError = false)
        {
            var icon = isError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }

        public bool ShowConfirmation(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (scheduleListBox.SelectedItem is string selectedItem && !string.IsNullOrWhiteSpace(selectedItem))
            {
                string date = FindDateOfSelectedItem(scheduleListBox.SelectedIndex);
                if (date != null && char.IsDigit(selectedItem[0]))
                {
                    int taskIndex = int.Parse(selectedItem.Split('.')[0]) - 1;
                    DeleteTaskClicked?.Invoke(this, new ScheduleSelectionEventArgs(date, taskIndex));
                }
            }
        }

        private string FindDateOfSelectedItem(int selectedIndex)
        {
            for (int i = selectedIndex; i >= 0; i--)
            {
                if (scheduleListBox.Items[i] is string item && item.StartsWith("---"))
                {
                    return item.Trim('-', ' ');
                }
            }
            return null;
        }
    }
}
