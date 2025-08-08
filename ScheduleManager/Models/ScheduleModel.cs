namespace ScheduleManager.Models
{
    /// <summary>
    /// スケジュールモデル
    /// </summary>
    public class ScheduleModel
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, List<string>> _schedules = new Dictionary<string, List<string>>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="date">タスクを追加する日付</param>
        /// <param name="task">追加するタスクの内容</param>
        public void AddSchedule(string date, string task)
        {
            if (!_schedules.ContainsKey(date))
            {
                _schedules[date] = new List<string>();
            }
            _schedules[date].Add(task);
        }

        /// <summary>
        /// 指定された日付のタスクを削除
        /// </summary>
        /// <param name="date">タスクを削除する日付</param>
        /// <param name="taskIndex">削除するタスクのインデックス</param>
        public void RemoveSchedule(string date, int taskIndex)
        {
            if (_schedules.ContainsKey(date))
            {
                var tasks = _schedules[date];
                if (taskIndex >= 0 && taskIndex < tasks.Count)
                {
                    tasks.RemoveAt(taskIndex);
                    if (tasks.Count == 0)
                    {
                        _schedules.Remove(date);
                    }
                }
            }
        }

        /// <summary>
        /// すべてのスケジュールを削除
        /// </summary>
        public void ClearAllSchedules()
        {
            _schedules.Clear();
        }

        /// <summary>
        /// すべてのスケジュールデータを取得
        /// </summary>
        /// <returns>スケジュールデータ</returns>
        public IReadOnlyDictionary<string, List<string>> GetAllSchedules()
        {
            return _schedules;
        }

        /// <summary>
        /// スケジュールデータを上書き設定
        /// </summary>
        /// <param name="newSchedules">上書きスケジュールデータ</param>
        public void SetSchedules(Dictionary<string, List<string>> newSchedules)
        {
            _schedules.Clear();
            if (newSchedules != null)
            {
                foreach (var entry in newSchedules)
                {
                    _schedules[entry.Key] = entry.Value;
                }
            }
        }
    }
}
