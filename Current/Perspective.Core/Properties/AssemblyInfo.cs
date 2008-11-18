using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Perspective.Core")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Perspective.Core")]
[assembly: AssemblyCopyright("Copyright ©  2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("2f3d71db-f559-478e-97e0-bd3cb8c6113c")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(Perspective.Core.LibraryInfo.GlobalVersion)]
[assembly: AssemblyFileVersion(Perspective.Core.LibraryInfo.GlobalVersion)]

[assembly: CLSCompliant(true)]

#if DN35
    [assembly:XmlnsDefinition("Dn35", "Perspective.Core")]
#endif

[assembly: XmlnsDefinition(Perspective.Core.LibraryInfo.XmlNamespace, "Perspective.Core")]
[assembly: XmlnsDefinition(Perspective.Core.LibraryInfo.XmlNamespace, "Perspective.Core.Wpf")]
[assembly: XmlnsDefinition(Perspective.Core.LibraryInfo.XmlNamespace, "Perspective.Core.Wpf.Data")]

namespace Perspective.Core
{
    /// <summary>
    /// A class to handle the default assembly attribute values for Perspective.
    /// </summary>
    public sealed class LibraryInfo
    {
        private LibraryInfo() { }

        /// <summary>
        /// The default XML namespace for Perspective.
        /// </summary>
        public const string XmlNamespace = "http://www.codeplex.com/perspective";

        /// <summary>
        /// Perspective global version number.
        /// </summary>
        public const string GlobalVersion = "1.0.1.0";
    }
}
