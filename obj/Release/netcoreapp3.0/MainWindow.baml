   M S B A M L   `   `   `  ���� M  IGameShowSimulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GameShowSimulationGameShowSimulation  #    GameShowSimulation.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:GameShowSimulation       � Title$  GameSimulation��$	��500��$
��1000��.���  � ColumnDefinitions �� $��40 ��� �� $��20 ��� �� $��20 ��� �� $��40 ��� �� $��50 ��� $��20 � � RowDefinitions �� $��40 ��� $��20 ��� �� $��30 ��� $��40 ��� $��20 �.+��� -   $��Card_1��$��Center=�$��1��$��2��
����� �$��Center=��� -   $��Card_2��$��Center=�$��3��$��2��
����� �$��Center=��� -   $��Card_3��$��Center=�$��5��$��2��
����� �$��Center=��� -   $��Result_Text��$��1��$
��Left=�$��4��$��	TextBlock��$
��Wrap=��� -   $��Restart Game��$��3��$��Center=�$��4��$��Center=�$��21��$��82���� $��7��$
��Left=�$��3��$	��Top=�$��Games: ��$	��Top=��� -   $��	GameCount��$��8��$��Center=�$��3��$��0��$
��Wrap=�$��Center=��� $��Win %��$��7��$
��Left=�$��4��$	��Top=��� -   $��	WinChance��$��8��$��Center=�$��4��$��0��$
��Wrap=�$��Center=��� -   $��ResultsListView��$��7��$��1��$��2�� �� View �� .P���  �� Width$ 70��$��Id�� �� DisplayMemberBinding ��  �� Path$ id��� $ 55��$��Changed�� �� $ ChangedChoice��� $ 55��$��Result�� �� $ Result��� -	   $��StartButton��$��Start Simulation��$��7��$
��Left=�$��88,0,0,0q�$��Center=�$��20��$��90��$��2���� -
   $��	ChangeBox��$��ChangeChoice��$��6��$
��Left=�$��14,0,0,0q�$��Center=�$��2���� -   $��Reset��$��5��$��Center=�$��4��$��Center=�$��20��$��59��