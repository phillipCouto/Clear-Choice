   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice&    Clear_Choice.Views.InventoryView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ /Resources/Theme.xaml?�6%   �� 5	      $- '/Resources/Views/InventoryViewDict.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5��6   $��1q�6   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��27 �6    �� 5      .+�5      �� -   5        $��grpItemInfo��5   $   $��0��6   $��14(�67   ���	#FFFFFFFF$��5,5,5,0q�6\   $��255.0��6m   ��5      �� 5       $��Item Information��6    
���������6K   ��  �� Path$ Width�	 �� ElementName$	 grpItemInfo��
 �� Mode$
 Default=�.��5"      �  5#      �� 5$      �� 5%       5'      �� 5(      $
��Auto �6(   �� 5)      $
��Auto �6(   .+�5+      � $��0��6   $��0��6+   $��5,5,5,5q�68    5,      �� 5-      $
��Auto �6/   �� 5.       50      �� 51      $
��Auto �6,   �� 52      $
��Auto �6,   �� 53      $
��Auto �6,   �� 54      $
��Auto �6,   �� 55      $
��Auto �6,   �� 56      $
��Auto �6,   .+�58      �� $��0��6$   $��0��64   $��Right=�6A   $��Center=�6]   
Item ID:6�   �� 59      -   5        $��	txtItemID��59   "   $��1��64   $
��Test��6D   $��5q�6P   $��0��6[   6h   �� 5;      $��0��6$   $��1��64   $��Right=�6A   $��Center=�6]   
Item Name:6�   �� 5<      -   5        $��txtItemName��5<   "   $��1��65   $
��Test��6E   $��5q�6Q   $��1��6\   6i   �� 5>      $��0��6$   $��2��64   $��Right=�6A   $��Center=�6]   Item Category:6�   &    Stemstudios.UIControls.SComboBox  5?      -   5        $��cmboCategory��5?   /   $��1��6C   $��5q�6S      
IsEditable . $��2��6p   6}   �� 5A      $��0��6$   $��3��64   $��Right=�6A   $��Center=�6]   $��5q�6x   	Quantity:6�   �� 5B      -   5        $��txtQuantity��5B   "   $��1��65   $��0��6E   $��5q�6N   $��3��6Y   ���	#00FFFFFF���	#FFFFFFFF6}   �� 5D      $��0��6$   $��4��64   $��Right=�6A   $��Center=�6]   $��5q�6x   Average Cost:6�   '   !Stemstudios.UIControls.SNumberBox  5E      -   5        $��txtAverageCost��5E   0   $��1��6F   $��5q�6V   $��4��6a   �� 5G      $��0��6$   $��5��64   $��Right=�6A   $��Center=�6]   $��5q�6x   Latest Cost:6�     5H      -   5        $��txtLatestCost��5H   0   $��1��6E   $��5q�6U   $��5��6`   5I      �� 5K      $��1��6!   $��0��61   $��5,5,5,5q�6>   .+�5L      �� $��0��6$   Item Description:6G   �� 5M      -   5        $��txtItemDescription��5M   "    �� TextWrapping$ WrapWithOverflow=�6<   $��Disabled=�5N   "   $
��Auto=�5O   "   $
��Auto��5P   "   $��85��5Q   "    �� AcceptsReturn . 5S   "   �� 5U       �� Orientation$ 
Horizontal=�6%   .+�5V      �� -	   5        $��cmdSaveEdit��5V   %   $��Save Changes��68   $��5q�6O   $��	10,5,10,5q�6Z   6n   �� 5W      -
   5        $��	cmdCancel��5W   %   $��Cancel��66   $��5q�6G   $��	10,5,10,5q�6R   6f   5X      5Y      5Z      5[      �� 5\      -   5        $��TabShowHideButton��5\      $��1��6-   $��Center=�6:   $	��135��6   $��27��6�   &   System.Windows.Controls.DataGrid  5]      -   5        $��inventoryGridView��5^      $��2��5]      $��10q�5_         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5d         RowHeaderWidth$ 17Q�5e         	RowHeight$ 20Q�63      SelectionMode$ Single=�5f      $   HorizontalScrollBarVisibility$ Hidden=�66   $��12(�5g      5j      5k      5l      5m      