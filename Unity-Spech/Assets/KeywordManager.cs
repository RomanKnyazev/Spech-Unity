using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.Scenes
{
    public class KeywordManager : MonoBehaviour
    {
        [SerializeField] private string[] _keywords;
        private KeywordRecognizer _recognizer;

        private void Start()
        {
            _recognizer = new KeywordRecognizer(_keywords);
            _recognizer.OnPhraseRecognized += OnPhraseRecognized;
            _recognizer.Start();
        }

        private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            Debug.Log(builder.ToString());
        }

        private void OnApplicationQuit()
        {
            if (_recognizer == null || !_recognizer.IsRunning) return;

            _recognizer.OnPhraseRecognized -= OnPhraseRecognized;
            _recognizer.Stop();
        }
    }
}
