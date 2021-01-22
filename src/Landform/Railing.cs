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
    public class Railing
    {
        private Railing()
        {
        }

        public static void ChangeHost(Element railing, Element host)
        {
            var id = host.Id;
            var doc = railing.Document;

            var internalRailing = railing as Autodesk.Revit.DB.Architecture.Railing;

            Transaction trans = new Transaction(doc);
            trans.Start("Change Host");

            internalRailing.HostId = id;

            trans.Commit();

        }
    }
}
