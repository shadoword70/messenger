   M S B A M L   `   `   `  ����J  FServerMessenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ServerMessenger.ViewsServerMessenger.Views  5      +    %ServerMessenger.Views.EndpointControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:ServerMessenger.Views     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6%   $��5q�62   .+�5      �� $��NetworkInterfaces��6   62   �� 5      $	��300��6   $	��300��6&   ��5      �� *	Endpoints+ �� SelectedItem 5      ��  �� Path$ Endpoint���5      �� 5      .R�5      �� $��5q�6%   .+�5      �� $��18(�6(   ��66   �� $
 Name�6S   �� 5      ��6(   �� $ Description�6L   �� 5      ��6(   �� $ NetworkInterfaceType�6U   5      5      5       5!      �� 5"   
   $��1��6   $��0��6%    � DataContext 62   �� *EndpointData+.+�5#      �� $��5q�6   .+�5$      �� $��Ip��6   6'   �� 5%       �� 
IsReadOnly . $	��100��5'      $��20��5(      ��5%      �� $ Ip� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�5(   $   5)      �� 5*      $��5q�6   .+�5+      �� $
��Port��6   6)   �� 5,      $	��100��5-      $��20��5.      .��6"   ��5/      �� 50      $
 Port�6"   $ PropertyChanged=�6.    �� ValidationRules 51      5  /System.Windows.Controls.DataErrorValidationRule  52   "   54      .��55   $   56      57      �� 58      $��5q�6   .+�59      �� $��
ServerName��6   6/   �� 5:      $	��100��5;      $��20��5<      ��5:      �� $ 
ServerName�$ PropertyChanged=�5<   $   5=      5>      �� 5?   
   $��0��6   $��2��6!   $	��100��65   $��1��6A   $��5q�6N   $��Create��6Y   ��6j   �� *Create+6�   5@      5A      