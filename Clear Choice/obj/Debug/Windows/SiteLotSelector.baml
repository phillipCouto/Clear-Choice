   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice*   $Clear_Choice.Windows.SiteLotSelector   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � Title$  Site or Lot Selector��5      $	��300��6"   $	��600��6/    � Icon$ /Resources/logo2.ico��6;    � 	Resources 5       �� MergedDictionaries �� Source�� 5       5      �� 5	      $ /Resources/Theme.xaml?�6)   %    5      .��5   
   �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Client Type:��6   $��0��6(   $��0��68   $��5q�6E   $��Right=�6P   $��Center=�6l   �� 5   
   -   5        $��cmboType��5      $��1��6#   $��0��63   $��5q�6@    �� SelectedIndex$ 0��6K   .@�5      �� .��6   	Builder6$   �� 5      .��6   Misc. Client6)   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgSitesOrLots��5      $��1��6*   $��0��67   $��2��6G   	   AutoGenerateColumns	 . $��5q�5   6   
   CanUserAddRows
 .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5          RowHeaderWidth$ 17Q�5!         	RowHeight$ 20Q�6/      SelectionMode$ Extended=�5"      $   HorizontalScrollBarVisibility$ Hidden=�64   $��12(�5#         SelectionUnit$ FullRow=�5$      5&      5'      5(      