using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfettiSoundPad
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private System.Media.SoundPlayer player;

        public Menu()
        {
            InitializeComponent();

            this.player = new System.Media.SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\sounds\sound.wav");
        }

        private void PlaySound_Click(object sender, RoutedEventArgs e)
        {
            this.player.Play();
        }
    }
}
