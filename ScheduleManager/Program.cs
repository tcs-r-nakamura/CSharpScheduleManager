using ScheduleManager.Models;
using ScheduleManager.Repositories;
using ScheduleManager.Views;
using static ScheduleManager.Presenters.ScedulePresenter;

namespace ScheduleManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- Dependency Injection ---
            var view = new MainForm();
            var model = new ScheduleModel();
            // Save data in the same directory as the executable
            var repository = new ScheduleRepository("schedule_data_csharp.json");

            var presenter = new SchedulePresenter(view, model, repository);

            view.Run();
        }
    }
}