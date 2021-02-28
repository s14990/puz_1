using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.Collections.ObjectModel;
using System.Threading;

namespace GameShowSimulation
{
    /// <summary>
    /// s14990, PUZ project, 9.1 simulation
    /// </summary>
    /// 

    //Class for storing results in a list
    public class ResultRow
    {
        public int id { get; set; }
        public bool ChangedChoice { get; set; }
        public string Result { get; set; }

        public ResultRow(int i,bool b,string r)
        {
            id = i;
            ChangedChoice = b;
            Result = r;
        }

        public override string ToString()
        {
            var tmp = ChangedChoice ? "Yes" : "No";
            return $"{id}     {tmp}     {Result}";
        }

    }

    public partial class MainWindow : Window
    {
        static string Win = "win";
        static string Lose = "lose";
        static string Question = "Are you sure you dont want to change your choice?";
        //static string WinCard = "Ferrari";
        static string WinCard = "WIN";
        static string EmptyCard = "Empty";
        static string WinMessage = "You Won";
        static string LoseMessage = "You Lost";
        static string ChooseCard = "Choose Random Card";


        private int priseId;
        private int firstChoice;
        int revealedCard;
        int runTimes = 0;
        int winTimes = 0;
        bool finishedGame = false;
        bool changedChoice = false;
        bool simulationRunning = false;
        bool simulationChange = false;
        List<int> emptyIds = new List<int>();
        Random random = new Random();
        List<Label> Cards = new List<Label>();

        public ObservableCollection<ResultRow> results = new ObservableCollection<ResultRow>();

        public MainWindow()
        {
            InitializeComponent();
            Cards.Add(Card_1);
            Cards.Add(Card_2);
            Cards.Add(Card_3);
            ResultsListView.ItemsSource = results;
            ResultsListView.DataContext = results;
            Init();
        }


        //clear previous data, select random number (1>= i < 4), mark other cards as empty and mask all cards
        private void Init()
        {
            emptyIds.Clear();
            revealedCard = 0;
            changedChoice = false;
            finishedGame = false;
            priseId = random.Next(1, 4);
            emptyIds.AddRange(Enumerable.Range(1, 3).Where(i => i != priseId));
            firstChoice = 0;
            Cards.ForEach(i => MaskCard(i));
            Result_Text.Text = ChooseCard;
        }


        private void Card_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (revealedCard != 1 && !finishedGame)
                ClickAction(1);
        }

        private void Card_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (revealedCard != 2 && !finishedGame)
                ClickAction(2);
        }

        private void Card_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (revealedCard != 3 && !finishedGame)
                ClickAction(3);
        }


        private void ClickAction(int id)
        {
            //if this is the first click
            //reveal random empty card if your original choice was win
            //or reveal other one is first choice is empty
            if (firstChoice == 0)
            {
                firstChoice = id;
                MarkCard(Cards[id - 1]);

                if (priseId == id)
                    revealedCard = emptyIds[random.Next(0, 2)];
                else
                    revealedCard = emptyIds.Where(i => i != id).First();

                UnmaskEmptyCard(Cards[revealedCard - 1]);
                //MessageBox.Show(Question);
                Result_Text.Text = Question;
            }
            //reveal chosen card and store results
            else
            {
                if (id != firstChoice)
                    changedChoice = true;
                runTimes += 1;
                finishedGame = true;
                if (priseId == id)
                {
                    UnmaskPriseCard(Cards[priseId - 1]);                    
                    winTimes += 1;
                    SaveResults(Win);
                    Result_Text.Text = WinMessage;
                    //MessageBox.Show(WinMessage);                   
                }
                else
                {
                    UnmaskEmptyCard(Cards[id - 1]);
                    SaveResults(Lose);
                    Result_Text.Text = LoseMessage;
                    //MessageBox.Show(LoseMessage);                  
                }

            }
        }

        //add results to the list and update text boxes
        private void SaveResults(string res)
        {
            results.Add(new ResultRow(runTimes, changedChoice, res));
            GameCount.Text = runTimes.ToString();
            double winChance = (double)winTimes / runTimes * 100;
            WinChance.Text = $"{Math.Round(winChance, 2)}%";
        }
        
        private void MaskCard(Label l)
        {
            l.Background = Brushes.Gray;
            l.Content = "";
        }

        private void UnmaskPriseCard(Label l)
        {
            l.Background = Brushes.Gold;
            l.Content = WinCard;
        }

        private void UnmaskEmptyCard(Label l)
        {
            l.Background = Brushes.Aqua;
            l.Content = EmptyCard;
        }

        private void MarkCard(Label l)
        {
            l.Background = Brushes.LightGray;
        }

        //restart_game
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void startSimulation()
        {
            int SpriseId;
            int Sid;
            bool Swin;
            string Sresult;
            List<int> SemptyIds = new List<int>();

            Task.Run(() =>
            {
                while (simulationRunning)
                {
                    //reduce overall simulation speed to make sure that ui is responsible
                    Thread.Sleep(10);
                    runTimes += 1;
                    SpriseId = random.Next(1, 4);
                    Sid = random.Next(1, 4);
                    //if you got it right in the first try, you are going to change choice later, you lost
                    //and if you got it wrong, other empty card revealed , you will choose correct one
                    //thats where 2/3 comes from, you can choose only 1 wrong card(correct choice) out of 3
                    if (simulationChange)
                        Swin = SpriseId == Sid ? false : true;
                    else
                        Swin = SpriseId == Sid ? true : false;
                    //other is simple, if you get it right you win 
                    Sresult = Swin ? Win : Lose;
                    if (Swin)
                        winTimes += 1;

                    //all actions on UI should be perfomed on the main thread
                    Application.Current.Dispatcher.Invoke(new Action(() => { SsaveResults(simulationChange, Sresult); }));
                }
            });
        }

        private void SsaveResults(bool c, string res)
        {
            results.Add(new ResultRow(runTimes, c, res));
            GameCount.Text = runTimes.ToString();
            double winChance = (double)winTimes / runTimes * 100;
            WinChance.Text = $"{Math.Round(winChance, 2)}%";
        }


        //start simulation and update checkbox based value
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeBox.IsChecked == null || ChangeBox.IsChecked == false)
                simulationChange = false;
            else
                simulationChange = true;
            if (simulationRunning)
            {
                simulationRunning = false;
                StartButton.Content = "Start";
            }
            else
            {
                simulationRunning = true;
                StartButton.Content = "Stop";
                startSimulation();
            }
        }

        //reset_click
        //clear result list, set both counters to 0 and textboxes to 0
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            runTimes = 0;
            winTimes = 0;
            results.Clear();
            GameCount.Text = "0";
            WinChance.Text = "0";
            Init();
        }
    }
}
