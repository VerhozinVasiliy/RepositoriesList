   M S B A M L   `   `   `  ����J  FLookingForRepos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:LookingForRepos.ViewLookingForRepos.View  5   	   %    LookingForRepos.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3view"clr-namespace:LookingForRepos.View     5   	   -   5          �Name$  MyMainWindow��5   	    � Title$# Репозитории GitHub��5   	   $	��768��6$   $
��1024��61    � WindowStartupLocation$ CenterScreen=�5	   	   $��14(�6.    � 	Resources 5
      2   ,LookingForRepos.View.IncrementIndexConverter   IncrementIndexConverter��%   &	          5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��70 �6   6*   �� 5      $��50 �6   6*   �� 5      6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��10q�6   $��40��6%    �� TextAlignment$ Center=�61   (&Введите слова поиска6^   �� 5      $��10q�6   $	��350��6"   $��40��6.   .��6;   ��5      �� 5       �� Path$ 
TextToFind�6    �� UpdateSourceTrigger$ PropertyChanged=�60   6X   .��5       	 J� InputBindings	 5      �� 5      

 �� Key$
 Enter��6J    �� Command 6!   �� $ LoadDataCommand�6J   .��5   )   5      �� 5      $/��)Загрузить репозитории��6   $��
10,10,10,0q�65   $
��Left=�6I   $	��Top=�6d   $	��274��6|   $��50��6�   ��5       �� *LoadDataCommand+{�5!      �� *IsButtonEnables+5"      � 5#   
   $��1��6   .+�5$      �� .+�5%      �� $��10, 0, 0, 0q�6<   
���  ����6   �� *ProgressorLabel+6Q   � 5&       � IsIndeterminate . $��10, 0, 10, 5q�5(   >   $��30��6f   y�6   �� *ProgressorVisible+6f   5)      5+      � 5,   
   $��2��6   .+�5-      &   System.Windows.Controls.DataGrid  -   5          Name$ RepoDataGrid��5.      $��
10,0,10,55q�5/         GridLinesVisibility$
 None=�50         AreRowDetailsFrozen .    SelectionMode$ Single=�52         CanUserReorderColumns .     
IsReadOnly .    HeadersVisibility$ Column=�55         AutoGenerateColumns .   �� SelectedIndex$ 0��58   :   $��2��6L   ��57      �� *MainRepoList+y�58      �� *IsDataGridVisible+   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ Наименование��6B      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Name+6X     5;      $ Описание��6I   $ * 6[    6)   �� *Description+6[     5<      $ 
Автор��6C   $ * 6R    6)   �� *Login+6R   4  .System.Windows.Controls.DataGridTemplateColumn  5=      $ Аватар��6-   $ * 6=      CellTemplate 5>      �� 5?      .R�5@   "   �� $��75��6?   $��75��6N   ��6(   �� *Ava+6N   5A      5C      5  /System.Windows.Controls.DataGridHyperlinkColumn  5D      $ Ссылка��6L   $ * 6\    6.   �� *	Reference+   ElementStyle 5E      �� 5F       �� 
TargetType" ��6$   .�5G   "   -   5        5H      5J        5K      $ Язык��6F   $ * 6T    6)   �� *
Language+6T     5L      $ Звезды��6H   $ * 6X    6)   �� *
StarsCount+6X     5M      $ 
Форки��6G   $ * 6V    6)   �� *	ForkCount+6V     5N      $! Дата изменения��6e   $ * 6}    6)   �� *DateLastUpdate+ �� StringFormat$ 
dd.MM.yyyy��6}   5P      �� 5Q      $��	10,0,0,10q�6   $��Bottom=�5R   :   $
��Left=�6U   $��29��6p   y�6   �� *IsDataGridVisible+��5S      `� 5T      $" Запись №{0} из {1}��6#   .1�5U      �� $ SelectedIndex�6"    �� 	Converter# [  67    �� ElementName$ RepoDataGrid��6l   �� 5V      $ Items.Count�6"   $ RepoDataGrid��65     �� Mode$  OneWay=�6P   5W      5Y      5Z      5[      5\      