   M S B A M L   `   `   `  ����  Clear Choice+    %Clear_Choice.Views.LowAmountInvReport   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $ /Resources/Theme.xaml?�6%   %    5      .��5      ��  �� CornerRadius$ 5,5,5,5��6   $��1q�6$   ���	#15FFFFFF��5   
   �� 5      .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ 	#55FFFFFF��6   $	 0.5Q�61   � 5      $ 	#FFFFFFFF��6   $ 1Q�61   5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      .+�5#      �� $��0��6   $��0��6(   $
��Left=�65   $��20,5,5,5q�6P   $	��Top=�6b   $��List of items��6z   $��20(�6�   $��Normal&�6�   
�������6�   �� 5$      $��0��6   $��1��6(   $��15(�65   $��Normal&�6C   $
��Left=�6X   $��20,5,5,5q�6s   $��Amount is less than:��6�   $��Center=�6�   
�������6�   �� 5%      -   5        $��txtQuantity��5%   e   $��1��6   $��1��6&   $��23��63   $
��Left=�6?   $��5q�6Z   $��Center=�6x   $��66��6�   $��5��6�   �� 5&      -   5        $��txtOkay��5&   2   $��2��6   $��1��6%   $
��Find��6B   $
��Left=�6Q   $��5q�6l   $��Center=�6w   $��	10,5,10,5q�6�   &   System.Windows.Controls.DataGrid  5(      -   5        $��dgInventory��5(      $��2��6*   $��4��67   $��5q�5)         CanUserAddRows .  	   CanUserDeleteRows	 .  
   
IsReadOnly
 .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 .5* 5-         RowHeaderWidth$ 17Q�5.         	RowHeight$ 20Q�63      SelectionMode$ Single=�5/      $   HorizontalScrollBarVisibility$ Hidden=�66   $��12(�50      52      53      54      55      