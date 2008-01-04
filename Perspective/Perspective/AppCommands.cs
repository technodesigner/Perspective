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
using System.Windows.Input;

namespace PerspectiveDemo
{
    public class AppCommands
    {
        private static RoutedCommand _openPageCommand;

        public static RoutedCommand OpenPageCommand
        {
            get { return _openPageCommand; }
        }

        private static RoutedCommand _loadSkinCommand;

        public static RoutedCommand LoadSkinCommand
        {
            get { return AppCommands._loadSkinCommand; }
        }

        static AppCommands()
        {
            _openPageCommand = new RoutedCommand();
            _loadSkinCommand = new RoutedCommand();
        }

    }
}
