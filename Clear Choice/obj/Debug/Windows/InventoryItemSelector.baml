   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice0   *Clear_Choice.Windows.InventoryItemSelector   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � Title$  Inventory Item Selector��5      $	��300��6%   $	��600��62    � Icon$ /Resources/logo2.ico��6>    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5	      $ /Resources/Theme.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��200 �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Item ID:��6   $��0��6$   $��0��64   $��Right=�6A   $��Center=�6]   &    Stemstudios.UIControls.SComboBox  5   
   -   5        $��
cmboItemID��5      $��1��61   $��0��6A   $��5q�6N      
IsEditable . 6v   �� 5   
   $��	Category:��6   $��0��6%   $��1��65   $��Right=�6B   $��Center=�6^     5   
   -   5        $��cmboCategory��5      $��1��63   $��1��6C   $��5q�6P   6[   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgItems��5      $��0��6$   $��3��64   $��2��6H   	   AutoGenerateColumns	 . $��5q�5   6   
   CanUserAddRows
 .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5#         RowHeaderWidth$ 17Q�5$         	RowHeight$ 20Q�6/      SelectionMode$ Extended=�5%      $   HorizontalScrollBarVisibility$ Hidden=�64   $��12(�5&         SelectionUnit$ FullRow=�5'      5)      5*      5+      