﻿#pragma checksum "..\..\..\Windows\SiteLotSelector.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9ACC0AD9705741AADC30BCEA1269769"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Stemstudios.UIControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Clear_Choice.Windows {
    
    
    /// <summary>
    /// SiteLotSelector
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class SiteLotSelector : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Windows\SiteLotSelector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmboType;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Windows\SiteLotSelector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSitesOrLots;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/windows/sitelotselector.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\SiteLotSelector.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cmboType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\..\Windows\SiteLotSelector.xaml"
            this.cmboType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmboType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgSitesOrLots = ((System.Windows.Controls.DataGrid)(target));
            
            #line 37 "..\..\..\Windows\SiteLotSelector.xaml"
            this.dgSitesOrLots.AutoGeneratedColumns += new System.EventHandler(this.dgSitesOrLots_AutoGeneratedColumns);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\Windows\SiteLotSelector.xaml"
            this.dgSitesOrLots.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.dgSitesOrLots_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

