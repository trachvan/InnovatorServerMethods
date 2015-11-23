

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
//*** referenced files: IOM.dll and InnovatorCore.dll
using Aras.IOM;

namespace PKG_DA18DF07609DD633661CF0D948B59E41
{
    public class ItemMethod : Item
    {
        public ItemMethod(IServerConnection arg)
            : base(arg)
        {
        }

#if EventDataIsAvailable
		public Item methodCode(
			)
		{
			return methodCode( null );
		}

		public Item methodCode(
			 eventData
			)
#else
        public Item methodCode()
#endif
        {
            Aras.Server.Core.CallContext CCO = ((Aras.Server.Core.IOMConnection)serverConnection).CCO;
            Aras.Server.Core.IContextState RequestState = CCO.RequestState;
            //*** Server side code starts here
            if (System.Diagnostics.Debugger.Launch())
            {
                System.Diagnostics.Debugger.Break();
            }

            Innovator inn = this.getInnovator();
           
            Item sourceItem = GetSourceItem(this);
           
            return this;
            //*** Server side code ends here
        }

        private Item GetSourceItem(Item item)
        {
            string sourceId = item.getProperty("source_id");
            Innovator inn = item.getInnovator();
            if (string.IsNullOrEmpty(sourceId))
            {
                Item currentItem = inn.newItem("Member", "get");
                currentItem.setID(item.getAttribute("id"));
                currentItem.setAttribute("select", "source_id");
                item = currentItem.apply();
                sourceId = item.getProperty("source_id");
            }
            Item result = inn.newItem("Identity", "get");
            result.setProperty("select", "classification, id");
            result.setID(sourceId);
            return result.apply();
        }

        private Item GetRelatedItem(Item item)
        {
            string relatedId = item.getProperty("related_id");
            Innovator inn = item.getInnovator();
            Item result = inn.newItem("Identity", "get");
            result.setAttribute("select", "classification");
            result.setID(relatedId);
            return result.apply();
        }

        private bool IsIdentitySystem(Item item)
        {
            string classification = item.getProperty("classification");
            Innovator inn = item.getInnovator();
            if (string.IsNullOrEmpty(classification))
            {
                Item currentItem = inn.newItem("Identity", "get");
                currentItem.setID(item.getAttribute("id"));
                currentItem.setAttribute("select", "classification");
                item = currentItem.apply();
                classification = item.getProperty("classification");
            }
            if (classification == "System")
            {
                return true;
            }
            else return false;
        }

        private bool IsIdentityTeam(Item item)
        {
            string classification = item.getProperty("classification");
            Innovator inn = item.getInnovator();
            if (string.IsNullOrEmpty(classification))
            {
                Item currentItem = inn.newItem("Identity", "get");
                currentItem.setID(item.getAttribute("id"));
                currentItem.setAttribute("select", "classification");
                item = currentItem.apply();
                classification = item.getProperty("classification");
            }
            if (classification == "Team")
            {
                return true;
            }
            else return false;

        }
    }

    public class CLS_DA18DF07609DD633661CF0D948B59E41 : Aras.Server.Core.IInnovatorServerMethod
    {
        public void FNCMethod(
            IServerConnection InnovatorServerASP,
            XmlDocument inDom,
#if EventDataIsAvailable
			 eventData,
#endif
 XmlDocument outDom
            )
        {
            ItemMethod inItem = null;
            Item outItem = null;
            inItem = new ItemMethod(InnovatorServerASP);
            inItem.dom = inDom;
            XmlNodeList nodes = inDom.SelectNodes("//Item[not(ancestor::node()[local-name()='Item'])]");
            if (nodes.Count == 1)
                inItem.node = (XmlElement)nodes[0];
            else
            {
                inItem.node = null;
                inItem.nodeList = nodes;
            }

            outItem = inItem.methodCode(
#if EventDataIsAvailable
				eventData
#endif
);
            if (outItem != null)
            {
                outDom.ReplaceChild(outDom.ImportNode(outItem.dom.DocumentElement, true), outDom.FirstChild);
            }
        }
    }
}

