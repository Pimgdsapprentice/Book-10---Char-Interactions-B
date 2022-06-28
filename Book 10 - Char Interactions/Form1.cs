using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Dynamic;

namespace Book_10___Char_Interactions
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        public void qTru()
        {
            richTextBox1.AppendText("True.\n");
        }
        public void qFal()
        {
            richTextBox1.AppendText("False.\n");
        }
        public void qBOutput(bool s)
        {
            richTextBox1.AppendText(s + "\n");
        }
        public void qOutput(string s)
        {
            richTextBox1.AppendText(s + "\n");
        }
        public void qIOutput(int s)
        {
            richTextBox1.AppendText(s.ToString() + "\n");
        }
        public string qIFor(int[] array)
        {
            string output = "";
            for(int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    output = array[i].ToString();
                }
                else
                {
                    output = output + ", " + array[i].ToString();
                }
            }
            return output;
        }
        public string AarryToStr(string[] ary, int spacer)
        {
            string spacer2 = "";
            if (spacer == 1)
            {
                spacer2 = ", ";
            }

            string output = "";
            for (int i = 0; i < ary.Length; i++)
            {
                if (i == 0)
                {
                    output = ary[i].ToString();
                }
                else
                {
                    output = output + spacer2 + ary[i].ToString();
                }
            }
            return output;
        }



        string[] comboBoxChoi = new string[] { "Generate NPCS", "b", "c" };

        //Debug
        // 1 = Debug On
        public int[] debug = { 0, 0, };
        // What each Debug Element is for
        public string[] debugElement = { "NPC Generation", "NPC Obs" };
        public int[] gameOutput = { 1, 0 };
        public string[] gameOutputEle = { "Encounters" };
        
        public void debugOut(string output, int debugiD)
        {
            if (debug[debugiD] == 1)
            {
                richTextBox1.AppendText(output + "\n");
            }
        }
        public void gameOut(string output, int gameOutputiD)
        {
            if (gameOutput[gameOutputiD] == 1)
            {
                richTextBox1.AppendText(output + "\n");
            }
        }



        

        public class events
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            //Intiated, Ongoing, Successful, Failed, Cancelled
            public string State { get; set; }
        }

        public string[][] eventlist = new string[][] { 
            //Id, Event Name, Description
            new string[] {"0", "Hunt", "A Hunt for Unspecified creature"}
        };


        public string[] cultureMotifs = new string[] { };
        //Cultures
        public class cultures
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int[] Motifs { get; set; }
            public int[] Festivals { get; set; }
        }

        //Pre NPC Functions
        /// <summary>
        /// 0: like - dislike
        /// 1: respect - disrespect
        /// 2: trust - distrust
        /// </summary>
        public class NPRelation
        {
            public int ID { get; set; }
            public int[] scores = { 100, 100, 100, 0};
        }
        /*
            string[][] terms = new string[][] {
                new string[] { "hates", "dislikes", "is ambivalent towards", "likes", "likes deeply" },
                new string[] { "disrespects", "looks down on", "is indifferent towards", "respects", "respects deeply"},
                new string[] { "distrusts", "is suspicious of", "is cautious of", "trusts", "trusts deeply"}
            };
        */
        public void nPCRelatOut(NPC n, int relation)
        {
            int[] ranges = new int[] { -150, -20, 20, 150};
            string[][] terms = new string[][] {
                new string[] { "Liking: ", "Respect: ", "Trust: "},
                new string[] { "Hates", "Dislikes", "Ambivalence", "Likes", "Strongly Likes"},
                new string[] { "Disrespects", "Disdain", "indifference", "Respect", "Deeply Respects"},
                new string[] { "Distrusts", "Suspicious", "Cautious", "Trusts", "Deeply Trusts"}
            };
            qOutput(n.Name + "'s perception of " + NPCs[n.NPcRelations[relation].ID].Name);
            //for (int i = 0; i < n.NPcRelations[relation].scores.Length; i++)
            for (int i = 0; i < 3; i++)
            {
                int z = 0;
                int y = n.NPcRelations[relation].scores[i];
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (y < ranges[j])
                    {
                        z = j;
                        break;
                    }
                }
                qOutput(terms[0][i] + terms[i + 1][z]);
            }
        }
        string[] nPCEmotion = new string[] { "Happy", "Sad" };
        string[] nPCEmotBuffsDef = new string[] { "Generic Celebration" };
        int[][] nPcEmotBuffs = new int[][] {
            //BuffID, Emotion, Weight, Duration
            new int[] {0, 0, 50, 0 }
        };
        public class nPCSkill
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int exp { get; set; }
        };

        //NPC Functions
        public class NPC
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
            public string Sex { get; set; }

            /*
            public List<int[]>  Emotions = new List<int[]> { new int[] { 0, 50 } };
            public List<int[]> EmotBuffs = new List<int[]> { new int[] { 0, 0, 50, 0 } };
            */
            public List<int[]> Emotions { get; set; }
            public List<int[]> EmotBuffs { get; set; }
            public int[] locationID { get; set; }
            public int[] oceanPer = new int[] { 50, 50, 50, 50, 50 };
            public int[] mbtiPer = new int[] { 0, 0, 0, 0, 0 };
            public string[] mbtiPerA = new string[] { "u", "u", "u", "u", "u" };
            public int mbtiPerNum { get; set; }
            public List<int> npcPhoneBk { get; set; }
            public List<NPRelation> NPcRelations { get; set; }
            public List<nPCSkill> nPCSkills = new List<nPCSkill> { };

        }
        List<NPC> NPCs = new List<NPC>();
        int npcIndex = -1;
        int observedNPC = -1;
        List<int> observedNPCS = new List<int>();
        public void npcOceanRead(int npc)
        {
            string[][] npcOceanDef = new string [][]{ 
                new string[] {"Practical", "Somewhat Practical", "Modest Curiosity", "Somewhat Curious", "Curious"},
                new string[] {"Impulsive", "Somewhat Impulsive", "Modest Reliability", "Somewhat Reliable", "Reliable" },
                new string[] {"Quiet", "Somewhat Quiet", "Moderate Extraversion", "Somewhat Outgoing", "Outgoing" },
                new string[] {"Guarded", "Somewhat Guarded", "Moderate Empathy", "Somewhat Empathetic", "Empathetic" },
                new string[] { "Calm", "Somewhat Calm", "Moderate Anxiety", "Somewhat Anxious", "Anxious"}
                };
            int[] npcOceanRanges = new int[] { 20, 40, 60, 100 };

            NPC nPC = NPCs[npc];
            for (int trait = 0; trait < nPC.oceanPer.Length; trait++)
            {
                for (int range = 0; range < npcOceanRanges.Length; range++)
                {
                    if (nPC.oceanPer[trait] <= npcOceanRanges[range])
                    {
                        richTextBox1.AppendText(npcOceanDef[trait][range] + "\n");
                        break;
                    }
                }
            }
        }
        public void npcMBTIRead(int npc)
        {
            NPC nPC = NPCs[npc];
            string output = nPC.mbtiPerA[0] + nPC.mbtiPerA[1] + nPC.mbtiPerA[2] + nPC.mbtiPerA[3] + "-" + nPC.mbtiPerA[4];
            richTextBox1.AppendText(output + "\n");
        }
        public void npcSkillRead(int npc)
        {
            NPC nPC = NPCs[npc];
            if (nPC.nPCSkills.CountBook 10 == 0)
            {
                qOutput(nPC.Name + " has no skills.");
            }


        }
        public int randomNPC()
        {
            int index = rnd.Next(NPCs.Count);
            debugOut(NPCs[index].ID.ToString(), 1);
            return NPCs[index].ID;
        }
        public void OceantoMBTI(int OceanIDX, int MBTiIDX, NPC nPC)
        {
            if (nPC.oceanPer[OceanIDX] > 50)
            {
                nPC.mbtiPer[MBTiIDX] = 1;
            }
        }
        public bool nPCHasMet(NPC npc1, NPC npc2)
        {
            if (npc1.npcPhoneBk.Contains(npc2.ID) == true)
            {
                return true;
            }
            return false;
        }
        public int nPCInteractChem(NPC npc1, NPC npc2)
        {
            int[][] mbtiRelations2 = new int[][]
            {
	            //Type: Good Relation --> Bad Relation
	            new int[] {15, 3, 5, 10, 8, 1, 7, 12, 4, 6, 13, 0, 14, 9, 15, 11, 2},
                new int[] {14, 2, 0, 14, 9, 11, 4, 6, 5, 13, 7, 1, 12, 8, 15, 10, 3},
                new int[] {6, 10, 8, 3, 5, 4, 12, 14, 1, 13, 6, 0, 15, 7, 9, 2, 11},
                new int[] {7, 11, 13, 2, 4, 9, 0, 15, 12, 14, 7, 6, 1, 5, 8, 3, 10},
                new int[] {9, 5, 3, 7, 14, 1, 10, 12, 0, 2, 9, 11, 6, 15, 8, 13, 4},
                new int[] {11, 7, 1, 14, 12, 5, 0, 11, 8, 3, 13, 2, 4, 9, 10, 15, 6},
                new int[] {0, 12, 14, 3, 5, 8, 10, 7, 9, 11, 0, 6, 2, 1, 15, 4, 13},
                new int[] {2, 14, 12, 7, 1, 10, 8, 9, 5, 11, 0, 2, 13, 4, 3, 6, 15},
                new int[] {13, 1, 7, 8, 10, 13, 5, 3, 14, 6, 11, 4, 12, 15, 2, 9, 0},
                new int[] {12, 0, 2, 11, 9, 4, 6, 15, 3, 12, 7, 5, 13, 14, 10, 8, 1},
                new int[] {4, 8, 10, 1, 7, 12, 14, 6, 15, 4, 3, 13, 11, 2, 5, 0, 9},
                new int[] {5, 9, 15, 6, 0, 13, 11, 14, 5, 2, 7, 10, 12, 3, 4, 1, 8},
                new int[] {8, 4, 6, 13, 15, 2, 1, 0, 11, 8, 3, 10, 7, 14, 9, 12, 5},
                new int[] {10, 6, 4, 15, 13, 9, 0, 2, 3, 1, 5, 10, 8, 11, 12, 14, 7},
                new int[] {1, 13, 11, 4, 2, 15, 9, 10, 8, 6, 1, 7, 14, 0, 3, 5, 12},
                new int[] {3, 15, 9, 6, 0, 13, 11, 4, 10, 3, 8, 12, 5, 1, 2, 7, 14}
            };

            for (int type = 0; type < mbtiRelations2.Length; type++)
            {
                if (npc1.mbtiPerNum == mbtiRelations2[type][0])
                {
                    for (int type2 = 1; type2 < mbtiRelations2[type].Length; type2++)
                    {
                        if (npc2.mbtiPerNum == mbtiRelations2[type][type2])
                        {
                            return type2;
                        }
                    }
                }
            }
            return -1;
        }
        //Emotions
        public void nPCEmotionOut(NPC npc)
        {
            string output = "";
            if (npc.Emotions != null)
            {
                if (npc.Emotions.Count == 0)
                {
                    qOutput(npc.ID + " is not feeling anything.");
                }
                else
                {
                    for (int i = 0; i < npc.Emotions.Count; i++)
                    {
                        for (int j = 0; j < npc.Emotions[i].Length; j++)
                        {
                            if (j == 0)
                            {
                                output = output + "Emotion: " + npc.Emotions[i][j] + "-" + nPCEmotion[npc.Emotions[i][j]];
                            }
                            else
                            {
                                output = output + " Weight: " + npc.Emotions[i][j] + " ";
                                qOutput(output);
                                output = "";
                            }
                        }
                    }
                }
            }
            else
            {
                qOutput(npc.ID + " is not feeling anything.");
            }
        }
        public bool nPcEmotChk(NPC n, int emotion)
        {
            if (n.Emotions != null)
            {
                for (int i = 0; i < n.Emotions.Count; i++)
                {
                    if (n.Emotions[i][0] == emotion)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void nPcEmotAdd(NPC n, int emotion, int weight)
        {
            if (n.Emotions != null)
            {
                if (nPcEmotChk(n, emotion) != true)
                {
                    if (weight > 0)
                    {
                        n.Emotions.Add(new int[] { emotion, weight });
                    }
                }
                else
                {
                    for (int i = 0; i < n.Emotions.Count; i++)
                    {
                        if (n.Emotions[i][0] == emotion)
                        {
                            if ((n.Emotions[i][0] + weight) < 0)
                            {
                                n.Emotions.RemoveAt(i);
                            }
                            else
                            {
                                n.Emotions[i][1] = n.Emotions[i][1] + weight;
                            }
                        }
                    }
                }
            }
            else
            {
                if (weight > 0)
                {
                    n.Emotions = new List<int[]> { new int[] { emotion, weight } };
                }
            }
        }
        //EmotBuffs
        public void nPCEmotBufOut(NPC npc)
        {
            string output = "";
            if (npc.EmotBuffs != null)
            {
                for (int i = 0; i < npc.EmotBuffs.Count; i++)
                {
                    for (int j = 0; j < npc.EmotBuffs[i].Length; j++)
                    {
                        //BuffID, Emotion, Weight, Duration
                        if (j == 0)
                        {
                            output = output + "Emotbuff: " + npc.EmotBuffs[i][j] + "-" + nPCEmotBuffsDef[npc.EmotBuffs[i][j]] + "\n";
                        }
                        else if (j == 1)
                        {
                            output = output + "Emotion: " + npc.EmotBuffs[i][j] + "-" + nPCEmotion[npc.EmotBuffs[i][j]] + "\n";
                        }
                        else if (j == 2)
                        {
                            output = output + "Weight: " + npc.EmotBuffs[i][j] + "\n";
                        }
                        else
                        {
                            output = output + "Duration: " + npc.EmotBuffs[i][j];
                            qOutput(output);
                            output = "";
                        }
                    }
                }
            }
            else
            {
                qOutput(npc.ID + " is not feeling anything.");
            }
        }
        public bool nPcEmotBufChk(NPC n, int EmotBuff)
        {
            if (n.EmotBuffs != null)
            {
                for (int i = 0; i < n.EmotBuffs.Count; i++)
                {
                    if (n.EmotBuffs[i][0] == EmotBuff)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void nPcEmotBufAdd(NPC n, int EmotBuff, int emot, int weight, int duration)
        {
            nPcEmotAdd(n, emot, weight);
            if (n.EmotBuffs != null)
            {
                n.EmotBuffs.Add(new int[] { EmotBuff, emot, weight, duration });
            }
            else
            {
                n.EmotBuffs = new List<int[]> { new int[] { EmotBuff, emot, weight, duration } };
            }
        }
        public void ProtoNPcEmotBufDurRem(NPC n, int duration)
        {
            if (n.EmotBuffs != null)
            {
                for (int i = 0; i < n.EmotBuffs.Count; i++)
                {
                    if (n.EmotBuffs[i][3] == duration)
                    {
                        nPcEmotAdd(n, n.EmotBuffs[i][1], - n.EmotBuffs[i][2]);
                        n.EmotBuffs.RemoveAt(i);
                    }
                }
            }
        }
        //Adding A NPC To another's memory
        public void nPcEncounter1(NPC npc1, NPC npc2)
        {
            npc1.npcPhoneBk.Add(npc2.ID);
            NPRelation rel = new NPRelation();
            rel.ID = npc2.ID;
            npc1.NPcRelations.Add(rel);
        }
        //Updeates Both NPC's Memories
        public void nPcEncounter2(NPC npc1, NPC npc2)
        {
            nPcEncounter1(npc1, npc2);
            nPcEncounter1(npc2, npc1);
        }
        // how well a encounter goes
        public void nPcEncounter3(NPC npc1, NPC npc2)
        {
            string[] mBTiChemistry = new string[] {
                "enjoyable", "pleasant", "amicable", "warm", "trying", "irritating", "frustrating", "very frustrating"
            };
            string mBTIChemFin = "";
            int[] mBTiRange = new int[] {
                2, 4, 6, 8, 10, 12, 14, 16
            };
            if (nPCHasMet(npc1, npc2) == true)
            {
                richTextBox1.AppendText(npc1.Name + " met " + npc2.Name + " again.\n");
            }
            else
            {
                richTextBox1.AppendText(npc1.Name + " encountered " + npc2.Name + ".\n");
                for (int type = 0; type < mBTiRange.Length; type++)
                {
                    if (nPCInteractChem(npc1, npc2) <= mBTiRange[type])
                    {
                        mBTIChemFin = mBTiChemistry[type];
                        break;
                    }
                }
                richTextBox1.AppendText(AarryToStr(npc1.mbtiPerA, 0) + " and " + AarryToStr(npc2.mbtiPerA, 0) + " had a " + mBTIChemFin + " encounter.\n");
                nPcEncounter2(npc1, npc2);
            }
        }
        //NPC Actions
        public string[][] nPcActions = new string[][]
        {
            new string[] {"0", "Interaction", " interacted with "},
        };
        public string npcInteraction(int i)
        {
            return nPcActions[i][2];
        }
        
        //Place Functions
        public class Place
        {
            public int[] ID = { -1, -1, -1, -1, -1, -1, -1, -1 };
            public List<int> NPCs = new List<int>();
        }
        List<List<Place>> Places = new List<List<Place>>();
        // 0 - Dimension, 1 - Galaxy, 2 - Solar System, 3 - Body, 4 - Continent, 5 - Province, 6 - City, 7 - Building
        int[] placeIndex = { -1, -1, -1, -1, -1, -1, -1, -1 };
        string[] placeType = { "Dimension", "Galaxy", "Solar System", "Body", "Continent", "Province", "City", "Building" };
        public void placeStore(Place pLA, int iDX)
        {
            if(Places.Count == iDX + 1)
            {
                Places[iDX].Add(pLA);
            }
            else
            {
                Places.Add(new List<Place> {pLA});
            }
        }
        public void generateDimension()
        {
            Place pla = new Place();
            placeIndex[0] = placeIndex[0] + 1;
            pla.ID = new int[] { placeIndex[0]};
            placeStore(pla, 0);
        }
        public void generatePlace(int[] subset)
        {
            Place pla = new Place();
            int Index = subset.Length;
            placeIndex[Index] = placeIndex[Index] + 1;
            int[] subset2 = new int[subset.Length + 1];
            for (int i = 0; i < subset.Length; i++)
            {
                subset2[i] = subset[i];
            }
            subset2[subset.Length - 1] = placeIndex[Index];
            pla.ID = subset2;
            placeStore(pla, Index);
        }
        public void doesPlaceExist(int[] subset)
        {
            if (subset.Length > placeType.Length)
            {
                richTextBox1.AppendText("No specific definition found " + (subset.Length - placeType.Length).ToString() + " value beyond " + placeType[placeType.Length -1] +  ".\n");
            }
            else
            {
                bool placeexists = false;
                for (int i = 0; i < subset.Length; i++)
                {
                    if (placeIndex[i] < subset[i])
                    {
                        richTextBox1.AppendText("This " + placeType[subset.Length - 1] + " doesn't exist.\n");
                        placeexists = false;
                        break;
                    }
                    placeexists = true;
                }
                if (placeexists == true)
                {
                    richTextBox1.AppendText("This " + placeType[subset.Length - 1] + " exists.\n");
                }
            }
        }
        public bool doesPlaceMatch (int[] chk, int[] vs)
        {
            for (int j = 0; j < chk.Length; j++)
            {
                if (chk[j] != vs[j])
                {
                    return false;
                }
            }
            return true;
        }
        public void updatePlaceNPCList (int location)
        {
            for (int j = 0; j < Places[location].Count(); j++)
            {
                Places[location][j].NPCs.Clear();
            }

                for (int i = 0; i < NPCs.Count; i++)
            {
                for (int j = 0; j < Places[location].Count(); j++)
                {
                    if (doesPlaceMatch(Places[location][j].ID, NPCs[i].locationID) == true)
                    {
                        Places[location][j].NPCs.Add(NPCs[i].ID);
                    }
                }
            }
        }





        //Proto NPC Gen
        public void generateNPCs1(int amt)
        {
            for (int i = 0; i < amt; i++)
            {
                npcIndex = npcIndex + 1;
                NPC npc = new NPC();
                npc.ID = npcIndex;
                npc.Name = "NPC" + i;
                npc.locationID = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                npc.npcPhoneBk = new List<int> { };
                npc.NPcRelations = new List<NPRelation> { };

                for (int trait = 0; trait < npc.oceanPer.Length; trait++)
                {
                    npc.oceanPer[trait] = rnd.Next(101);
                }

                /*
                new string[] { "Introverted", "Extraverted" },
                new string[] { "Sensing", "Intuition" },
                new string[] { "Thinking", "Feeling" },
                new string[] { "Perceiving", "Judging" },
                new string[] { "Flow", "Stress" }
                */

                string[][] MBTIDEF = new string[][] {
                    new string[] { "I", "E" },
                    new string[] { "S", "N" },
                    new string[] { "T", "F" },
                    new string[] { "P", "J" },
                    new string[] { "F", "S" }
                };

                OceantoMBTI(2, 0, npc);
                OceantoMBTI(0, 1, npc);
                OceantoMBTI(3, 2, npc);
                OceantoMBTI(1, 3, npc);
                OceantoMBTI(4, 4, npc);

                for (int trait = 0; trait < npc.mbtiPer.Length; trait++)
                {
                    npc.mbtiPerA[trait] = MBTIDEF[trait][npc.mbtiPer[trait]];
                }


                string[] mBTiToNum = new string[] {
                    "ISTJ", "ISTP", "ISFJ", "ISFP", "INFJ", "INFP", "INTJ", "INTP", "ESTP", "ESTJ", "ESFP", "ESFJ", "ENFP", "ENFJ", "ENTP", "ENTJ"
                };

                string mbtiWhole = npc.mbtiPerA[0] + npc.mbtiPerA[1] + npc.mbtiPerA[2] + npc.mbtiPerA[3];

                for (int type = 0; type < mBTiToNum.Length; type++)
                {
                    if (mbtiWhole == mBTiToNum[type])
                    {
                        npc.mbtiPerNum = type;
                    }
                }


                debugOut(npc.Name, 0);
                NPCs.Add(npc);
            }
        }
        //Proto World Generation
        public void generateLocations1()
        {
            generateDimension();
            //Galaxy
            generatePlace(new int[] { 0 });
            //Solar System
            generatePlace(new int[] { 0, 0 });
            //Body
            generatePlace(new int[] { 0, 0, 0 });
            // Continent
            generatePlace(new int[] { 0, 0, 0, 0 });
            // Province
            generatePlace(new int[] { 0, 0, 0, 0, 0 });
            // City
            generatePlace(new int[] { 0, 0, 0, 0, 0, 0 });
            /*
            doesPlaceExist(new int[] { 0, 0, 0, 0, 0, 0, 0 });
            doesPlaceExist(new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            doesPlaceExist(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            */
        }

        public void protoObs()
        {
            observedNPCS.Clear();
            foreach (NPC npc in NPCs)
            {
                if (npc.ID != observedNPC)
                {
                    observedNPCS.Add(npc.ID);
                }
            }
        }
        public void button1Stuff(int i)
        {
            if (i == 0)
            {

            }
        }
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(comboBoxChoi);
            generateNPCs1(10);
            generateLocations1();
            updatePlaceNPCList(6);

            nPcEncounter2(NPCs[0], NPCs[1]);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            qOutput(NPCs[0].Name + npcInteraction(0) + NPCs[1].Name);
            nPCRelatOut(NPCs[0], 0);
            */

            npcSkillRead(0);


            /*
            for (int i = 0; i < NPCs[0].NPcRelations.Count; i++)
            {
                qIOutput(NPCs[0].NPcRelations[i].ID);
            }
            */
            /*
            string a = "0";
            for (int i = 0; i < nPcActions.Length; i++)
            {
                if (nPcActions[i][0] == a)
                {
                    qOutput(nPcActions[i][1]);
                }
            };
            */
        }
    }
}
