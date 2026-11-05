using System.Collections.Generic;
using UnityEngine;

public static class RandomClass
{
    public static List<int> random(List<int> randomNums,int ExtractNum)
    {
        if (ExtractNum > randomNums.Count)
        {
            Debug.LogError("ランダムで排出する値総数より、抽出する値総数の方が大きくなっています");
            return null;
        }
        
        List<int> _randomNums = randomNums;
        for (int i = 0; i < _randomNums.Count; i++)
        {
                int Random = UnityEngine.Random.Range(0, i + 1);

                (_randomNums[i], _randomNums[Random]) = (_randomNums[Random], _randomNums[i]); 
        }

        List<int> ExtractNumbers = new List<int>();
        for (int i = 0; i < ExtractNum; i++)
        { 
            ExtractNumbers.Add(_randomNums[i]);
        }

        return ExtractNumbers;
    }
}
