using UnityEngine;

public class MazeDataGenerator
{

    private float _placementThreshold;

    public MazeDataGenerator()
    {
        _placementThreshold = 0.1f;
    }

    public int[,] FromDimensions(int rows, int columns)
    {
        var data = new int[rows, columns];
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        for (var ri = rowMax; ri >= 0; ri--)
        {
            for (var ci = 0; ci <= columnMax; ci++)
            {
                if (ri == 0 || ci == 0 || ri == rowMax || ci == columnMax)
                {
                    data[ri, ci] = 1;
                }
                else if (ri % 2 == 0 && ci % 2 == 0)
                {
                    if (!(Random.value > _placementThreshold))
                        continue;
                    data[ri, ci] = 1;
                    var a = Random.value < 0.5 ? 0 : Random.value < 0.5 ? -1 : 1;
                    var b = a != 0 ? 0 : Random.value < 0.5 ? -1 : 1;
                    data[ri + a, ci + b] = 1;
                }
            }
        }
        return data;
    }

}