namespace ScheduleManager.Models
{
    /// <summary>
    /// スケジュール項目
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// 予定日付（yyyy-MM-dd）
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 予定内容
        /// </summary>
        public string Task { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="date">予定日付</param>
        /// <param name="task">予定内容</param>
        public Schedule(string date, string task)
        {
            Date = date;
            Task = task;
        }
    }
}
