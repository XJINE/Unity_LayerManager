using UnityEngine;

public class LayerManagerSample : MonoBehaviour
{
    protected virtual void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddNewLayer();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetLayerIndex();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShowLayerInfos();
        }
    }

    protected virtual void OnGUI() 
    {
        GUILayout.Label("These results are output in console.");
        GUILayout.Label("Press [A] : Try to add new layer.");
        GUILayout.Label("Press [G] : Try to get existing layer.");
        GUILayout.Label("Press [S] : Show all of layers.");
    }

    protected virtual void AddNewLayer()
    {
        string layerName = "Sample" + Random.Range(0, 3);
        int layerIndex = LayerManager.AddNewLayer(layerName);

        if (layerIndex < 0)
        {
            Debug.Log("Failed to Add New Layer \"" + layerName + "\"");
        }
        else 
        {
            Debug.Log("Add New Layer \"" + layerName + "\" at " + layerIndex);
        }
    }

    protected virtual void GetLayerIndex()
    {
        string layerName = "Sample" + Random.Range(0, 3);
        int layerIndex = LayerManager.GetLayerIndex(layerName);

        if (layerIndex < 0)
        {
            Debug.Log("Failed to Find Layer \"" + layerName + "\"");
        }
        else 
        {
            Debug.Log("Find Layer \"" + layerName + "\" at " + layerIndex);
        }
    }

    protected virtual void ShowLayerInfos()
    {
        Debug.Log("- Layer Infos - ");

        foreach (var key in LayerManager.LayerDictionary.Keys) 
        {
            Debug.Log(string.Format("[{0}] {1}", key, LayerManager.LayerDictionary[key]));
        }
    }
}