# Unity_LayerManager

Assist to manage layer index and the name via script.

## Import to Your Project

You can import this asset from UnityPackage.

- [LayerManager.unitypackage](https://github.com/XJINE/Unity_LayerManager/blob/master/LayerManager.unitypackage)

## How to Use

Register layer index and the name when set the layer to any objects in dynamically.

NOTE:

Unity prepares ``32`` layers and the ``0`` ~ ``7`` layer are reserved in default.

### GetMinUnusedLayerIndex

``GetMinUnusedLayerIndex`` function returns a index which is unused.
When all of the layer index is used, it will return ``-1``.

### AddNewLayer

``AddNewLayer`` function register new index and the name to ``LayerManager``.
This function enable to set any layer index as you like, but if omitted, the layer index will be unused min value.

When all of the layer index or just you set is already used, this function will failed and return ``-1``.
And when you set existing name, it will also failed.

### GetLayerName / GetLayerIndex

``GetLayerName`` function returns a name with index. If the index is never used, it will return ``null``.

``GetLayerIndex`` function returns a index with name, and returns ``null`` when the name is never used.

### LayerToName / NameToLayer

``LayerToName`` and ``NameToLayer`` functions are same with ``GetLayerName`` and ``GetLayerIndex``.

## Limitation

### Compatibility

There is no compatibility with default Unity functions such as ``LayerMask.NameToLayer`` or any others.
So the layer which handled with ``LayerManager`` can't be find with these functions.

### Remove Layer

There is no function to remove registered layer because some objects may belong the layer when it is removed.
And then, there is no way to find and remove these objects.
