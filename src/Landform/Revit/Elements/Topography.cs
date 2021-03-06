﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.DesignScript.Geometry;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Dynamo.Graph.Nodes;
using Revit.GeometryConversion;
using RevitServices.Transactions;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace Landform.Revit.Elements
{
    /// <summary>
    /// Wrapper Class for Topography
    /// </summary>
    public class Topography
    {
        private Topography()
        {
        }
        /// <summary>
        /// Check if list of points are distinct in XY location.
        /// </summary>
        /// <param name="pointsToCheck">List of points.</param>
        /// <returns></returns>
        [NodeCategory("Query")]
        public static bool DistinctXY(List<Point> pointsToCheck)
        {
            return TopographySurface.ArePointsDistinct(pointsToCheck.ToXyzs());
        }
        
        /// <summary>
        /// Get the boundary points from a toposurface.
        /// </summary>
        /// <param name="topography">The toposurface.</param>
        /// <returns name="boundaryPoints">The boundary points.</returns>
        [NodeCategory("Query")]
        public static List<Point> GetBoundaryPoints(global::Revit.Elements.Topography topography)
        {
            //cast the Revit.Elements.Topography to the Autodesk.Revit.DB.TopographySurface version
            var internalTopography = topography.InternalElement as TopographySurface;

            //get the boundary points, convert to list and cast as Dynamo points
            return internalTopography.GetBoundaryPoints().ToList().ToPoints();
        }
        /// <summary>
        /// Get the interior points from a toposurface.
        /// </summary>
        /// <param name="topography">The toposurface.</param>
        /// <returns name="interiorPoints">The interior points.</returns>
        [NodeCategory("Query")]
        public static List<Point> GetInteriorPoints(global::Revit.Elements.Topography topography)
        {
            //cast the Revit.Elements.Topography to the Autodesk.Revit.DB.TopographySurface version
            var internalTopography = topography.InternalElement as TopographySurface;

            //get the interior points, convert to list and cast as Dynamo points
            return internalTopography.GetInteriorPoints().ToList().ToPoints();
        }
        /// <summary>
        /// Removes points from an existing toposurface. CAUTION: Must be run in Manual Mode.
        /// </summary>
        /// <param name="topography">The toposurface.</param>
        /// <param name="pointsToRemove">The points to remove.</param>
        /// <returns name="topography">The toposurface.</returns>
        [NodeCategory("Actions")]
        public static global::Revit.Elements.Topography DeletePoints(global::Revit.Elements.Topography topography, List<Point> pointsToRemove)
        {
            //cast the Revit.Elements.Topograph to the Autodesk.Revit.DB.TopographySurface version
            var internalTopography = topography.InternalElement as TopographySurface;
            //get the document related to the topography
            //TIP: (this method is useful because it retrieves the related document rather than just the current one)
            var doc = internalTopography.Document;

            /*TIP: you can also get the current active document with this built-in dynamo method
            var doc = DocumentManager.Instance.CurrentDBDocument*/

            //force close the dynamo transaction
            TransactionManager.Instance.ForceCloseTransaction();

            //start a topography edit scope
            TopographyEditScope editScope = new TopographyEditScope(doc, "Landform-Delete Points");
            editScope.Start(internalTopography.Id);

            //create and start a transaction to make a change to the topography
            Transaction transaction = new Transaction(doc);
            transaction.Start("Start deleting points.");

            //delete points - ToXyzs() will convert Dynamo points to Autodesk.Revit.DB.Point equivalents
            internalTopography.DeletePoints(pointsToRemove.ToXyzs());

            //finish and commit the transaction
            transaction.Commit();

            //commit the edit
            editScope.Commit(new TopographyEditFailuresPreprocessorSimple());

            return topography;
        }

        /// <summary>
        /// Adds points to an existing toposurface. CAUTION: Must be run in Manual Mode.
        /// </summary>
        /// <param name="topography">The toposurface.</param>
        /// <param name="pointsToAdd">The points to remove.</param>
        /// <returns name="topography">The toposurface.</returns>
        [NodeCategory("Actions")]
        public static global::Revit.Elements.Topography AddPoints(global::Revit.Elements.Topography topography, List<Point> pointsToAdd)
        {
            //cast the Revit.Elements.Topograph to the Autodesk.Revit.DB.TopographySurface version
            var internalTopography = topography.InternalElement as TopographySurface;
            //get the document related to the topography
            //TIP: (this method is useful because it retrieves the related document rather than just the current one)
            var doc = internalTopography.Document;

            /*TIP: you can also get the current active document with this built-in dynamo method
            var doc = DocumentManager.Instance.CurrentDBDocument*/

            //force close the dynamo transaction
            TransactionManager.Instance.ForceCloseTransaction();

            //start a topography edit scope
            TopographyEditScope editScope = new TopographyEditScope(doc, "Landform-Add Points");
            editScope.Start(internalTopography.Id);

            //create and start a transaction to make a change to the topography
            Transaction transaction = new Transaction(doc);
            transaction.Start("Start adding points.");

            //add points - ToXyzs() will convert Dynamo points to Autodesk.Revit.DB.Point equivalents
            internalTopography.AddPoints(pointsToAdd.ToXyzs());

            //finish and commit the transaction
            transaction.Commit();

            //commit the edit
            editScope.Commit(new TopographyEditFailuresPreprocessorSimple());

            return topography;
        }

        /// <summary>
        /// Moves points within an existing toposurface. The shift delta will be the specified X,Y, and Z values within the vector. CAUTION: Must be run in Manual Mode.
        /// </summary>
        /// <param name="topography">The toposurface.</param>
        /// <param name="pointsToMove">The points to shift.</param>
        /// <param name="vectorDelta">The vector to shift the points.</param>
        /// <returns name="topography">The toposurface.</returns>
        public static global::Revit.Elements.Topography MovePoints(global::Revit.Elements.Topography topography, List<Point> pointsToMove, Vector vectorDelta)
        {
            //cast the Revit.Elements.Topograph to the Autodesk.Revit.DB.TopographySurface version
            var internalTopography = topography.InternalElement as TopographySurface;
            //get the document related to the topography
            //TIP: (this method is useful because it retrieves the related document rather than just the current one)
            var doc = internalTopography.Document;

            //force close the dynamo transaction
            TransactionManager.Instance.ForceCloseTransaction();

            //start a topography edit scope
            TopographyEditScope editScope = new TopographyEditScope(doc, "Landform-Move Points");
            editScope.Start(internalTopography.Id);

            //create and start a transaction to make a change to the topography
            Transaction transaction = new Transaction(doc);
            transaction.Start("Start moving points.");

            //move points - ToXyzs() will convert Dynamo points to Autodesk.Revit.DB.Point equivalents
            internalTopography.MovePoints(pointsToMove.ToXyzs(), vectorDelta.ToRevitType());

            //finish and commit the transaction
            transaction.Commit();

            //commit the edit
            editScope.Commit(new TopographyEditFailuresPreprocessorSimple());

            return topography;
        }
    }

    #region Helpers
    class TopographyEditFailuresPreprocessorSimple : IFailuresPreprocessor
    {
        // For debugging
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            try
            {
                return FailureProcessingResult.Continue;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error", $"The edit failed. Here is the error message from Revit: {e.Message}");
                return FailureProcessingResult.ProceedWithRollBack;
            }
        }
    }
    #endregion
}
