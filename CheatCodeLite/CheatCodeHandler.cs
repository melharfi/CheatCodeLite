using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CheatCodeLite
{
    public class CheatCodeHandler
    {
        readonly double interval;
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

        // add new cheat code pattern
        public void AddChainePattern(ChainePattern cp)
        {
            // some checks
            foreach (ChainePattern current in availablePatterns)
            {
                // check against fact that passed pattern will hide some existed pattern line new pattern "ABC" will hide an existing pattern like "ABCD" cause the older one will never be triggered
                if (cp.Count() < current.Count())
                {
                    int[] newCp = new int[cp.Count()];
                    Array.Copy(current.Pattern, 0, newCp, 0, current.Count() - 1);
                    if (cp.Pattern.SequenceEqual(newCp))
                        throw new Exception("Duplication found, new pattern \"" + cp.Alias + "\" will hide \"" + current.Alias + "\" alreay existed");
                }
                // check against fact that passed pattern will be hidden by an existing pattern line new pattern "ABCD" will be hidden by an existing pattern like "ABC" will hide new one "ABCD"
                else if (cp.Count() > current.Count())
                {
                    int[] newCp = new int[current.Count()];
                    Array.Copy(cp.Pattern, 0, newCp, 0, cp.Pattern.Length - 1);
                    if (current.Pattern.SequenceEqual(newCp))
                        throw new Exception("Duplication found, the already registred pattern \"" + current.Alias + "\" will hide the new one \"" + cp.Alias + "\"");
                }
                // check if dupplication exist
                else if (cp.Count() == current.Count())
                {
                    if (current.Pattern.SequenceEqual(cp.Pattern))
                        throw new Exception("Pattern \"" + cp.Alias + "\" already exist");
                }
            }

            availablePatterns.Add(cp);
            matchedPatterns.Clear();
            matchedPatterns = new List<ChainePattern>(availablePatterns);
        }

        int[] patternInProgress;
        /// <summary>
        /// Pass a keystoke handled by user side using either windows event like KeyPress or a third party
        /// </summary>
        /// <param name="keyValue">KeyValue</param>
        public void AddKeystroke(int keyValue)
        {
            if (patternInProgress == null)
            {
                // check if any pattern start with the given key
                if(availablePatterns.Exists(f => f.Pattern[0].Equals(keyValue)))
                {
                    //Pattern match found in the first index
                    patternInProgress = new[] { keyValue };
                    stopWatch.Restart();
                }
                return;
            }
            else
            {
                // check if timeup to restart listening
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalMilliseconds > interval)
                {
                    patternInProgress = new[] { keyValue };
                    stopWatch.Restart();
                    return;
                }
                // construct new pattern, add new keystroke to the patternInProgress
                patternInProgress = patternInProgress.Concat(new [] { keyValue }).ToArray();

                // control variable
                //bool found = false;

                // check if a pattern contain new currentPattern
                // check for
                //if (availablePatterns.FindAll(f => f.Pattern.Length >= patternInProgress.Length && f.Pattern.ToList().GetRange(0, patternInProgress.Length).ToArray().SequenceEqual(patternInProgress)).Any())
                //{
                //    found = true;
                //}


                //if (!found)
                //{
                //    // if no match then reset patternInProgress and add new keystroke
                //    patternInProgress = new[] { keyValue };
                //}
                //else
                //{
                //    // check if it match any pattern
                //    var matched = matchedPatterns.Find(f => f.Pattern.SequenceEqual(patternInProgress));
                //    if(matched != null)
                //    {
                //        PatternEventHandler patternEventHandler = new PatternEventHandler(matched);
                //        // trigger event
                //        OnAddedCheatCodeHandler(patternEventHandler);
                //        stopWatch.Stop();
                //        patternInProgress = null;
                //    }
                //}


                if (availablePatterns.FindAll(f => f.Pattern.Length >= patternInProgress.Length && f.Pattern.ToList().GetRange(0, patternInProgress.Length).ToArray().SequenceEqual(patternInProgress)).Any())
                {
                    // check if it match any pattern
                    var matched = matchedPatterns.Find(f => f.Pattern.SequenceEqual(patternInProgress));
                    if (matched != null)
                    {
                        PatternEventHandler patternEventHandler = new PatternEventHandler(matched);
                        // trigger event
                        OnAddedCheatCodeHandler(patternEventHandler);
                        stopWatch.Stop();
                        patternInProgress = null;
                    }
                }
                else
                {
                    patternInProgress = new[] { keyValue };
                }



            }
        }

    }
}
