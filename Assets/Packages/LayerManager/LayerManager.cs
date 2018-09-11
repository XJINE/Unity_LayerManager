using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public static class LayerManager
{
    // WARNING:
    // LayerManegr works wrong if layer settings are controlled without LayerManager.

    #region Field

    // NOTE:
    // Unity prepares 32 layers and the 0 ~ 7 layers are reserved in default.

    public const int MaxLayerIndex = 31;

    public const int MaxPreReservedLayerIndex = 7;

    private static bool isInitialized;

    private static readonly Dictionary<int, string> layerDictionary;

    #endregion Field

    #region Property

    public static bool IsInitialized
    {
        get { return LayerManager.isInitialized; }
    }

    public static ReadOnlyDictionary<int, string> LayerDictionary 
    {
        get;
        private set;
    }

    #endregion Property

    #region Constructor

    static LayerManager()
    {
        LayerManager.layerDictionary
        = new Dictionary<int, string>(LayerManager.MaxLayerIndex + 1);
        
        LayerManager.LayerDictionary
        = new ReadOnlyDictionary<int, string>(LayerManager.layerDictionary);
        
        InitializeLayerDictionary();

        LayerManager.isInitialized = true;
    }

    #endregion Constructor

    #region Method

    private static void InitializeLayerDictionary()
    {
        LayerManager.layerDictionary.Clear();

        for (int i = 0; i < LayerManager.MaxLayerIndex; i++)
        {
            // CAUTION:
            // If layer has not any name, following code shows "" (in Unity 5.4.x).

            string layerName = LayerMask.LayerToName(i);

            if (string.IsNullOrEmpty(layerName))
            {
                LayerManager.layerDictionary.Add(i, null);
            }
            else
            {
                LayerManager.layerDictionary.Add(i, layerName);
            }
        }
    }

    public static int AddNewLayer(string layerName, int layerIndex = -1)
    {
        if (layerIndex < 0) 
        {
            layerIndex = GetMinUnusedLayerIndex();
        }

        bool reservedLayerIndex   = layerIndex <= LayerManager.MaxPreReservedLayerIndex;
        bool registeredLayerIndex = LayerManager.layerDictionary[layerIndex] != null;
        bool registeredLayerName  = LayerManager.layerDictionary.Values.Contains<string>(layerName);

        if (reservedLayerIndex || registeredLayerIndex || registeredLayerName)
        {
            return -1;
        }

        LayerManager.layerDictionary[layerIndex] = layerName;

        return layerIndex;
    }

    public static string GetLayerName(int layerIndex)
    {
        return LayerManager.layerDictionary[layerIndex];
    }

    public static int GetLayerIndex(string layerName)
    {
        for (int i = 0; i < LayerManager.MaxLayerIndex; i++)
        {
            if (LayerManager.layerDictionary[i] == layerName)
            {
                return i;
            }
        }

        return -1;
    }

    public static int GetMinUnusedLayerIndex()
    {
        // NOTE:
        // Return -1 when all layer is used.

        for (int i = LayerManager.MaxPreReservedLayerIndex + 1; i < LayerManager.MaxLayerIndex; i++)
        {
            string layerName = LayerManager.layerDictionary[i];

            if (layerName == null)
            {
                return i;
            }
        }

        return -1;
    }

    // NOTE:
    // NameToLayer & LayerToName are implemented to make LayerManager like LayerMask(Unity standard).

    public static int NameToLayer(string layerName)
    {
        return GetLayerIndex(layerName);
    }

    public static string LayerToName(int layerIndex)
    {
        return GetLayerName(layerIndex);
    }

    #endregion Method
}