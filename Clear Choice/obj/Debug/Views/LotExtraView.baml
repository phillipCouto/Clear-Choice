   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice%   Clear_Choice.Views.LotExtraView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Atoolkit-http://schemas.microsoft.com/wpf/2008/toolkit     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5	      $ /Resources/Theme.xaml?�6%   �� 5
      $, &/Resources/Views/LotExtraViewDict.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��27 �6    �� 5      �� 5      $
��Auto �6    .+�5       �� -   5        $��grpExtraInfo��5       $��14(�6+   ���	#FFFFFFFF$��5,5,5,0q�6P   $��0��6a   $	��300��6n   ��5!      �� 5"      $��Extra Information��6    
���������6L   ��  �� Path$ Width�	 �� ElementName$	 grpExtraInfo��
 �� Mode$
 Default=�.��5$      �  5%      �� 5&      $
��Auto �6+   �� 5'      �� 5(      $
��Auto �6+   �� 5)       5+      �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(   �� 5.      $
��Auto �6(   �� 5/      $
��Auto �6(   �� 50      $
��Auto �6(   �� 51      $
��Auto �6(   �� 52      $
��Auto �6(   �� 53      $
��Auto �6(   �� 54      $
��Auto �6(   .+�56      �� $��Extra:��6    $��0��6.   $��0��6>   $��5q�6K   $��Right=�6V   $��Center=�6r   &    Stemstudios.UIControls.SComboBox  57      -   5        $��	cmboExtra��57   +   $��1��6<   $��0��6L   $��5q�6Y      
IsEditable . 6�   �� 59      $��	Location:��6    $��0��61   $��1��6A   $��5q�6N   $��Right=�6Y   $��Center=�6u   �� 5:      -   5        $��txtLocation��5:      $��1��61   $��1��6A   $��5q�6N   6Y   �� 5<      $	��PO:��6!   $��0��6,   $��2��6<   $��5q�6I   $��Right=�6T   $��Center=�6p   �� 5=      -   5        $��txtPO��5=      $��1��6+   $��2��6;   $��5q�6H   6S   �� 5?      $��Invoice:��6    $��0��60   $��3��6@   $��5q�6M   $��Right=�6X   $��Center=�6t   �� 5@      -   5        $��
txtInvoice��5@      $��1��60   $��3��6@   $��5q�6M   6X   �� 5B      $��Unit Price:��6    $��2��63   $��0��6C   $��5q�6P   $��Right=�6[   $��Center=�6w   '   !Stemstudios.UIControls.SNumberBox  5C      -   5        $��txtUnitPrice��5C   ,   $��3��6@   $��0��6P   $��5q�6|   �� 5E      $��	Quantity:��6    $��2��61   $��1��6A   $��5q�6N   $��Right=�6Y   $��Center=�6u   �� 5F      -   5        $��txtQuantity��5F      $��3��61   $��1��6A   $��5q�6N   6Y   �� 5H      $��Billed Quantity:��6    $��2��68   $��2��6H   $��5q�6U   $��Right=�6`   $��Center=�6|   �� 5I      -   5        $��txtBilledQuantity��5I      $��3��67   $��2��6G   $��5q�6T   6_   �� 5K      $��Billed Date:��6    $��2��64   $��3��6D   $��5q�6Q   $��Right=�6\   $��Center=�6x   (   "Stemstudios.UIControls.SDatePicker  5L      -	   5        $��dpBilledDate��5L   -   $��3��6A   $��3��6Q   $��5q�6^      TextForeground
 �����6�   �� 5N      $��Total price:��6!   $��2��65   $��4��6E   $��5q�6R   $��Right=�6]   $��Center=�6y     5O      -
   5        $��txtTotalPrice��5O   ,   $��3��6A   $��4��6Q   $��5q�6^      
IsReadOnly . 6i   �� 5Q      $��Notes:��6    $��0��6.   $��5��6>   $��5q�6K   $��Right=�6V   $��Center=�6r   �� 5R      -   5        $��txtNotes��5R      $��1��6.   $��5��6>   $��3��6K    �� TextWrapping$ WrapWithOverflow=�6_   $��Disabled=�5S   "   $
��Auto=�5T   "   $
��Auto��5U   "   $��50��5V       �� AcceptsReturn . $��5q�5Y   %   �� 5[       �� Orientation$ 
Horizontal=�6!   $��0��6:   $��6��6J   $��4��6W   .+�5\      �� -   5        $��cmdSaveEdit��5\   !   $��Unlock Form��64   $��5q�6J   $��	10,5,10,5q�6U   6i   �� 5]      -   5        $��	cmdCancel��5]   !   $��Cancel��62   $��5q�6C   $��	10,5,10,5q�6N   6b   5^      5_      5`      �� 5a      -   5        $��TabShowHideButton��5a      $��1��6-   $��Center=�6:   $	��135��6W   $��27��6c   6o   &   System.Windows.Controls.DataGrid  5b      -   5        $��ExtraGridView��5c      $��2��5b      $��10q�5d         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5h         RowHeaderWidth$ 17Q�5i         	RowHeight$ 20Q�63      SelectionMode$ Single=�5j      $   HorizontalScrollBarVisibility$ Hidden=�66   $��12(�5k      5n      �� 5o      $ 
Horizontal=�6   $��3��62   $��Right=�6?   .+�5p      �� $��Total Quantity��6   $��5q�62   $��14(�6=   ���	#FFFFFFFF6K   �� 5q      -   5        $��txtTotalQuantity��5q      $��5q�62   ���	#00FFFFFF���	#FFFFFFFF �� 
IsReadOnly . $��0��6}   �� 5r      $��Total Value��6   $��5q�6/   $��14(�6:   ���	#FFFFFFFF6H     5s      -   5        $��amtTotalValue��5s   (   $��5q�6=      
IsCurrency .  .    Amount$ 0��6l   5t      5u      5v      5w      