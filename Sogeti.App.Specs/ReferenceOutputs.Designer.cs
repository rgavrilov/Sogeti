﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sogeti.App.Specs {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ReferenceOutputs {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ReferenceOutputs() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Sogeti.App.Specs.ReferenceOutputs", typeof(ReferenceOutputs).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 3 fname3 lname3 http://link-3 2003-12-31 2004-12-31 party1 portrait3 thumbnail3 New York
        ///Total count: 4..
        /// </summary>
        internal static string DefaultFilterOutput {
            get {
                return ResourceManager.GetString("DefaultFilterOutput", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File missingFile.csv not found in the file system..
        /// </summary>
        internal static string FileNotFoundOutput {
            get {
                return ResourceManager.GetString("FileNotFoundOutput", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Presidency ,President ,Wikipedia Entry,Took office ,Left office ,Party ,Portrait,Thumbnail,Home State
        ///1,George Washington,http://en.wikipedia.org/wiki/George_Washington,1789-04-30,1797-03-04,Independent ,GeorgeWashington.jpg,thmb_GeorgeWashington.jpg,Virginia
        ///2,John Adams,http://en.wikipedia.org/wiki/John_Adams,1797-03-04,1801-03-04,Federalist ,JohnAdams.jpg,thmb_JohnAdams.jpg,Massachusetts
        ///3,Thomas Jefferson,http://en.wikipedia.org/wiki/Thomas_Jefferson,1801-03-04,1809-03-04,Democratic-Republican ,Thoma [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Presidents {
            get {
                return ResourceManager.GetString("Presidents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 8  Martin Van Buren            http://en.wikipedia.org/wiki/Martin_Van_Buren      1837-03-04 1841-03-04 Democratic MartinVanBuren.jpg     thmb_MartinVanBuren.gif     New York
        ///13 Millard Fillmore            http://en.wikipedia.org/wiki/Millard_Fillmore      1850-07-09 1853-03-04 Whig       MillardFillmore.jpg    thmb_MillardFillmore.png    New York
        ///21 Chester A. Arthur           http://en.wikipedia.org/wiki/Chester_A._Arthur     1881-09-19 1885-03-04 Republican ChesterAArthur.gif     thmb_ChesterAArthur.gif  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RealOutput_Text {
            get {
                return ResourceManager.GetString("RealOutput_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;
        ///&lt;Result xmlns:i=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; xmlns=&quot;http://schemas.sogeti.com/rgavrilov/2014-02-23&quot;&gt;
        ///  &lt;Records&gt;
        ///    &lt;President&gt;
        ///      &lt;LeftOffice&gt;1841-03-04T00:00:00&lt;/LeftOffice&gt;
        ///      &lt;Name&gt;Martin Van Buren&lt;/Name&gt;
        ///      &lt;Presidency&gt;8&lt;/Presidency&gt;
        ///      &lt;State&gt;New York&lt;/State&gt;
        ///      &lt;TookOffice&gt;1837-03-04T00:00:00&lt;/TookOffice&gt;
        ///    &lt;/President&gt;
        ///    &lt;President&gt;
        ///      &lt;LeftOffice&gt;1853-03-04T00:00:00&lt;/LeftOffice&gt;
        ///      &lt;Name&gt;Millard Fillmore&lt;/Name&gt;
        ///      &lt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RealOutput_Xml {
            get {
                return ResourceManager.GetString("RealOutput_Xml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 2, fname2 lname2, http://link-2, 2003-12-31, 2004-12-31, party2, portrait2, thumbnail2, homeState2
        ///Total count: 2..
        /// </summary>
        internal static string WithStateFilterOutput {
            get {
                return ResourceManager.GetString("WithStateFilterOutput", resourceCulture);
            }
        }
    }
}
