   M S B A M L   `   `   `  ����J  FClientMessenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ClientMessenger.ViewsClientMessenger.Views  5   	   G(clr-namespace:ClientMessenger.ViewModelsClientMessenger.ViewModels  5   	   1    +ClientMessenger.Views.CreateGroupChatWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ClientMessenger.Views     5   	   ?
viewModels(clr-namespace:ClientMessenger.ViewModels     5   	     � Title$  CreateGroupChatWindow��5	   	   $	��400��6'   $	��300��64    � 
ResizeMode$ NoResize=�6@    � WindowStartupLocation$ CenterScreen=�5
   	      CustomWindow#��[  6.    � DataContext 5      9   3ClientMessenger.ViewModels.CreateGroupChatViewModel  5   
    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        #  System.Windows.SystemCommands   CloseWindowCommand   Command# Z 5      6w     5   
   -   5           MaximizeWindowCommand# Z 5      6z     5   
   -   5           MinimizeWindowCommand# Z 5      6z     5   
   -   5        	   RestoreWindowCommand# Z	 5      6y   
 � 	Resources
 5       �� 
TargetType  CustomTextBox �� BasedOn  CustomLabel�� 5   
   %`   '��       7  '�� "     7  �� 5      " ��6   8   62   �� 5      " ��6   8   60   5      .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   .+�5"   
   �� $��0��6   $��0 0 0 10q�6"   .+�5#      �� $��Название чата��6   �� 5$      ��6   �� *Model.ChatName+5%      �� 5&   
   $��1��6   .+�5'      �� $��Пользователи��6   6-   �� 5(      -   5         ��Name$ UsersListBox��5(        CustomListBoxItem#��[ 6,     CustomListBox#��[ 6d   $	��200��5*      $��Stretch=�6#   $��Stretch=�6A   $��Disabled=�6]    �� SelectionMode$ Multiple=�5+      $��0 0 0 10q�6/   ��.  ��5)      �� *Model.Users+ �� UpdateSourceTrigger$ PropertyChanged=���5,      �� 5-      .R�5.      � $��Stretch=�6   $��Stretch=�6;    � ColumnDefinitions 5/      �� 50   "   $
��Auto �63   �� 51   "   $��* �63    53      �� 54   "   $��* �60   �� 55   "   $��* �60   .+�57      A� $��50��6&   $��50��62   $��0��6=   $��0��6M   $��2��6Z   $��Center=�6k   $	��0.6��58   *   
������  �� StrokeDashCap$ Round=�6U   ��59   "   �� 5:   &    j� Stretch$ UniformToFill=�61    �� ImageSource 6I   �� *EmployeePhoto+5<      �� 5=       �� Orientation$ 
Horizontal=�6)   $��1��6B   $��0��6R   .+�5>   "   ��   CustomTextBlock#��[ 6,   $��20��6U   $��5 0 0 0q�6a   $��13(�6�   ��6r   �� *	ShortName+6�   5?      �� 5@      #��[ 6(   $��20��6Q   $��1��6]   $��1��6m   $��5 0 0 0q�6z   $��13(�6�   ��6�   �� *
Position+6�   5A      5B      5D      5E      �� 5F   
   $��2��6     CustomButton#��[ 6   $��Создать��6D   ��6V   �� *CreateGroupChat+��6z   ��  �� ElementName$ UsersListBox��6�   5G      5H      