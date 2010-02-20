//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Interop;

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for XBAPs.
    /// </summary>
    public static class XbapHelper
    {
        private static Dictionary<string, string> _queryStrings;

        /// <summary>
        /// Gets the dictionary filled with URI's parameters.
        /// </summary>
        public static Dictionary<string, string> QueryStrings
        {
            get { return _queryStrings; }
        }

        static XbapHelper()
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                _queryStrings = new Dictionary<string, string>();
                Uri uri = BrowserInteropHelper.Source;
                if (uri != null)
                {
                    string query = uri.Query;
                    if (query.Length > 0)
                    {
                        string regEx = @"(\w*)=([a-zA-Z0-9_/\.-]*)";
                        MatchCollection matches = Regex.Matches(query, regEx, RegexOptions.IgnoreCase);
                        foreach (Match match in matches)
                        {
                            _queryStrings[match.Groups[1].Value] = match.Groups[2].Value;
                        }
                    }
                }
            }
        }

        private static string _pageParameterKey = "page";

        /// <summary>
        /// Gets the parameter key which indicates a page (i.e. to open. it)
        /// </summary>
        public static string PageParameterKey
        {
            get { return _pageParameterKey; }
        } 

        /// <summary>
        /// Gets the parameter value which indicates a page (i.e. to open. it)
        /// </summary>
        public static string PageParameterValue
        {
            get 
            {
                string s = "";
                if (_queryStrings.ContainsKey(_pageParameterKey))
                {
                    s = _queryStrings[_pageParameterKey];
                }
                return s; 
            }
        }
    }
}
