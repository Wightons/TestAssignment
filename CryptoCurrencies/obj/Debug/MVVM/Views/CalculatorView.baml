   M S B A M L   `   `   `  ����K  GCryptoCurrencies, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:CryptoCurrencies.MVVM.ViewsCryptoCurrencies.MVVM.Views  5      0    *CryptoCurrencies.MVVM.Views.CalculatorView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:CryptoCurrencies.MVVM.Views     5      .��5	      � 
������ .+�5
   
   �� $
��Left=�6   $��Bottom=�6,   $��	10,0,0,10q�6G   $��25��6[   $	��100��6g   
����ح���6�   �� *BackCommand+.��5      �� $
��Back��6     �� TextAlignment$  Center=�6$   $��Center=�6;   $��Oswald)�6X   $��SemiBold"�6l   5      �� 5   
   $��10,10,10,40q�6    �� CornerRadius$ 9��6&   
�������$��0q�6N   ._�5      �� $��70q�6   $	��300��6!   $��3q�6-   
���   �$ 10��6U   
����ح�$	��230��6~   ._�5      �� $��5q�6   .+�5      �� $��	You Sell:��6    $��20(�61   
�������$��Roboto Slab)�6Q   �� 5       �� Orientation$ 
Horizontal=�6!   .+�5      �� $��10,0,0,0q�6@   $	��100��6R   $��18(�6^    �� TextAlignment$ Center=�6l   ��6"   �� *SellCurrency+6l   �� 5      $��54,0,0,0q�6#   $��30��65   $	��120��6A   $��Right=�6M    �� DisplayMemberPath$
 Name��6�   ��6i   �� *CollectionOfCurrencies+ �� SelectedItem 5   #   �� *SelectedCurrencyToSell+5      �� 5      $��You Get:��6    $��20(�60   
�������$��Roboto Slab)�6Q   �� 5      $ 
Horizontal=�6!   .+�5      �� $��10,0,0,0q�6?   $	��100��6Q   $��18(�6]   $ Center=�6k   {�.  ��6"   �� *GetCurrency+6�   �� 5      $��54,0,0,0q�6#   $��30��65   $	��120��6A   $��Right=�6M   $
 Name��6�   ��6i   �� *CollectionOfCurrencies+ 5   #   �� *SelectedCurrencyToGet+5      �� 5      $	��100��6   $��40��6)   $��0,35,0,0q�65   ��6G   �� *CalculateCommand+.��5      �� $��	Calculate��6$   $  Center=�65   $��18(�6L   5      5       5!      5"      5#      5$      