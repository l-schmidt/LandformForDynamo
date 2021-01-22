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
using Dynamo.Graph.Nodes;
using Revit.GeometryConversion;
using RevitServices.Transactions;

namespace Landform
{
    /// <summary>
    /// Wrapper Class for Railing
    /// </summary>
    public class Railing
    {
        private Railing()
        {
        }

        /// <summary>
        /// Set or change the host for a railing.
        /// </summary>
        /// <param name="railing">The railing(s)</param>
        /// <param name="host">The host(s)</param>
        /// <returns name="result">The result</returns>
        public static string SetHost(Revit.Elements.Element railing, Revit.Elements.Element host)
        {
            var id = host.InternalElement.Id;

            var internalRailing = railing.InternalElement as Autodesk.Revit.DB.Architecture.Railing;

            var doc = internalRailing.Document;

            string result;

            try
            {
                TransactionManager.Instance.EnsureInTransaction((doc));

                internalRailing.HostId = id;

                TransactionManager.Instance.TransactionTaskDone();
                result = "Success";
            }
            catch (Exception)
            {
                result = "Transaction Failed";
            }

            return result;

        }
    }
}
