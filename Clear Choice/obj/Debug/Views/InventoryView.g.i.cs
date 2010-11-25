﻿#pragma checksum "..\..\..\Views\InventoryView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2A2CE2C0A62F8168029ABC4D32C9050E"
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
    /// InventoryView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class InventoryView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox grpItemInfo;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtItemID;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtItemName;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SComboBox cmboCategory;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantity;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox txtAverageCost;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Stemstudios.UIControls.SNumberBox txtLatestCost;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtItemDescription;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdSaveEdit;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdCancel;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image TabShowHideButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Views\InventoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid inventoryGridView;
        
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
            System.Uri resourceLocater = new System.Uri("/Clear Choice;component/views/inventoryview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\InventoryView.xaml"
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
            
            #line 5 "..\..\..\Views\InventoryView.xaml"
            ((Clear_Choice.Views.InventoryView)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UserControl_IsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grpItemInfo = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 3:
            this.txtItemID = ((System.Windows.Controls.TextBox)(target));
            
            #line 58 "..\..\..\Views\InventoryView.xaml"
            this.txtItemID.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtItemName = ((System.Windows.Controls.TextBox)(target));
            
            #line 61 "..\..\..\Views\InventoryView.xaml"
            this.txtItemName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cmboCategory = ((Stemstudios.UIControls.SComboBox)(target));
            
            #line 64 "..\..\..\Views\InventoryView.xaml"
            this.cmboCategory.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmboCategory_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtQuantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtAverageCost = ((Stemstudios.UIControls.SNumberBox)(target));
            return;
            case 8:
            this.txtLatestCost = ((Stemstudios.UIControls.SNumberBox)(target));
            return;
            case 9:
            this.txtItemDescription = ((System.Windows.Controls.TextBox)(target));
            
            #line 84 "..\..\..\Views\InventoryView.xaml"
            this.txtItemDescription.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.fieldTextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cmdSaveEdit = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\Views\InventoryView.xaml"
            this.cmdSaveEdit.Click += new System.Windows.RoutedEventHandler(this.cmdSaveEdit_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.cmdCancel = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\..\Views\InventoryView.xaml"
            this.cmdCancel.Click += new System.Windows.RoutedEventHandler(this.cmdCancel_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.TabShowHideButton = ((System.Windows.Controls.Image)(target));
            
            #line 93 "..\..\..\Views\InventoryView.xaml"
            this.TabShowHideButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TabShowHideButton_MouseDown);
            
            #line default
            #line hidden
            return;
            case 13:
            this.inventoryGridView = ((System.Windows.Controls.DataGrid)(target));
            
            #line 100 "..\..\..\Views\InventoryView.xaml"
            this.inventoryGridView.AutoGeneratedColumns += new System.EventHandler(this.inventoryGridView_AutoGeneratedColumns);
            
            #line default
            #line hidden
            
            #line 105 "..\..\..\Views\InventoryView.xaml"
            this.inventoryGridView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.inventoryGridView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

