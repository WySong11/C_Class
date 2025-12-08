using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DiceGameUseTimer
{
    /// <summary>
    /// CSV 에서 읽어온 한 캐릭터 설정을 담는 구조체
    /// </summary>
    public class CharacterConfig
    {
        public ClassType ClassType;
        public CharacterStats Stats;
        public BaseSkill Skill;
    }

    /// <summary>
    /// game_data.csv 를 읽어서 캐릭터 설정을 로드하는 클래스
    /// </summary>
    public static class LoadData
    {
        // 기본 CSV 파일 이름
        private const string DefaultCsvPath = "game_data.csv";

        /// <summary>
        /// CSV 를 읽어서 ClassType → CharacterConfig 딕셔너리로 반환
        /// </summary>
        public static Dictionary<ClassType, CharacterConfig> Load(string csvPath = DefaultCsvPath)
        {
            var result = new Dictionary<ClassType, CharacterConfig>();

            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException("game_data.csv 파일을 찾을 수 없습니다.", csvPath);
            }

            // 모든 줄 읽기
            string[] lines = File.ReadAllLines(csvPath);

            // 첫 줄은 헤더라서 건너뜀
            if (lines.Length <= 1)
                return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // 간단한 CSV 라고 가정하고 콤마로 Split
                string[] tokens = line.Split(',');
                if (tokens.Length < 9)
                    continue;    // 칼럼 개수가 안 맞으면 스킵

                // 0: Class
                if (!Enum.TryParse(tokens[0].Trim(), true, out ClassType classType))
                {
                    // ClassType 파싱 실패하면 스킵
                    continue;
                }

                // 1: Health
                int health = ParseInt(tokens[1], 1000);
                // 2: AttackPower
                int attackPower = ParseInt(tokens[2], 100);
                // 3: AttackSpeed
                double attackSpeed = ParseDouble(tokens[3], 1000.0);

                // 4: Skill
                string skillName = tokens[4].Trim();

                // 5: Condition
                BaseSkill.UseSkillCondition condition = BaseSkill.UseSkillCondition.Always;
                Enum.TryParse(tokens[5].Trim(), true, out condition);

                // 6: ConditionValue
                // CSV 에서 0.5 같이 들어와도 int 로 캐스팅해서 사용
                // AttackCount 조건이면 정수 공격 횟수로 쓰는 게 안전함
                int conditionValue = (int)ParseDouble(tokens[6], 0.0);

                // 7: CooldownSeconds
                float cooldownSeconds = (float)ParseDouble(tokens[7], 0.0);

                // 8: SkillMultiplier
                float skillMultiplier = (float)ParseDouble(tokens[8], 1.0);

                // 능력치 / 스킬 객체 생성
                var stats = new CharacterStats(health, health, attackPower, attackSpeed);

                var skill = new BaseSkill
                {
                    SkillName = skillName,
                    Condition = condition,
                    ConditionValue = conditionValue,
                    CooldownSeconds = cooldownSeconds,
                    SkillMultiplier = skillMultiplier
                };

                // 딕셔너리에 저장
                var config = new CharacterConfig
                {
                    ClassType = classType,
                    Stats = stats,
                    Skill = skill
                };

                // 같은 ClassType 이 한 번 더 나오면 덮어쓰기
                result[classType] = config;
            }

            return result;
        }

        /// <summary>
        /// 로드된 CharacterConfig 를 BaseCharacter 에 적용하는 헬퍼
        /// </summary>
        public static void ApplyToCharacter(BaseCharacter character, CharacterConfig config)
        {
            if (character == null || config == null)
                return;

            character.classtype = config.ClassType;
            character.Stats = new CharacterStats(
                config.Stats.Health,
                config.Stats.MaxHealth,
                config.Stats.AttackPower,
                config.Stats.AttackSpeed);

            character.skill = config.Skill;
        }

        /// <summary>
        /// 문자열을 int 로 파싱
        /// 실패하면 기본값 반환
        /// </summary>
        private static int ParseInt(string s, int defaultValue)
        {
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                return value;

            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double d))
                return (int)d;

            return defaultValue;
        }

        /// <summary>
        /// 문자열을 double 로 파싱
        /// 실패하면 기본값 반환
        /// </summary>
        private static double ParseDouble(string s, double defaultValue)
        {
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double d))
                return d;

            return defaultValue;
        }
    }
}
