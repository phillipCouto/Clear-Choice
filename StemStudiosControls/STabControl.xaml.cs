using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Stemstudios.DataAccessLayer;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// This is the Custom Tab Controller for the Stemstudios UIControl Library. This answers the issue of accessing a tab controller statically and also allows for more TabControl customizations.
    /// </summary>
    public partial class STabControl : UserControl
    {
        private Hashtable tabs = new Hashtable();
        private Hashtable tabNames = new Hashtable();
        private int SelectedTab = -1;

        /// <summary>
        /// Initializes the Tab Controller
        /// </summary>
        public STabControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Will Add a new tab to the controller, if tab exists the control will select the tab.
        /// </summary>
        /// <param name="tab"></param>
        public void AddTab(STabItem tab)
        {
            //Check if tab already exists.
            if (!TabAlreadyOpen(tab))
            {
                //Add tab Name and object to Hashtables
                tabs.Add(tabs.Count, tab);
                tabNames.Add(tab.Name, tabs.Count-1);

                //Addes the Tabs Icon to the Top of the Control and associates it with event handlers.
                tab.IconClicked += TabItemClicked;
                tab.IconClosing += TabItemClosing;
                TabHeaders.Children.Add(tab);

                //If there is already a tab selected we just let the SelectTab function handle the rest.
                if (SelectedTab > -1)
                {
                    SelectTab(tab);
                }
                else
                {
                    //Tell the tab it is selected.
                    ((STabItem)TabHeaders.Children[TabHeaders.Children.Count - 1]).IsSelected = true;

                    //Update internal selection tracker
                    SelectedTab = TabHeaders.Children.Count - 1;

                    //Add content to content panel
                    TabContent.Children.Add(tab.TabContent);
                }
            }
            else
            {
                //Select the existing Tab
                SelectTab(tab);
            }

        }
        /// <summary>
        /// Checks if the Tab is currently in the TabControl if so returns true otherwise returns false.
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public Boolean TabAlreadyOpen(STabItem tab)
        {
            if (tabNames[tab.Name] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This Selects the Tab that matches the name of the STabItem object passed. This will skip if tab does not exist to prevent expection.
        /// </summary>
        /// <param name="tab"></param>
        public void SelectTab(STabItem tab)
        {
            if (tabNames[tab.Name] != null)
            {
                //Get the Tabs position
                STabItem tabItem = (STabItem)tabs[tabNames[tab.Name]];
                int index = TabHeaders.Children.IndexOf(tabItem);

                //Deselect the old tab
                ((STabItem)TabHeaders.Children[SelectedTab]).IsSelected = false;

                //Configure the Content animation
                DoubleAnimation fadeOut = new DoubleAnimation();
                fadeOut.From = 1;
                fadeOut.To = 0;
                fadeOut.Duration = new Duration(new TimeSpan(500000));

                //Attach Event handler
                fadeOut.Completed += PanelFadedOut;

                //Begin animation
                TabContent.BeginAnimation(Grid.OpacityProperty, fadeOut);

                //Change the Internal SelectedTab postion and select the passed tab.
                SelectedTab = index;
                ((STabItem)TabHeaders.Children[SelectedTab]).IsSelected = true;

                if (((STabItem)TabHeaders.Children[SelectedTab]).TabContent is ISTabView)
                {
                    ((ISTabView)((STabItem)TabHeaders.Children[SelectedTab]).TabContent).TabIsGainingFocus();
                }
            }
            
        }
        /// <summary>
        /// Changes the title of a tab and adds the new tab to the tab names table.
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="title"></param>
        /// <param name="newTabName"></param>
        public void ChangeTabTitle(String tabName, String title,String newTabName)
        {
            if (tabNames[tabName] != null)
            {
                //Get the Tabs position
                STabItem tabItem = (STabItem)tabs[tabNames[tabName]];
                int index = TabHeaders.Children.IndexOf(tabItem);
                ((STabItem)TabHeaders.Children[index]).TabTitle = title;
                ((STabItem)TabHeaders.Children[index]).Name = newTabName;
                if (!tabName.Equals(newTabName))
                {
                    tabNames.Add(newTabName, tabNames[tabName]);
                    tabNames.Remove(tabName);
                }
            }
        }

        public void RemoveTab(String tabName)
        {
            if (tabNames[tabName] != null)
            {
                //Get the Tabs position
                STabItem tabItem = (STabItem)tabs[tabNames[tabName]];
                RemoveTab(tabItem);
            }
        }
        /// <summary>
        /// Removes Tab object from the TabControl
        /// </summary>
        /// <param name="tab"></param>
        public void RemoveTab(STabItem tab)
        {
            //Check if tab exists
            if(TabAlreadyOpen(tab))
            {
                //Make sure to not close all tabs
                if (tabs.Count > 1)
                {
                    //Get the index of the tab to close
                    int index = TabHeaders.Children.IndexOf(tab);
                    //Check if tab is currently selected
                    if (index == SelectedTab)
                    {
                        //If tab is first in selection move selection to next
                        if (SelectedTab == 0)
                        {
                            SelectTab((STabItem)TabHeaders.Children[SelectedTab + 1]);
                        }
                        //Select Previous Tab
                        else
                        {
                            SelectTab((STabItem)TabHeaders.Children[SelectedTab - 1]);
                        }
                    }
                    else
                    {
                        if (SelectedTab > index)
                        {
                            SelectedTab--;
                        }
                    }
                    //Remove tab in Hashtables
                    tabNames.Remove(tab.Name);
                    tabs.Remove(index);

                    Object[] keys = new Object[tabs.Keys.Count];
                    tabs.Keys.CopyTo(keys, 0);
                    Array.Sort(keys);
                    
                    foreach (Object key in keys)
                    {
                        int keyValue = Int32.Parse(key.ToString());
                        if (keyValue > index)
                        {
                            tabs.Add(keyValue - 1, tabs[keyValue]);
                            tabs.Remove(keyValue);
                            tabNames[((STabItem)tabs[keyValue - 1]).Name] = keyValue - 1;
                        }
                    }
                    //Remove Tab Icon
                    TabHeaders.Children.Remove((STabItem)TabHeaders.Children[index]);
                }
            }
        }
        /// <summary>
        /// This handles the event when a TabItemIcon is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItemClicked(Object sender,MouseEventArgs e)
        {
            //Get the Tabs index using the sender object and issuing a Selection Change
            int index = TabHeaders.Children.IndexOf((STabItem)sender);
            if (((STabItem)TabHeaders.Children[SelectedTab]).TabContent is ISTabContent)
            {
                if (!((ISTabContent)((STabItem)TabHeaders.Children[SelectedTab]).TabContent).TabIsLosingFocusCallBack())
                {
                    return;
                }
            }
            else if(((STabItem)TabHeaders.Children[SelectedTab]).TabContent is ISTabView)
            {
                if (!((ISTabView)((STabItem)TabHeaders.Children[SelectedTab]).TabContent).TabIsLosingFocus())
                {
                    return;
                }
            }
            SelectTab((STabItem)TabHeaders.Children[index]);
        }
        /// <summary>
        /// Handles the closing event sent by the STabIcon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItemClosing(Object sender, EventArgs e)
        {
            //Get the Tabs index using the sender object and issuing a Selection Change
            Database.Instance.RollbackTransaction();
            int index = TabHeaders.Children.IndexOf((STabItem)sender);
            RemoveTab((STabItem)TabHeaders.Children[index]);
        }
        /// <summary>
        /// Returns the STabItem associated with the Name provided.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public STabItem GetItem(String Name)
        {
            return (STabItem)tabs[tabNames[Name]];
        }
        /// <summary>
        /// Returns the STabItem associated with the index provided.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public STabItem GetItem(int index)
        {
            return (STabItem)tabs[index];
        }
        /// <summary>
        /// This is the event handler for when the content is completed in fading out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelFadedOut(Object sender, EventArgs e)
        {
            //Remove the Old Content
            TabContent.Children.RemoveAt(0);

            //Grab the Selected tab and add it to content panel
            STabItem tabItem = (STabItem)TabHeaders.Children[SelectedTab];
            TabContent.Children.Add(tabItem.TabContent);

            //Fade in the selected tabs content
            DoubleAnimation fadeOut = new DoubleAnimation();
            fadeOut.From = 0;
            fadeOut.To = 1;
            fadeOut.Duration = new Duration(new TimeSpan(500000));
            TabContent.BeginAnimation(Grid.OpacityProperty, fadeOut);
        }
    }
}
