   M S B A M L   `   `   `  ����E  AFuelBudget, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:FuelBudget.ViewFuelBudget.View  5   	   ;"clr-namespace:FuelBudget.ViewModelFuelBudget.ViewModel  5   	   ;"clr-namespace:FuelBudget.ConvertorFuelBudget.Convertor  5	   	        FuelBudget.View.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:FuelBudget.View     5   	   3view"clr-namespace:FuelBudget.ViewModel     5   	   6convert"clr-namespace:FuelBudget.Convertor     5	   	     � Title$  
MainWindow��5   	   $	��550��6   $	��900��6)    � 	Resources 5         FuelStaticList S� 
ObjectType    FuelBudget.View.MainWindow S� 
MethodName8   2FuelBudget.Convertor.EmptyIntToVisibilityConverter ! EmptyIntToVisibilityConverter��%S   &	        &	 ,     S� 5   
   "  64   $ GetFuels��6[     5   
   60   5       � DataContext 5      (   "FuelBudget.ViewModel.MainViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   6-   �� 5      6!   .+�5   
   � $��0��6   .+�5      �� .+�5      (  "System.Windows.Controls.DatePicker  $��10q�6      SelectedDate 6)   �� *SelectedDate+�� 5      $��10q�6   ��6%   �� *SaveCommand+.��6E   Save6K   5      5       &   System.Windows.Controls.DataGrid  5"   
   $��1��6      AutoGenerateColumns$ False��5#         RowDetailsVisibilityMode$ Visible=�5$      	   CanUserAddRows$
	 True��5%      ��5"   #   �� *DepartmentButgetList+
 �� Mode$
 TwoWay=�   RowStyle 5'      �� 5(       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5)      ��  �� Property � |� �� Value$ False��62    �� Triggers 5*      P� 5+       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5,      ��  � ��
 ����6P   5-      5/         Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 61   �� *Id+4
  .System.Windows.Controls.DataGridTemplateColumn
  53      $ Департамент��6)    
  CellTemplate 54      �� 55      .R�56      �� ��6(   �� *Department.Name+57       
  CellEditingTemplate 59      �� 5:      .R�5;      �� $|�False��6'    �� DisplayMemberPath$
 Name��5>   '   ��5<   !   �� *DataContext.DepatrmentList+ �� RelativeSource ��  �� AncestorType
 Window �� SelectedItem 5=   '   �� *
Department+ �� UpdateSourceTrigger$ PropertyChanged=�5>   '   5?      5A        5B      $! Стоимость-План��6%      
IsReadOnly$
 True��6=    6O   �� *GetAllPlanCost+  5C      $! Стоимость-Итог��6%   $
 True��6=    6O   �� *GetAllFactCost+5D      
  5E      $  ��6)    5F      �� 5G      .R�5H      �� $��Удалить��6%   
���  ����67   �� *%#DataContext.DeleteDepartmantCommand+ �� 
 Window��5I   1   �� y�5J   1   �� *Id+ �� 	Converter# [  �� FallbackValue$ 	Collapsed��5L      5N         RowDetailsTemplate 5P      �� 5Q      .R�5R        $ False��5S      $
	 True��5T      ��5R      �� *FuelDetails+$
 TwoWay=� 5V      �� 5W      "  6$   .�5X   "   ��  � |�$ False��6>    5Y   "   P� 5Z   &    �@J�IsMouseOver$
 True��6E   . �5[   *   ��  � ��
 �����6\   5\   '   5^       5`        5a      $ Id��61    6=   �� *Id+
  5b      $ Топливо��65    5c   "   �� 5d   &   .R�5e   *   �� ��64   �� *	Fuel.Name+5f   '    5h   "   �� 5i   &   .R�5j   *   �� $
 Name��5l   3   ��5j   3   �� *DataContext.FuelList+ �� 
 Window 5k   3   �� *Fuel+$ PropertyChanged=�5l   3   5m   '   5o        5p      $ Литры-План��61    6E   �� *
VolumePlan+  5q      $ Цена-План��61    6D   �� *FuelPlanCost+  5r      $ Лиры-Факт��61    6D   �� *
VolumeFact+  5s      $ Цена-Факт��61    6D   �� *FuelFactCost+
  5t      $  ��65    5u   "   �� 5v   &   .R�5w   *   �� $��Удалить��61   ��6C   �� *DataContext.DeleteCommand+ �� 
 Window��5x   1   �� y�5y   1   �� *Id+# [ $ 	Collapsed��5{   '   5}      5      5�      5�      5�      5�      