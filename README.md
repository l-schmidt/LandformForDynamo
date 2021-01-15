# LandformForDynamo
A collection of Dynamo nodes to assist with site and landscape modeling in Revit.

### Change Log
------
### 2020.11.18
- Changes:
  - Migration to 2.x
  - Minor cleanup of various nodes due to migration to 2.x
  - All nodes checked against Revit 2021.1 / Dynamo 2.6
- Known Issues:
  - Element.TopBasePolysurface
  - NurbsSurface.ByNurbsCurves
  - Point.GridTriangle
  - Point.GridTriangleContain
  - Surfaces.TopBasePolysurface
  - Topography.ColorByElevation
- Deprecated:
  - Curves.Reorder - use archi-lab Group Curves
  - List.RemoveEmpty - use archi-lab Clear List
  - Math.RandomIntegerMin - use Clockwork RandomList.AsIntegers
  - SlabShape.AdjacentPoints - replaced with SlabShape.AddAdjacentPoints
  - SlabeShape.MatchPoints - replaced with SlabShape.AddAdjacentPoints
