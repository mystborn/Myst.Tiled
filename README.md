# Myst.Tiled
Provides mechanisms for interacting with Tiled maps

# How to Use
Currently all you can do is load xml formatted Tiled maps. Support for loading json maps and saving maps are coming in the future.

To load a map, all you need to do create a new `TiledMap` with the path to the file. The path can be relative or absolute.

```cs
var map = new TiledMap("path/to/map");
```
