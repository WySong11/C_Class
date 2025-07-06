using System;
using System.Collections.Generic;

public class GameDataManager
{
    // CSV 파일 경로와 JSON 파일 경로를 상수로 정의합니다.
    private const string CSV_DIR_PATH = "\\Data\\";
    private const string CSV_FILE_PATH = "LevelData.csv";

    private const string JSON_DIR_PATH = "\\Save\\";
    private const string JSON_FILE_PATH = "SaveData.json";

    private static readonly Lazy<GameDataManager> _instance = new(() => new GameDataManager());
    public static GameDataManager Instance => _instance.Value;
    private GameDataManager() { }

    // CSVManager instance for CSV operations
    private readonly CSVManager _csvManager = CSVManager.Instance;
    // JSONManager instance for JSON operations
    private readonly JSONManager _jsonManager = JSONManager.Instance;

    private List<GameData> _gameDataList = new List<GameData>();
    private List<SaveData> _saveDataList = new List<SaveData>();

    public GameData GetGameData(int level)
    {
        // 게임 데이터 리스트가 비어있으면 CSV에서 데이터를 로드합니다.
        if (_gameDataList.Count == 0)
        {
            _gameDataList = LoadGameDataListFromCSV();
        }

        // 주어진 레벨에 해당하는 게임 데이터를 찾습니다.
        return _gameDataList.Find(data => data.level == level) ?? new GameData();
    }

    public List<GameData> LoadGameDataListFromCSV(string filePath = CSV_FILE_PATH)
    {
        string? dirPath = AppDomain.CurrentDomain.BaseDirectory + CSV_DIR_PATH;

        var rows = _csvManager.Read(dirPath + filePath, null, ',');
        var gameDataList = new List<GameData>();

        // 첫 번째 행은 헤더이므로 건너뜁니다.
        for (int i = 1; i < rows.Count; i++)
        {
            var row = rows[i];
            if (row.Length >= 3 &&
                int.TryParse(row[0], out int level) &&
                int.TryParse(row[1], out int hp) &&
                int.TryParse(row[2], out int atk))
            {
                gameDataList.Add(new GameData(level, hp, atk));
            }
        }
        return gameDataList;
    }

    // JSON 파일에서 게임 데이터를 저장하는 메서드
    public void SaveSaveDataListToJSON(string filePath = JSON_FILE_PATH)
    {
        string? dirPath = AppDomain.CurrentDomain.BaseDirectory + JSON_DIR_PATH;

        // 디렉토리가 존재하지 않으면 생성
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        _jsonManager.Write(dirPath + filePath, _saveDataList);
    }

    // JSON 파일에서 게임 데이터를 로드하는 메서드
    public void LoadSaveDataListFromJSON(string filePath = JSON_FILE_PATH)
    {
        string? dirPath = AppDomain.CurrentDomain.BaseDirectory + JSON_DIR_PATH;

        var list = _jsonManager.Read<List<SaveData>>(dirPath + filePath);
        _saveDataList = list ?? new List<SaveData>();
    }

    // 게임 데이터를 추가하는 메서드
    public void AddSaveData(SaveData saveData)
    {
        _saveDataList.Add(saveData);
        SaveSaveDataListToJSON(); // 변경된 데이터를 JSON 파일에 저장합니다.
    }

    public void PrintGameDataList()
    {
        Console.WriteLine("\n-------------------------------------");
        Console.WriteLine("Level  Name    Hp    Attack");
        Console.WriteLine("-------------------------------------");

        /*string name = "Tom";
        Console.WriteLine($"|{name,-6}|"); // 출력: |Tom   |
        Console.WriteLine($"|{name,6}|");  // 출력: |   Tom|*/

        foreach (var savedata in _saveDataList)
        {
            Console.WriteLine($"{savedata.level,5}  {savedata.playerName,-6}  {GetGameData(savedata.level).hp,3}  {GetGameData(savedata.level).atk,5}");

        }

        Console.WriteLine("-------------------------------------\n");
    }
}
