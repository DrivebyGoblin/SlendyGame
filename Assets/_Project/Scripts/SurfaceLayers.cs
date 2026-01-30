using System.Collections.Generic;

public static class SurfaceLayers
{
    private static readonly Dictionary<LayerType, int> _layerMap = new Dictionary<LayerType, int>
    {
        { LayerType.Grass, 0 },
        { LayerType.Wood, 6 }
    };


    public static int GetLayerValue(LayerType layerType)
    {
        if (_layerMap.TryGetValue(layerType, out int value))
        {
            return value;
        }

        return 0;
    }
}


public enum LayerType
{
    Grass,
    Wood
}
