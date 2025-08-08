using System.Text.Json;

namespace ScheduleManager.Repositories
{
    /// <summary>
    /// スケジュールリポジトリ
    /// </summary>
    public class ScheduleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">スケジュールデータを保存/読み込みするファイルのパス</param>
        public ScheduleRepository(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// スケジュールデータをJSONファイルに保存
        /// </summary>
        /// <param name="schedules">保存するスケジュールデータ</param>
        /// <returns>操作タスク</returns>
        public async Task SaveAsync(IReadOnlyDictionary<string, List<string>> schedules)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(schedules, options);
           　await File.WriteAllTextAsync(_filePath, jsonString);
        }

        /// <summary>
        /// JSONファイルからスケジュールデータを読み込み
        /// </summary>
        /// <returns>読み込みスケジュールデータ</returns>
        public async Task<Dictionary<string, List<string>>> LoadAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new Dictionary<string, List<string>>();
            }

            var jsonString = await File.ReadAllTextAsync(_filePath);
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new Dictionary<string, List<string>>();
            }

            var schedules = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);
            return schedules ?? new Dictionary<string, List<string>>();
        }
    }
}
