using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Speech_RPG
{
    class Program
    {
        static SpeechRecognitionEngine engine;

        // make a player
        static player player = new player();
        // make an enemy
        static Enemy enemy = new Enemy();

        static List<string> speechQueue = new List<string>();

        static SpeechSynthesizer synth = new SpeechSynthesizer();
        

        static void Main(string[] args)
        {
            engine = new SpeechRecognitionEngine();
            // set audio input device to the default device
            engine.SetInputToDefaultAudioDevice();
            // define which words to look for
            engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "attack", "heal", "thot", "hoe", "oof", "this game is trash" }))));
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Engine_SpeechRecognized);


            // set audio output device to default device
            synth.SetOutputToDefaultAudioDevice();


            synth.SpeakAsync("you are faced with an enemy. defeat the enemy by saying \"attack\" or \"heal\"");
            //


            // keep open
            Console.ReadLine();

        }

        async static void Engine_SpeechRecognized(object ob, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.Result.Text);

            await CheckAnswer(e.Result.Text);

        }

        async static Task CheckAnswer(string text)
        {
            if (text == "thot" || text == "hoe")
            {
                synth.SpeakAsync("no u");
                await Task.Delay(1000);
            }
            if (text == "oof")
            {
                synth.SpeakAsync("owo");
                await Task.Delay(1000);
            }
            if(text == "this game is trash")
            {
                synth.SpeakAsync("yeah i know it was made in 3 hours");
                Console.WriteLine("¯\\_(ツ)_/¯ (shrug emoji but i can't be bothered to fix the middle char)");
                await Task.Delay(1000);
            }

            // heal player
            if (text == "heal")
            {
                player.Heal();
                synth.SpeakAsync("you were healed for 50. you now have " + player.GetLives() + " lives");
                await Task.Delay(1000);
            }

            // attack enemy
            if (text == "attack")
            {
                synth.SpeakAsync("the enemy took " + enemy.TakeDamage().ToString() + " damage");
                await Task.Delay(1000);

                if(enemy.GetLives() <= 0)
                {
                    synth.SpeakAsync("congratulations you defeated the enemy");
                    await Task.Delay(3000);
                    synth.SpeakAsync("terminating...");
                    await Task.Delay(5000);
                    Environment.Exit(420);
                }

                synth.SpeakAsync("the enemy now has " + enemy.GetLives().ToString() + " lives left");
                await Task.Delay(1000);

                synth.SpeakAsync("the enemy attacked back dealing " + player.TakeDamage().ToString() + " damage");
                await Task.Delay(1000);
                synth.SpeakAsync("you now have " + player.GetLives() + " lives left");
                await Task.Delay(1000);
                synth.SpeakAsync("Do you want to heal or attack?");
                await Task.Delay(1000);
            }
        }
    }
}
