using System;
using Dynamo.Graph.Nodes;
using RevitServices.Transactions;

namespace Landform.Revit.Elements
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
        [NodeCategory("Actions")]
        public static string SetHost(global::Revit.Elements.Element railing, global::Revit.Elements.Element host)
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
