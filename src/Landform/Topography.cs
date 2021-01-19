using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Revit.GeometryConversion;
using RevitServices.Transactions;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace Landform
{
    public class Topography
    {
        private Topography()
        {
        }
        public static List<Point> GetPoints(Revit.Elements.Topography topography)
        {
            var points = topography.Points;
            return points;
        }
        public static Revit.Elements.Topography DeletePoints(Revit.Elements.Topography topography, List<Point> pointsToRemove)
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

        public static Revit.Elements.Topography AddPoints(Revit.Elements.Topography topography, List<Point> pointsToAdd)
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

        public static Revit.Elements.Topography MovePoints(Revit.Elements.Topography topography, List<Point> pointsToMove, Vector vectorDelta)
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
