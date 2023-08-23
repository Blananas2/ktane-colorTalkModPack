﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class KMazeyTalk : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Buttons;
    public TextMesh Display;
    public KMSelectable Submit;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;
    private List<int> NumberFucker = new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83};
    private List<int> WeedKhungus = new List<int>{16,18,20,22,24,26,28,46,48,50,52,54,56,58,76,78,80,82,84,86,88,106,108,110,112,114,116,118,136,138,140,142,144,146,148,166,168,170,172,174,176,178,196,198,200,202,204,206,208,226,228,230,232,234,236,238,256,258,260,262,264,266,268,286,288,290,292,294,296,298,316,318,320,322,324,326,328,346,348,350,352,354,356,358};
    int Youisdumb = 0;
    int Initial = 0;
    bool REPLACEDONE = false;
    char VibeCheck = '-';
    string Maze =
"---------------"+ //0-14
"|XOX|XOXOXOXOX|"+//15-29
"|O-O-O-O-----O|"+//30-44
"|X|X|X|XOXOX|X|"+//45-59
"|O-O-O---O-O--|"+//60-74
"|X|X|XOXOX|XOX|"+//75-89
"|O-----O-----O|"+
"|XOXOX|XOXOXOX|"+
"|----O-O-O---O|"+
"|XOXOX|X|XOX|X|"+
"|O-----O---O--|"+
"|XOXOXOXOX|XOX|"+
"|O-----------O|"+
"|X|XOX|XOXOX|X|"+
"|O-O-O-O-O---O|"+
"|XOX|X|X|XOXOX|"+
"|----O-O-O----|"+
"|XOXOXOXOX|XOX|"+
"|O-----O-O---O|"+
"|X|XOXOX|XOXOX|"+
"|O-O-O-------O|"+
"|XOX|X|XOX|XOX|"+
"|O---O-O-O-O--|"+
"|XOX|XOX|XOXOX|"+
"---------------";
    char Climax = '-';
    int weed = 0;
    int Wavecheck = 0;
    bool Vibin = false;
    int BigIfSad = -1;
    string OneDeeChess = "";

    private Coroutine buttonHold;
	private bool holding = false;

    void Awake () {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable Button in Buttons) {
            Button.OnInteract += delegate () { ButtonPress(Button); return false; };
        }
        Submit.OnInteract += delegate () { SubPress(); return false; };
        Submit.OnInteractEnded += delegate { SubRelease(); };
        weed = 16;
    }

    void Start () {
      while(VibeCheck != 'X'){
      Youisdumb = UnityEngine.Random.Range(0,Maze.Length);
      BigIfSad = Youisdumb;
      VibeCheck = Maze[Youisdumb];
      }
      while(Climax != 'X'){
        Wavecheck = UnityEngine.Random.Range(0,Maze.Length);
        while (Wavecheck == Youisdumb) {
            Wavecheck = UnityEngine.Random.Range(0,Maze.Length);
        }
        Climax = Maze[Wavecheck];
      }
      //Debug.Log(Youisdumb);
      for (int i = 0; i < 84; i++) {
        if (Wavecheck == WeedKhungus[i]) {
          Display.text = KMazeyTalk1.phrases[i];
          Debug.LogFormat("[KayMazey Talk #{0}] Your ending phrase is \"{1}\".",moduleId,KMazeyTalk1.phrases[i].Replace("\n", ""));
          OneDeeChess = KMazeyTalk1.phrases[i];
        }
      }
	}

  void ButtonPress(KMSelectable Button){
    Button.AddInteractionPunch();
    GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Button.transform);
    if (REPLACEDONE == true) {
      if (Button == Buttons[0]) { //right
        Youisdumb += 1;
        if (Maze[Youisdumb] == 'O') {
          Youisdumb += 1;
        }
        else {
          GetComponent<KMBombModule>().HandleStrike();
          Youisdumb -= 1;
        }
      }
      else if (Button == Buttons[1]) { //Left
        Youisdumb -= 1;
        if (Maze[Youisdumb] == 'O') {
          Youisdumb -= 1;
        }
        else {
          Youisdumb += 1;
          GetComponent<KMBombModule>().HandleStrike();
        }
      }
      else if (Button == Buttons[2]) { //Up
        Youisdumb -= 15;
        if (Youisdumb < 0 || Maze[Youisdumb] == 'O') {
          Youisdumb -= 15;
        }
        else {
          Youisdumb += 15;
          GetComponent<KMBombModule>().HandleStrike();
        }
      }
      else if (Button == Buttons[3]) { //Down
        Youisdumb += 15;
        if (Youisdumb > 374 || Maze[Youisdumb] == 'O') {
          Youisdumb += 15;
        }
        else {
          Youisdumb -= 15;
          GetComponent<KMBombModule>().HandleStrike();
        }
      }
      else {
        Debug.Log("Fuck");
      }
      //Debug.Log(Youisdumb);

      for (int i = 0; i < 84; i++) {
        if (Youisdumb == WeedKhungus[i]) {
          Display.text = KMazeyTalk1.phrases[i];
        }
      }
    }
  }

  void SubPress () {
        Submit.AddInteractionPunch();
            if (buttonHold != null)
    		{
    			holding = false;
    			StopCoroutine(buttonHold);
    			buttonHold = null;
    		}

    	buttonHold = StartCoroutine(HoldChecker());
    }

    void SubRelease () {
        StopCoroutine(buttonHold);
        if (holding) {
            for (int i = 0; i < 84; i++) {
              if (BigIfSad == WeedKhungus[i]) {
                Youisdumb = BigIfSad;
                Display.text = KMazeyTalk1.phrases[i];
                Debug.LogFormat("[KayMazey Talk #{0}] Resetting... back to \"{1}\"",moduleId,KMazeyTalk1.phrases[i].Replace("\n", ""));
              }
            }
        } else {
            ActuallySubmit();
        }
        holding = false;
    }

    IEnumerator HoldChecker()
    {
    	yield return new WaitForSeconds(0.4f);
    	holding = true;
    }

  void ActuallySubmit(){
    //Submit.AddInteractionPunch();
    GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Submit.transform);
    if (REPLACEDONE == false) {
      REPLACEDONE = true;
    }
    if (Vibin == false) {
      Vibin = true;
      for (int i = 0; i < 84; i++) {
        if (Youisdumb == WeedKhungus[i]) {
          Display.text = KMazeyTalk1.phrases[i];
          Debug.LogFormat("[KayMazey Talk #{0}] Your current phrase is \"{1}\".",moduleId,KMazeyTalk1.phrases[i].Replace("\n", ""));
        }
      }
    }
    else if (Youisdumb == Wavecheck) {
      GetComponent<KMBombModule>().HandlePass();
      Display.text = "KayMazey Talk";
      Audio.PlaySoundAtTransform("TwinklyBullshit", transform);
    }
    else {
      GetComponent<KMBombModule>().HandleStrike();
    }
  }

  //Toilet Paper
  #pragma warning disable 414
  private readonly string TwitchHelpMessage = @"Use !{0} move UDRL to move around the maze. Use !{0} circle to press the circle.";
  #pragma warning restore 414
  IEnumerator ProcessTwitchCommand(string ohgodno){
    ohgodno = ohgodno.ToUpperInvariant().Trim();
    Match meth = Regex.Match(ohgodno, @"^(?:MOVE ([UDRL]+)|CIRCLE)$");
    if (meth.Groups[1].Success){
      char[] crabcrab = meth.Groups[1].Value.ToCharArray();
      Dictionary<char, int> help = new Dictionary<char, int>() {
        { 'R', 0 },
        { 'L', 1 },
        { 'U', 2 },
        { 'D', 3 }
      };
      yield return null;
      for (int i = 0; i < crabcrab.Length; i++){
        Buttons[help[crabcrab[i]]].OnInteract();
        yield return new WaitForSeconds(.1f);
      }
    }
    else if (meth.Success)
    {
      yield return null;
      Submit.OnInteract();
      yield return new WaitForSeconds(.1f);
      Submit.OnInteractEnded();
    }
    else
      yield return "sendtochaterror Valid commands are move and circle. Use !{1} help to see the full commands.";
    yield break;
  }
  //Iterative Breadth First Search
  struct Graph
    {
        public int Position { get { return _position; } }
        public int PreviousLocation { get { return _previousLocation; } }
        public string Direction { get { return _direction; } }
        public Graph(int position, int previousLocation, string direction)
        {
            _position = position;
            _previousLocation = previousLocation;
            _direction = direction;
        }
        private int _position;
        private int _previousLocation;
        private string _direction;
    }
    string BFS()
    {
        //Youisdumb Is the initial location
        Queue<int> ihatewaiting = new Queue<int>();
        HashSet<Graph> lickingBalls = new HashSet<Graph>();
        ihatewaiting.Enqueue(Youisdumb);
        lickingBalls.Add(new Graph(Youisdumb, -1, ""));
        int[] oofSet = new int[4] { 1, -1, -15, 15 };
        string[] oofDir = new string[4] { "R", "L", "U", "D" };
        //+1 right - 1 left - 15 up + 15 down
        while (ihatewaiting.Count != 0)
        {
            var pee = ihatewaiting.Dequeue();
            if (pee == Wavecheck)
            {
                string asser = "";
                Graph pee2pee = new Graph();
                do
                {
                    pee2pee = lickingBalls.Where(graph => graph.Position == pee).ElementAt(0);
                    asser = pee2pee.Direction + asser;
                    pee = pee2pee.PreviousLocation;
                }
                while (pee != -1);
                return asser;
            }
            for (int i = 0; i < 4; i++)
            {
                // -1 -15 +15 +1 -> // -2 -30 +30 +2
                if (!(Maze[pee + oofSet[i]] == 'O') || lickingBalls.Any(graph => graph.Position == (pee + 2 * oofSet[i])))
                    continue;
                lickingBalls.Add(new Graph(pee + 2 * oofSet[i], pee, oofDir[i]));
                ihatewaiting.Enqueue(pee + 2 * oofSet[i]);
            }
        }
        return "69";
    }
    IEnumerator TwitchHandleForcedSolve()
    {
        if (!Vibin)
            yield return ProcessTwitchCommand("circle");
        yield return new WaitForSeconds(.1f);
        string fourtwenty = BFS();
        if (fourtwenty == "69") yield break;
        if (fourtwenty != "")
            yield return ProcessTwitchCommand("move " + fourtwenty);
        yield return ProcessTwitchCommand("circle");
    }
}
