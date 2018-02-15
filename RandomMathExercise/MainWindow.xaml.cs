using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomMathExercise
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int num1, num2, num3, result;

        private Random random;
        
        public MainWindow()
        {
            InitializeComponent();

            CommandBinding binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += BindingOnExecuted;
            this.CommandBindings.Add(binding);

            SetNumbers(out num1, out num2, out num3);
            SetExerciseToTextBlock();
        }

        private void BindingOnExecuted(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            SetNumbers(out num1, out num2, out num3);
            SetExerciseToTextBlock();
            CongratsTextBlock.Visibility = Visibility.Hidden;
            ResultTextBox.Background = Brushes.BurlyWood;
            ResultTextBox.Text = String.Empty;
        }

        private void SetNumbers(out int n1, out int n2, out int n3)
        {
            random = new Random();
            n1 = random.Next(1, 50);
            Thread.Sleep(20);
            random = new Random();
            n2 = random.Next(1, 8);
            Thread.Sleep(20);
            random = new Random();
            n3 = random.Next(1, 101);
        }

        private bool CheckResultFromExercise(int n1, int n2, int n3)
        {
            int result;
            if (Int32.TryParse(ResultTextBox.Text, out result))
            {
                if (result == n1 * n2 + n3)
                    return true;
            }
            return false;
        }

        private void SetExerciseToTextBlock()
        {
            ExerciseTextBlock.Text = $"({num1} * {num2}) + {num3} =";
        }
        
        private void CheckButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckResultFromExercise(num1, num2, num3))
            {
                ResultTextBox.Background = Brushes.ForestGreen;
                CongratsTextBlock.Text = "You did a great job!";
                CongratsTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ResultTextBox.Background = Brushes.Brown;
                CongratsTextBlock.Text = "Try again!";
                CongratsTextBlock.Visibility = Visibility.Visible;
            }
            
        }

        private void ResultTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null) ((TextBox) sender).Background = Brushes.BurlyWood;
            CongratsTextBlock.Visibility = Visibility.Hidden;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CheckButton_OnClick(sender, e);
        }
    }
}
