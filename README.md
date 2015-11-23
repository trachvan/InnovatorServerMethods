# InnovatorServerMethods

This Visual Studio project contains Innovator's server side template for creating/editing Innovator's server side method. A C# class file is provided:
    MethodTemplate1.cs.

Referenced Files:

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

Embedded Server Method:

This method resides in the MethodTemplate1.cs file:

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
        

