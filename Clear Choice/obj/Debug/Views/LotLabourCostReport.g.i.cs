﻿#pragma checksum "..\..\..\Views\LotLabourCostReport.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9A0DC294949824EDAC57B7258E38741A"
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


namespace Clear_Choice.Views {
    
    
    /// <summary>
    /// LotLabourCostReport
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class LotLabourCostReport : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SDatePicker dpFrom;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SDatePicker dpTo;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgLabourHours;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAvgHours;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox amtAvgCost;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTotalHours;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Views\LotLabourCostReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox amtTotalCost;
        
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
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/views/lotlabourcostreport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\LotLabourCostReport.xaml"
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
            this.dpFrom = ((Stemstudios.UIControls.SDatePicker)(target));
            
            #line 40 "..\..\..\Views\LotLabourCostReport.xaml"
            this.dpFrom.SelectedDateChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dpFrom_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dpTo = ((Stemstudios.UIControls.SDatePicker)(target));
            
            #line 42 "..\..\..\Views\LotLabourCostReport.xaml"
            this.dpTo.SelectedDateChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dpFrom_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgLabourHours = ((System.Windows.Controls.DataGrid)(target));
            
            #line 52 "..\..\..\Views\LotLabourCostReport.xaml"
            this.dgLabourHours.AutoGeneratedColumns += new System.EventHandler(this.dgLots_AutoGeneratedColumns);
            
            #line default
            #line hidden
            
            #line 52 "..\..\..\Views\LotLabourCostReport.xaml"
            this.dgLabourHours.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgLabourHours_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtAvgHours = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.amtAvgCost = ((Stemstudios.UIControls.SNumberBox)(target));
            return;
            case 6:
            this.txtTotalHours = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.amtTotalCost = ((Stemstudios.UIControls.SNumberBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

