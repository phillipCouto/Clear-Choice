   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice1   +Clear_Choice.Views.InventoryTransactionView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ /Resources/Theme.xaml?�6%   �� 5	      $8 2/Resources/Views/InventoryTransactionViewDict.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5��6   $��1q�6   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��27 �6    �� 5      �� 5      $
��Auto �6    .+�5       �� -   5        $��grpRecieverInfo��5    $   $��0��6   $��14(�6;   
�������$��5q�6\   ��5!      �� 5"      $��Transaction Information��6    
���������6R   ��  �� Path$ Width�	 �� ElementName$	 grpRecieverInfo��
 �� Mode$
 Default=�.��5$      �  5%      �� 5&      $
��Auto �6+   �� 5'      �� 5(      $
��Auto �6+   �� 5)      .+�5+      �� $��0��6    $��
Recieving:��60   $��Center=�6B   �� 5,      -   5        $��txtReciever��5,      $��1��61   $��5q�6A    �� 
IsReadOnly . 
����������	#00FFFFFF6q   �� 5-      $��2��6    $��Transaction Date:��60   $��Center=�6I   (   "Stemstudios.UIControls.SDatePicker  5.      -   5        $��dpTransactionDate��5.   -   $��3��6F   $��5q�6V      TextForeground
 �����6�   5/      50      �� 51      -   5        $��grpItemInfo��51   $   $��1��6   $��14(�67   ���	#FFFFFFFF$��5,5,5,0q�6\   $��185.0��6m   ��52      �� 53      $��Item Information��6    
���������6K   �� $ Width�$	 grpItemInfo��$
 Default=�.��55      � $��0��6   $��0��6'   $��5q�64    56      �� 57      $
��Auto �6+   �� 58      �� 59      $
��Auto �6+   �� 5:       5<      �� 5=      $
��Auto �6(   �� 5>      $
��Auto �6(   �� 5?      $
��Auto �6(   �� 5@      $
��Auto �6(   .+�5B      �� $��0��6    $��0��60   $��Right=�6=   $��Center=�6Y   
Item ID:6~   �� 5C      -   5        $��	txtItemID��5C      $��1��60   $
��Test��6@   $��5q�6L   $��0��6W    . 
����������	#00FFFFFF6�   �� 5E      $��0��6    $��1��60   $��Right=�6=   $��Center=�6Y   
Item Name:6�   �� 5F      -   5        $��txtItemName��5F      $��1��61   $
��Test��6A   $��5q�6M   $��1��6X    . 
����������	#00FFFFFF6�   �� 5H      $��0��6    $��2��60   $��Right=�6=   $��Center=�6Y   Item Category:6�   �� 5I      -   5        $��txtCategory��5I      $��1��61   $��2��6A   $��5q�6N    . 
����������	#00FFFFFF6~   �� 5K      $��2��6    $��0��60   $��Right=�6=   $��Center=�6Y   $��5q�6t   	Quantity:6�   �� 5L      -   5        $��txtQuantity��5L      $��3��61   $��0��6A   $��5q�6J   $��0��6U   6b   �� 5N      $��2��6    $��1��60   $��Right=�6=   $��Center=�6Y   $��5q�6t   Average Cost:6�   '   !Stemstudios.UIControls.SNumberBox  5O      -	   5        $��txtAverageCost��5O   ,   $��3��6B   $��5q�6R   $��1��6]   �� 5Q      $��2��6    $��2��60   $��Right=�6=   $��Center=�6Y   $��5q�6t   Latest Cost:6�     5R      -
   5        $��txtLatestCost��5R   ,   $��3��6A   $��5q�6Q   $��2��6\   6i   �� 5T      $��0��6!   $��3��61   $��5q�6>   $��4��6I   .+�5U      ��  �� Orientation$ 
Horizontal=�6%   .+�5V      �� -   5        $��cmdSaveEditItem��5V   %   $��Add Item��6<   $��5q�6O   $��	10,5,10,5q�6Z   6n   �� 5W      -   5        $��cmdCancelItem��5W   %   $��Cancel��6:   $��5q�6K   $��	10,5,10,5q�6V   6j   �� 5X      -   5        $��cmdRemoveItem��5X   %   $��Remove Item��6:   $��5q�6P   $��	10,5,10,5q�6[   6o   5Y      5Z      5[      5\      �� 5]      -   5        $��TabShowHideButton��5]      $��2��6-   $��Center=�6:   $	��135��6W   $��27��6c   6o   &   System.Windows.Controls.DataGrid  5^      -   5        $��dgTransactionItems��5_      $��3��5^      $��10q�5`         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5d         RowHeaderWidth$ 17Q�5e         	RowHeight$ 20Q�63      SelectionMode$ Single=�5f      $   HorizontalScrollBarVisibility$ Hidden=�6.   $��12(�5g      5j      �� 5k      $ 
Horizontal=�6   $��4��62   $��Right=�6?   .+�5l      �� $��Total Quantity��6   $��5q�62   $��14(�6=   ���	#FFFFFFFF6K   �� 5m      -   5        $��txtTotalQuantity��5m      $��5q�62   ���	#00FFFFFF���	#FFFFFFFF . $��0��6}   �� 5n      $��Total Value��6   $��5q�6/   $��14(�6:   ���	#FFFFFFFF6H     5o      -   5        $��amtTotalValue��5o   (   $��5q�6=      
IsCurrency .    
IsReadOnly .    Amount$ 0��6l   5p      5q      5r      5s      