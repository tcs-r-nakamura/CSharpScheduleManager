namespace ScheduleManager.Views
{
    /// <summary>
    /// スケジュール管理ビューインターフェース
    /// </summary>
    public interface IScheduleView
    {
        // <summary>
        /// 選択日付
        /// </summary>
        string SelectedDate { get; }

        /// <summary>
        /// 入力タスクのテキスト
        /// </summary>
        string TaskText { get; set; }

        /// <summary>
        /// スケジュール一覧をビューに表示
        /// </summary>
        /// <param name="schedules">表示スケジュール</param>
        void DisplaySchedules(IReadOnlyDictionary<string, List<string>> schedules);

        /// <summary>
        /// メッセージを表示
        /// </summary>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="caption">メッセージボックスのタイトル</param>
        /// <param name="isError">true：エラーメッセージ false：通常メッセージ</param>
        void ShowMessage(string message, string caption, bool isError = false);

        /// <summary>
        /// 確認メッセージを表示
        /// </summary>
        /// <param name="message">表示する確認メッセージ</param>
        /// <param name="caption">メッセージボックスのタイトル</param>
        /// <returns>true：「はい」選択 false：それ以外選択</returns>
        bool ShowConfirmation(string message, string caption);


        /// <summary>
        /// タスク追加ボタンがクリックされたときに発生するイベント
        /// </summary>
        event EventHandler AddTaskClicked;

        /// <summary>
        /// 全リセットボタンがクリックされたときに発生するイベント
        /// </summary>
        event EventHandler ResetAllClicked;

        /// <summary>
        /// タスク削除ボタンがクリックされたときに発生するイベント
        /// </summary>
        event EventHandler<ScheduleSelectionEventArgs> DeleteTaskClicked;

        /// <summary>
        /// ビューを実行
        /// </summary>
        void Run();
    }

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleSelectionEventArgs : EventArgs
    {
        /// <summary>
        /// 選択スケジュール日付
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// 選択タスクインデックス
        /// </summary>
        public int TaskIndex { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="date">選択されたスケジュールの日付</param>
        /// <param name="taskIndex">選択されたタスクのインデックス</param>
        public ScheduleSelectionEventArgs(string date, int taskIndex)
        {
            Date = date;
            TaskIndex = taskIndex;
        }
    }
}
