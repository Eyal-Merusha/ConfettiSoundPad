using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NAudio.Wave;

namespace ConfettiSoundPad
{
    public partial class Menu : Page
    {
        // record sound
        private WaveInEvent waveIn;
        private WaveFileWriter waveFile;

        // play sound
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;

        private string filePath = @"C:\Users\17010\Desktop\sound.wav";

        public Menu()
        {
            InitializeComponent();

            // prepare to start recording
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;

            // prepare to play sound
            waveOut = new WaveOutEvent();
            
        }

        private void PlaySound_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (audioFileReader == null)
                {
                    audioFileReader = new AudioFileReader(filePath);
                    waveOut.Init(audioFileReader);
                }
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void RecordSound_Click(object sender, RoutedEventArgs e)
        {
            if (Record.Background == Brushes.LightGray) // start recording
            {
                waveFile = new WaveFileWriter(filePath, waveIn.WaveFormat); // prepare for recording

                waveIn.StartRecording();
                Record.Content = "Stop Recording";
                Record.Background = Brushes.Red;
            }
            else // stop recording
            {
                waveIn.StopRecording();
                Record.Content = "Start Recording";
                Record.Background = Brushes.LightGray;
            }
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            // cleare the wave file
            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }

            if (e.Exception != null)
            {
                MessageBox.Show($"An error occurred while recording: {e.Exception.Message}");
            }
        }
    }
}
