   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5   	    Clear Choice0   *Clear_Choice.Windows.LotRepairActionWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5   	     � Title$  Repair Action��5   	   $	��250��6   $	��400��6,    � Icon$ /Resources/logo2.ico��68   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Problem Area:��6   $��0��6)   $��0��69   $��Right=�6F   $��Center=�6b   �� 5   
   -   5        $��
txtProblem��5      $��1��6$   $��0��64   $��5q�6A    �� 	MaxLength$ 45��6k   �� 5   
   $��Description:��6   $��0��6(   $��1��68   $��Right=�6E   $��Center=�6a   �� 5   
   -   5        $��txtDescription��5      $��1��6(   $��1��68   $��5q�6E   $	 150��6o   �� 5   
   $��Action Date:��6   $��0��6(   $��2��68   $��Right=�6E   $��Center=�6a   (   "Stemstudios.UIControls.SDatePicker  5   
   -   5        $��dpActionDate��5   !   $��1��65   $��2��6E   $��5q�6R   6]   �� 5   
   $��Action Time:��6   $��0��6(   $��3��68   $��Right=�6E   $��Center=�6a   �� 5   
   -   5        $��txtTime��5      $��1��6!   $��3��61   $��5q�6>   $ 8��6h   �� 5   
   $��Action Taken:��6   $��0��6)   $��4��69   $��Right=�6F   $��Center=�6b   �� 5    
   -   5        $��	txtAction��5       $��1��6#   $��4��63   $��5q�6@   $	 150��6j   �� 5"   
   $��0��6   $��2��6%   $��5��69    �� Orientation$ 
Horizontal=�6F   .+�5#      �� -   5        $��cmdSaveEdit��5#      $��Save Changes��6(   $��5q�6?   $��	10,5,10,5q�6J   6^   �� 5$      -   5        $��	cmdCancel��5$      $��Cancel��6&   $��5q�67   $��	10,5,10,5q�6B   6V   5%      5&      5'      