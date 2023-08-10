using Google.Cloud.Speech.V1;
using System;
using System.Net.Http;
using System.Windows;

using System.Threading.Tasks;
using System.Text;

namespace Chat_Gpt_sesli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();

        }
        private async void Start()
        {
            string audioFilePath = @"audio.wav";

            // Create a SpeechClient object using your Google Cloud credentials
            SpeechClientBuilder builder = new SpeechClientBuilder();
            builder.CredentialsPath = @"C:\Users\nihad\Downloads\myid.json";
            SpeechClient speechClient = builder.Build();

            // Create a recognition config object
            RecognitionConfig config = new RecognitionConfig();
            config.Encoding = RecognitionConfig.Types.AudioEncoding.Linear16;
            config.SampleRateHertz = 48000;
            config.LanguageCode = "tr-TR";

            // Create a recognition audio object from the audio file
            RecognitionAudio audio = RecognitionAudio.FromFile(audioFilePath);

            // Call the Speech-to-Text API to transcribe the audio
            RecognizeResponse response = speechClient.Recognize(config, audio);
            StringBuilder strb = new StringBuilder();
            // Print the transcribed text to the console
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    strb.Append(alternative.Transcript);
                    MessageBox.Show($"Transcription: {alternative.Transcript}");
                }
            }
            //////////////////

        }

    }
}
