using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

namespace Assets.Scenes
{
    public class DictationManager : MonoBehaviour
    {
        [SerializeField]
        private Text _hypotheses;

        [SerializeField]
        private Text _recognitions;

        private DictationRecognizer _dictationRecognizer;

        public DictationManager(Text recognitions, Text hypotheses)
        {
            _recognitions = recognitions;
            _hypotheses = hypotheses;
        }

        private void Start()
        {
            _dictationRecognizer = new DictationRecognizer();

            _dictationRecognizer.DictationResult += (text, confidence) =>
            {
                Debug.LogFormat("Dictation result: {0}", text);
                _recognitions.text += text + "\n";
            };

            _dictationRecognizer.DictationHypothesis += (text) =>
            {
                Debug.LogFormat("Dictation hypothesis: {0}", text);
                _hypotheses.text += text;
            };

            _dictationRecognizer.DictationComplete += (completionCause) =>
            {
                if (completionCause != DictationCompletionCause.Complete)
                    Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
            };

            _dictationRecognizer.DictationError += (error, hresult) =>
            {
                Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
            };

            _dictationRecognizer.Start();
        }
    }
}