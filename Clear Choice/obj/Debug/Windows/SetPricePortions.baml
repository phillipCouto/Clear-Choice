   M S B A M L   `   `   `  ����E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS8clr-namespace:Stemstudios.UIControls;assembly=UIControlsStemstudios.UIControls  5   	    Clear Choice+   %Clear_Choice.Windows.SetPricePortions   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   O
UIControls8clr-namespace:Stemstudios.UIControls;assembly=UIControls     5   	     � Title$  Set Price Portions��5   	   $	��186��6$   $	��300��61   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$   $��Rough In Portion:��61   $��5q�6J   $��Right=�6U   $��Center=�6q   '   !Stemstudios.UIControls.SNumberBox  5   
   -   5        $��
amtRoughIn��5   =   $��1��6    $��0��60   $��5q�6O   6Z   �� 5   
   $��0��6   $��1��6$   $��Service Portion:��61   $��5q�6I   $��Right=�6T   $��Center=�6p     5   
   -   5        $��
amtService��5   =   $��1��6    $��1��60   $��5q�6O   6Z   �� 5   
   $��0��6   $��2��6$   $��Final Portion:��61   $��5q�6G   $��Right=�6R   $��Center=�6n     5   
   -   5        $��amtFinal��5   =   $��1��6    $��2��60   $��5q�6M   6X   �� 5   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��3��6>   .+�5      �� -   5        $��cmdSave��5      $
��Save��6$   $��	10,5,10,5q�63   $��5q�6G   {�.  6|   �� 5      -   5        $��	cmdCancel��5      $��Cancel��6&   $��	10,5,10,5q�67   $��5q�6K   6p   5      5      5       