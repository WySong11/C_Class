using System;
using System.Collections.Generic;
using CSVFileManager;   
using JSONFileManager;  

public class GameDataManager
{
    // CSV 파일 경로와 JSON 파일 경로를 상수로 정의합니다.
    private const string CSV_FILE_PATH = "WordList.csv";

    private const string JSON_FILE_PATH = "SaveData.json";

    private static readonly Lazy<GameDataManager> _instance = new(() => new GameDataManager());
    public static GameDataManager Instance => _instance.Value;
    private GameDataManager() { }

    // CSVManager instance for CSV operations
    private readonly CSVFileProcessor _csvManager = CSVFileProcessor.Instance;
    // JSONManager instance for JSON operations
    private readonly JSONFileProcessor _jsonManager = JSONFileProcessor.Instance;

    private List<GameData> _gameDataList = new List<GameData>();
    private List<SaveData> _saveDataList = new List<SaveData>();

    private const int MAX_HP = 5; // 최대 HP를 상수로 정의합니다.

    public int GetMaxHP()
    {
        return MAX_HP;
    }

    public GameData GetGameData(int index)
    {
        // 게임 데이터 리스트가 비어있으면 CSV에서 데이터를 로드합니다.
        if (_gameDataList.Count == 0)
        {
            _gameDataList = LoadGameDataListFromCSV();
        }

        // 주어진 인덱스에 해당하는 게임 데이터를 찾습니다.
        if (index < 0 || index >= _gameDataList.Count)
        {
            return new GameData(); // 인덱스가 범위를 벗어나면 빈 GameData 객체를 반환합니다.
        }
        return _gameDataList[index];
    }

    public List<GameData> LoadGameDataListFromCSV(string filePath = CSV_FILE_PATH)
    {
        string? dirPath = AppDomain.CurrentDomain.BaseDirectory;

        var rows = _csvManager.Read(dirPath + filePath, null, ',');
        _gameDataList.Clear(); // 기존 데이터를 초기화합니다.

        // 첫 번째 행은 헤더이므로 건너뜁니다.
        for (int i = 1; i < rows.Count; i++)
        {
            var row = rows[i];
            if (row.Length >= 4)
            {
                _gameDataList.Add(new GameData
                {
                    Word = row[0],
                    Hints = new List<string> { row[1], row[2], row[3] },
                });
            }
        }
        return _gameDataList;
    }

    // JSON 파일에서 게임 데이터를 저장하는 메서드
    public void SaveSaveDataListToJSON(string filePath = JSON_FILE_PATH)
    {
        string? dirPath = AppDomain.CurrentDomain.BaseDirectory;

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
        string? dirPath = AppDomain.CurrentDomain.BaseDirectory;

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
        Console.WriteLine("Name  PlayCount  Answer");
        Console.WriteLine("-------------------------------------");

        foreach (var savedata in _saveDataList)
        {
            Console.WriteLine($"{savedata.PlayerName,5}  {savedata.PlayCount,-6}  {savedata.Answer,-6}");

        }

        Console.WriteLine("-------------------------------------\n");
    }

    public GameData GetRandomAnswer()
    {
        if (_gameDataList.Count == 0)
        {
            Console.WriteLine("게임 데이터가 없습니다.");
            return new GameData();
        }
        Random random = new Random();
        int index = random.Next(_gameDataList.Count);
        return _gameDataList[index];
    }
}
