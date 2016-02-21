/*******************************************************************************
 * Copyright (c) 2015 Bo Kang
 *   
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *  
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Loader
{
    public class ProblemLoader
    {
        #region Static Load

        static ProblemLoader()
        {
            InitProblems();
        }

        private static List<MathProblem> Problems { get; set; }

        static void InitProblems()
        {
            Problems = new List<MathProblem> { Capacity = 1000 };
            for (int i = 0; i < 1000; i++)
            {
                Problems.Insert(i, null);
            }            
            LoadProblemFile();
        }

        static void LoadProblemFile()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var resourceName = "Loader.Problems.json";
            using (Stream stream = currentAssembly.GetManifestResourceStream(resourceName))
            using (var file = new StreamReader(stream))
            using (var reader = new JsonTextReader(file))
            {
                var jProblems = (JArray)JToken.ReadFrom(reader);

                foreach (var jToken in jProblems)
                {
                    var jo = (JObject)jToken;
                    if (jo == null) continue;

                    var problemIndex = jo["id"].Value<int>();
                    var problemDescription = jo["problem"].Value<string>();
                    var problemtype = jo["type"].Value<string>();
                    var pt = (MathProblem.ProblemType)Enum.Parse(typeof(MathProblem.ProblemType), problemtype);
                    var mp = new MathProblem(problemIndex, problemDescription, pt);

                    #region Syntax Loading
                    //Problem Tagging Information
                    var tagArray = jo["syntaxTagging"].Value<JArray>();
                    if (tagArray == null) continue;

                    foreach (var tagToken in tagArray)
                    {
                        var tagO = (JObject)tagToken;
                        if (tagO == null) continue;

                        var tagIndex = tagO["id"].Value<int>();
                        var tagStr = tagO["tag"].Value<string>();
                        var tagInteractType = tagO["interaction"].Value<string>();
                        var it =
                            (ProblemWord.InteractionType)
                                Enum.Parse(typeof(ProblemWord.InteractionType), tagInteractType);

                        var pw = new ProblemWord(tagIndex, tagStr, it);

                        if (it == ProblemWord.InteractionType.Input)
                        {
                            pw.DragObject = pw.Word;
                            if (tagO["input"] != null) // kr of input knowledge
                            {
                                var inputFormat = tagO["input"].Value<string>();
                                if (inputFormat != null)
                                {
                                    pw.DragObject = inputFormat;
                                }
                            }

                            //pw.PQType = ProblemWord.ProblemQuestionType.Problem;
                            //TODO
                        }
                        else if (it == ProblemWord.InteractionType.Concept)
                        {
                            var conceptFormat = tagO["concept"].Value<string>();
                            if (conceptFormat != null)
                            {
                                object explain = GeometryConcept.
                                    ConceptCrawler.Instance.RetrieveConceptExplain(conceptFormat);
                                string concept = "Concept: " + conceptFormat;
                                if (explain != null)
                                {
                                    pw.DragObject = new Tuple<string, string>(concept, (string)explain);
                                }
                                else
                                {
                                    pw.DragObject = new Tuple<string, string>(conceptFormat, "TODO");
                                }
                            }
                        }
                        else if (it == ProblemWord.InteractionType.Hybrid)
                        {
                            var conceptFormat = tagO["concept"].Value<string>();
                            object conceptTuple1 = null;
                            if (conceptFormat != null)
                            {
                                object explain = GeometryConcept.
                                   ConceptCrawler.Instance.RetrieveConceptExplain(conceptFormat);
                                string concept = "Concept: " + conceptFormat;
                                if (explain != null)
                                {
                                    conceptTuple1 = new Tuple<string, string>(concept, (string)explain);
                                }
                                else
                                {
                                    conceptTuple1 = new Tuple<string, string>(conceptFormat, "TODO");
                                }
                            }

                            object inputTuple2 = null;
                            inputTuple2 = pw.Word;
                            if (tagO["input"] != null) // kr of input knowledge
                            {
                                var inputFormat = tagO["input"].Value<string>();
                                if (inputFormat != null)
                                {
                                    inputTuple2 = inputFormat;
                                }
                            }
                            pw.DragObject = new Tuple<object, object>(conceptTuple1, inputTuple2);
                        }
                        mp.Words.Add(pw);
                    }
                    #endregion
                 
                    Problems.Insert(mp.Index-1, mp);
                }
            }
        }

        #endregion

        #region Problem Specifics

        public ProblemLoader(int index)
        {
            Load(index);
        }

        public int CurrentProblemIndex { get; set; }

        public MathProblem CurrentProblem { get; set; }

        private void Load(int index)
        {
            CurrentProblemIndex = index - 1;
            CurrentProblem = Problems[CurrentProblemIndex];
        }

        public MathProblem LoadNext()
        {
            MathProblem mp;
            if (CurrentProblemIndex == Problems.Count - 1)
            {
                mp = Problems[CurrentProblemIndex];
                CurrentProblemIndex = 0;
            }
            else
            {
                mp = Problems[CurrentProblemIndex++];
            }
            return mp;
        }

        private int GenerateRandomProblemIndex()
        {
            var r = new Random();
            int rInt = r.Next(1, Problems.Count);
            return rInt;
        }

        public void LoadRandomProblem()
        {
            while(true)
            {
                int index = GenerateRandomProblemIndex();

                if (Problems[index - 1] != null)
                {
                    CurrentProblemIndex = index - 1;
                    CurrentProblem = Problems[index - 1];
                    return;
                }
            }
        }

        #endregion
    }
}
