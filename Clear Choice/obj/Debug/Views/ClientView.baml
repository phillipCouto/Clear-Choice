   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5       Clear Choice"   ClearChoice.Views.ClientView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ /Resources/IconSet.xaml?�6%   �� 5      $ /Resources/Theme.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5       � ColumnDefinitions 5      �� 5       �� 5!      .+�5#      �� -   5        $��grpNames��5#      $��14(�6'   $��0��65   $��0��6E   $��2��6R   ���#FFFFFF$��5q�6x   ��5$      �� 5%      $��Client Information��6    
���������6M   ��  �� Path$ Width�	 �� ElementName$	 grpNames��
 �� Mode$
 Default=�.��5'      �  5(      �� 5)      $
��auto �6+   �� 5*       5,      �� 5-      $
��auto �6(   �� 5.      $
��auto �6(   �� 5/      $
��auto �6(   �� 50      $
��auto �6(   �� 51      $
��auto �6(   �� 52      $
��auto �6(   .+�54      �� $��Name:��6    ���	#FFFFFFFF$��0��6D   $��0��6T   $��Center=�6a   $��Right=�6|   �� 55      -   5        $��txtName��55      $��1��6-   $��0��6=   $��5,5,5,5q�6J   6[   �� 56      $��Type of Client:��6    ���	#FFFFFFFF$��0��6N   $��1��6^   $��Center=�6k   $��Right=�6�   &    Stemstudios.UIControls.SComboBox  57      -   5        $��cmbTypeOfClient��57   +   $��1��6B   $��1��6R   $��5q�6_      SelectedIndex$ 0��6j      Items 58      �� 59      .��6+   	Builder64   �� 5:      .��6+   Miscellaneous Client6A   5<      �� 5=      $��Phone Number:��6    ���	#FFFFFFFF$��0��6L   $��2��6\   $��Center=�6i   $��Right=�6�   (   "Stemstudios.UIControls.SPhoneField  5>      -   5        $��txtContactNumber��5>   -   $��1��6E   $��2��6U   $��5,5,5,5q�6b   6s   �� 5?      $��Fax Number:��6    $��0��63   $��3��6C   $��5q�6P   $��Center=�6[   $��Right=�6v     5@      -   5        $��txtFaxNumber��5@   -   $��1��6A   $��3��6Q   $��5q�6^   6i   �� 5A      $��Email Address:��6    ���	#FFFFFFFF$��0��6M   $��4��6]   $��Center=�6j   $��Right=�6�   �� 5B      -   5        $��txtEmail��5B      $��1��6.   $��4��6>   $��5,5,5,5q�6K   6\   �� 5D       �� Orientation$ 
Horizontal=�6!   $��0��6:   $��5��6J   $��2��6W   .+�5E      �� -   5        $��cmdSaveEdit��5E   8   $��Save Changes��6!   $��0��6e   $��3��6u   $��5q�6�   $	��150��6�   $��	10,5,10,5q�6�   �� 5F      -   5        $��	cmdCancel��5F   2   $��Cancel��6!   $��5q�6\   $	��150��6g   $
��Left=�6v   $��	10,5,10,5q�6�   5G      5H      5I      �� 5J      -	   5        $��grpAddressInfo��5J      $��1��6-   $��0��6=   $��14(�6J   ���#FFFFFF$��5q�6m   ��5K      �� 5L      $��Address Information��6    
���������6N   �� $ Width�$	 grpAddressInfo��$
 Default=�.��5N      �  5O      �� 5P      $
��auto �6+   �� 5Q       5S      �� 5T      $
��auto �6(   �� 5U      $
��auto �6(   �� 5V      $
��auto �6(   .+�5X      �� $��Street Address:��6    ���	#FFFFFFFF$��0��6N   $��0��6^   $��Center=�6k   $��Right=�6�   �� 5Y      -
   5        $��	txtStreet��5Y      $��1��6/   $��0��6?   $��5,5,5,5q�6L   6]   �� 5Z      $��City:��6    ���	#FFFFFFFF$��0��6D   $��1��6T   $��Center=�6a   $��Right=�6|   �� 5[      -   5        $��txtCity��5[      $��1��6-   $��1��6=   $��5,5,5,5q�6J   6[   �� 5\      $��Postal Code:��6    ���	#FFFFFFFF$��0��6K   $��2��6[   $��Center=�6h   $��Right=�6�   �� 5]      -   5        $��txtPostalCode��5]      $��1��63   $��2��6C   $��5,5,5,5q�6P   6a   5`      5a      �� 5b      -   5        $��grpPriceInfo��5b      $��1��6+   $��1��6;   $��14(�6H   ���#FFFFFF$��5q�6k   ��5c      �� 5d      $��Price Portions��6    
���������6I   �� $ Width�$	 grpPriceInfo��$
 Default=�.��5f      �  5g      �� 5h      $
��auto �6+   �� 5i       5k      �� 5l      $
��auto �6(   �� 5m      $
��auto �6(   �� 5n      $
��auto �6(   .+�5p      �� $��	Rough In:��6    ���	#FFFFFFFF$��0��6H   $��0��6X   $��Center=�6e   $��Right=�6�   '   !Stemstudios.UIControls.SNumberBox  5q      -   5        $��
amtRoughIn��5q   ,   $��1��6>   $��0��6N   $��5,5,5,5q�6[      
IsCurrency .  6�   �� 5r      $��Service:��6    ���	#FFFFFFFF$��0��6G   $��1��6W   $��Center=�6d   $��Right=�6     5s      -   5        $��
amtService��5s   ,   $��1��6>   $��1��6N   $��5,5,5,5q�6[    .  6�   �� 5t      $��Final:��6    ���	#FFFFFFFF$��0��6E   $��2��6U   $��Center=�6b   $��Right=�6}     5u      -   5        $��amtFinal��5u   ,   $��1��6<   $��2��6L   $��5,5,5,5q�6Y    .  6�   5v      5w      &   System.Windows.Controls.DataGrid  5x      -   5        $��siteLotsView��5x   H   $��0��6   $��2��6'   $��2��64   $��5q�6\      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5}         RowHeaderWidth$ 17Q�5~         	RowHeight$ 20Q�63      SelectionMode$ Extended=�5      $   HorizontalScrollBarVisibility$ Hidden=�68   $��14(�5�         SelectionUnit$ FullRow=�5�   B   5�      5�      5�      5�      