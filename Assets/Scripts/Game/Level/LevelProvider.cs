using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.Bonuses;
using Game.Managers;
using UnityEngine;

namespace Game.Level
{
    public static class LevelProvider
    {

        public static LevelData LoadLevel(string name)
        {
            var levelFile = Resources.Load<TextAsset> ("Levels/" + name);
            
            if (!levelFile)
            {
                Debug.LogError ($"Level {name} not founded");
                return null;
            }
            
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream (levelFile.bytes);
            var data = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            
            return data;
        }

        public static void SaveLevel(string name, LevelData level)
        {
            var formatter = new BinaryFormatter ();
            var path = Application.dataPath + "/Resources/Levels/" + name + ".bytes";
            var stream = new FileStream (path, FileMode.Create);
            
            formatter.Serialize (stream, level);
            stream.Close ();
        }
        
        public static LevelData CreateRandomLevel()
        {
            var gManager = GameManager.Instance;
            
            var possibleBlockCount = Random.Range(gManager.MinBlocksInLevel, gManager.MaxBlocksInLevel);
            var maxBlocksInRow = gManager.MaxBlocksInRow;
            var rowCount = possibleBlockCount > maxBlocksInRow? possibleBlockCount/maxBlocksInRow : 1;

            var levelData = new LevelData {blockMap = new BaseBlockConfig[rowCount, maxBlocksInRow]};

            for (var i = 0; i < rowCount; i++)
            {
                var positions = RandomPositions(1, maxBlocksInRow);
                for (var j = 0; j < maxBlocksInRow; j++)
                {
                    if (positions.Contains(j))
                    {
                        var blockHp = Random.Range(1, gManager.MaxBlockHp + 1);
                        var bonus = BonusProvider.TryGetRandomBonus(0.3f);
                        levelData.blockMap[i, j] = new BaseBlockConfig(blockHp, bonus);
                    }
                    else
                    {
                        levelData.blockMap[i, j] = null;
                    }
                }
            }

            return levelData;
        }

        private static List<int> RandomPositions(int minPositions, int maxPositions)
        {
            var positions = new List<int>();
            for (var i = 0; i < maxPositions; i++)
                positions.Add(i);
        
            var posToDeleteCount = maxPositions - Random.Range(minPositions, maxPositions);
            for (var i = 0; i < posToDeleteCount; i++)
                positions.Remove(Random.Range(0, positions.Count));

            return positions;
        }
    }
}