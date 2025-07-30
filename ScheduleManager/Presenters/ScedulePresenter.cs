using ScheduleManager.Views;

namespace ScheduleManager.Presenters
{
    internal class ScedulePresenter
    {
        /// <summary>
        /// スケジュール管理プレゼンター
        /// </summary>
        public class SchedulePresenter
        {
            private readonly IScheduleView _view;
            private readonly ScheduleModel _model;
            private readonly ScheduleRepository _repository;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="view">管理ビュー</param>
            /// <param name="model">管理モデル</param>
            /// <param name="repository">管理リポジトリ</param>
            public SchedulePresenter(IScheduleView view, ScheduleModel model, ScheduleRepository repository)
            {
                _view = view;
                _model = model;
                _repository = repository;

                _view.AddTaskClicked += async (s, e) => await AddTaskAsync();
                _view.ResetAllClicked += async (s, e) => await ResetAllAsync();
                _view.DeleteTaskClicked += async (s, e) => await DeleteTaskAsync(e.Date, e.TaskIndex);

                _ = LoadInitialDataAsync();
            }

            /// <summary>
            /// 読み込みんだリポジトリデータをモデルに設定してビューを更新
            /// </summary>
            private async Task LoadInitialDataAsync()
            {
                try
                {
                    var schedules = _repository.LoadAsync();
                    _model.SetSchedules(schedules);
                    _view.DisplaySchedules(_model.GetAllSchedules());
                }
                catch (System.Exception ex)
                {
                    _view.ShowMessage($"スケジュールの読み込みに失敗しました: {ex.Message}", "エラー", true);
                }
            }

            /// <summary>
            /// 新しいタスクをスケジュールに追加
            /// </summary>
            private async Task AddTaskAsync()
            {
                var date = _view.SelectedDate;
                var task = _view.TaskText;

                if (string.IsNullOrWhiteSpace(task))
                {
                    _view.ShowMessage("予定を入力してください。", "入力エラー");
                    return;
                }

                _model.AddSchedule(date, task);
                _view.TaskText = ""; // Clear input
                await SaveAndRefreshViewAsync();
            }

            /// <summary>
            /// 指定タスクを削除
            /// </summary>
            /// <param name="date">タスクを削除する日付</param>
            /// <param name="taskIndex">削除するタスクのインデックス</param>
            private async Task DeleteTaskAsync(string date, int taskIndex)
            {
                _model.RemoveSchedule(date, taskIndex);
                await SaveAndRefreshViewAsync();
            }

            /// <summary>
            /// すべてのタスクをリセット
            /// </summary>
            private async Task ResetAllAsync()
            {
                if (_view.ShowConfirmation("すべての予定を削除してもよろしいですか？", "リセットの確認"))
                {

                }
            }

            /// <summary>
            /// スケジュールデータを保存してビューを更新
            /// </summary>
            private async Task SaveAndRefreshViewAsync()
            {
                try
                {
                    await _repository.SaveAsync(_model.GetAllSchedules());
                    _view.DisplaySchedules(_model.GetAllSchedules());
                }
                catch (System.Exception ex)
                {
                    _view.ShowMessage($"スケジュールの保存に失敗しました: {ex.Message}", "エラー", true);
                }
            }
        }
    }
}
