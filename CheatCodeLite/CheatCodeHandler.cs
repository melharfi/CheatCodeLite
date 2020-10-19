using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CheatCodeLite
{
    public class CheatCodeHandler : IEnumerable<int>
    {
        readonly double interval;
        int index;
        List<ChainePattern> matchedPatterns = new List<ChainePattern>();
        readonly List<ChainePattern> availablePatterns = new List<ChainePattern>();
        readonly Stopwatch stopWatch = new Stopwatch();
        public event EventHandler<PatternEventHandler> AddedPattern;
        public CheatCodeHandler(double keystrokesInterval)
        {
            this.interval = keystrokesInterval;
        }

        protected virtual void OnAddedCheatCodeHandler(PatternEventHandler e)
        {
            AddedPattern?.Invoke(this, e);
        }

        public void AddChainePattern(ChainePattern cp)
        {
            foreach (ChainePattern current in availablePatterns)
            {
                if (cp.Count() < current.Count())
                {
                    int[] newCp = new int[cp.Count()];
                    Array.Copy(current.Pattern, 0, newCp, 0, current.Count() - 1);
                    if (cp.Pattern.SequenceEqual(newCp))
                        throw new Exception("Duplication found, new pattern \"" + cp.Alias + "\" will hide \"" + current.Alias + "\" alreay existed");
                }
                else if (cp.Count() > current.Count())
                {
                    int[] newCp = new int[current.Count()];
                    Array.Copy(cp.Pattern, 0, newCp, 0, cp.Pattern.Length - 1);
                    if (current.Pattern.SequenceEqual(newCp))
                        throw new Exception("Duplication found, the already registred pattern \"" + current.Alias + "\" will hide the new one \"" + cp.Alias + "\"");
                }
                else if (cp.Count() == current.Count())
                {
                    if (current.Pattern.SequenceEqual(cp.Pattern))
                        throw new Exception("Pattern \"" + cp.Alias + "\" already exist");
                }
            }

            availablePatterns.Add(cp);
        }
        void Clear()
        {
            index = 0;
            stopWatch.Stop();
            matchedPatterns = new List<ChainePattern>(availablePatterns);
        }
        public void RegisterKeystroke(int key)
        {
        MatchLabel:
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalMilliseconds > interval)
                {
                    Clear();
                    goto MatchLabel;
                }
            }
            else
                stopWatch.Start();

            bool isMatched = false;
            for (int cnt = matchedPatterns.Count; cnt > 0; cnt--)
            {
                if (matchedPatterns[cnt - 1].Pattern[index] == key)
                {
                    isMatched = true;
                }
                else
                {
                    matchedPatterns.RemoveAt(cnt - 1);
                }
            }

            if (!isMatched)
            {
                Clear();
                goto MatchLabel;
            }
            else
            {
                if (matchedPatterns.Count == 0)
                {
                    throw new Exception("Code should not be reached");
                }
                else
                {
                    foreach (ChainePattern current in matchedPatterns)
                    {
                        if (current.Count() == index + 1)
                        {
                            PatternEventHandler patternEventHandler = new PatternEventHandler(current);
                            OnAddedCheatCodeHandler(patternEventHandler);
                            Clear();
                            return;
                        }
                    }
                    index++;
                }
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
