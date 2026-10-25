using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
public class CLIText
{
    public static List<string?> stiledata { get; set; } = new List<string?>(); // example: stiledata.Add("stileName:beforetitle:aftertitle:lineformat:line1:line2:line3:line4:line5:line6..."); // : <-- section seprator ; <-- subsection     
    /* probtys
     * text fomat ex: text+time ues for prit funcion 
     * stileName: the name of the stile
     * beforetitle: the text before the title
     * aftertitle: the text after the title
     modifier: the modifier for the stile
     @modifier: same as text 
     @behening: this will be the text that will be printed at the bigaing example: @behening;Hello
     @end: this will be the text that will be printed at the end example: @end;World
     @fill : this will be the text that will be printed at the end of the line example: @fill;# out put : @b

    */
    public static List<string?> textdata { get; set; } = new List<string?>(); // List to hold text data new system 
                                                                              // example: textdata.Add("textdata:@modifier:textdata");
    /* modifer list:
    main:
     * @speed;0: if zero will insta print if not itwill print text by character with a delay default is 0
     
     * @modifier: textdata - this is a modifier that can be used to change the
    color of the text: will support all the old color that the old system had 
     * @ascii;String: this will change the color using the ascii colar code aka more colors than dotnet
     * @name;string: this will change the color using the windows suppoted format aka more colors than dotnet
     * @red            → Sets text to Red
     * @green          → Sets text to Green
     * @blue           → Sets text to Blue
     * @yellow         → Sets text to Yellow
     * @cyan           → Sets text to Cyan
     * @magenta        → Sets text to Magenta
     * @gray           → Sets text to Gray
     * @white          → Sets text to White
     * @black          → Sets text to Black
     * @darkred        → Sets text to DarkRed
     * @darkgreen      → Sets text to DarkGreen
     * @darkblue       → Sets text to DarkBlue
     * @darkyellow     → Sets text to DarkYellow
     * @darkcyan       → Sets text to DarkCyan
     * @darkmagenta    → Sets text to DarkMagenta
     * @darkgray       → Sets text to DarkGray
     * @Mcolor         → Uses CLISettings.Mcolor
     * @Dmcolor        → Uses CLISettings.Dmcolor
     * @PColor         → Uses CLIPlayer.Color 
     * @return         → Adds a new line after the text
     * @cancelreturn   → Cancels the new line after the text becase the backend will add a new line after the text by default
     * @Blockreturn    → Blocks all new lines
     * @textboxnext    → this will add a text box at the bottom of the screen with the a simple wait befor the next text is printed
     * @clear          → Clears the screen

     
    public static void TextRender(string title, string endtype = "default", string stile = "default")
    {
        // calls the seleted stile to render the text
        // calls the text back end
        //stilePicker(stile, "top", title);
        // parse the textdata and render it
        //stilePicker(stile, endtype, title);

    }
    /*
    example of textdata:
    textdata.Add("@speed;0:hi:@PColor:@name:default");
    CLIText.TextRender("root", "next", "default");
    */
    public static void UI(
       string text = "",
       string Header = "",// the header text supports the @modifier system note @retrun is blocked in the header and will not work
       char[]? box = null)
    {
        box ??= new[] { '╔', '═', '╗', '║', '╚', '╝' };

        UItop(Header, box);
        // parse the textdata and render it interset @clear and @return to handle them here instead of the backend
        UIBottom(box);

    }
    static void UIBottom(char[] box)
    {
        CLIText.print($"{box[4]}{box[1]}{box[5]}", 0, true);
        //get the length of the terminal and ues it to caultae the amount of box[1] to print
    }
    static void UItop(string Header, char[] box)
    {
        CLIText.print(box[0].ToString(), 0, false);
        CLIText.textBackend("@Blockreturn:" + Header);
        //get the length of the terminal and ues it to caultae the amount of box[1] to print
        CLIText.print($"{box[1]}{box[2]}", 0, true);
    }
    public static int textBackendMeta(string data, string mode)
    {
        // text Gets the text length 
        // retun Gets the ammount of returns in the text
        List<string> table;
        int count = 0;
        table = data.Split(':').ToList();
        foreach (string tdata in table)
        {
            if (tdata.StartsWith("@"))
            {
                if (tdata.Contains("@speed"))
                {
                    // do nothing
                }
                else if (tdata.Contains("@return"))
                {
                    if (mode == "retun")
                    {
                        count++;
                    }
                }
            }
            else
            {
                if (mode == "text")
                {
                    count += tdata.Length+count;
                }
            }
        }
        return count;
    }
    public static void textBackend(string data)
    {
        List<string> table;
        table = data.Split(':').ToList();
        int speed = 0;
        bool treturn = false;
        bool Blockreturn = false;
        //if @ char send to modifier handler else send to print handler
        foreach (string tdata in table)
        {
            if (tdata.StartsWith("@"))
            {
                if (tdata.Contains("@speed"))
                {
                    string[] speeddata = tdata.Split(';');
                    if (speeddata.Length > 1 && int.TryParse(speeddata[1], out int parsedSpeed))
                    {
                        speed = parsedSpeed;
                    }
                }
                else if (tdata.Contains("@return"))
                {
                    if (Blockreturn == false)
                    {
                        print(" ", 0, true);
                    }
                }
                else if (tdata.Contains("@clear"))
                {
                    Console.Clear();
                }
                else if (tdata.Contains("@textboxnext"))
                {
                    print(">", 0, false);
                    Console.ReadLine();
                }
                else if (tdata.Contains("@colen"))
                {
                    print(":", 0, false);
                }
                else if (tdata.Contains("@cancelreturn"))
                {
                    treturn = false;
                }
                else if (tdata.Contains("@Blockreturn"))
                {
                    Blockreturn = true;
                    treturn = false;
                }
                else if (tdata.Contains("@"))// the bug was here it had @modifier wen it shoud have been @
                {
                    string[] colordata = tdata.Split('@');
                    if (colordata.Length > 1)
                    {
                        CLIText.Color(true, colordata[1]);
                    }
                }
            }
            else
            {
                print(tdata, speed, false);
            }
        }
        print("", 0, treturn);
    }
    public static void print(string text, int speed = 0, bool treturn = false) // done for now 
    {
        if (string.IsNullOrEmpty(text))
            return;

        // Instant print
        if (speed <= 0)
        {
            if (treturn)
                Console.WriteLine(text);
            else
                Console.Write(text);
            return;
        }

        // Timed character-by-character print
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(speed);
        }

        if (treturn)
            Console.WriteLine();
    }

    public static void Color(bool uesDotnet, string Color = "Mcolor", bool isBackground = false)
    {
        if (!uesDotnet) return;

        ConsoleColor targetColor;

        switch (Color)
        {
            case "Mcolor": targetColor = ConsoleColor.White; break;
            case "Dmcolor": targetColor = ConsoleColor.Gray; break;
            case "White": targetColor = ConsoleColor.White; break;
            case "Black": targetColor = ConsoleColor.Black; break;
            case "Red": targetColor = ConsoleColor.Red; break;
            case "Magenta": targetColor = ConsoleColor.Magenta; break;
            case "Yellow": targetColor = ConsoleColor.Yellow; break;
            case "Green": targetColor = ConsoleColor.Green; break;
            case "Blue": targetColor = ConsoleColor.Blue; break;
            case "Cyan": targetColor = ConsoleColor.Cyan; break;
            case "Gray": targetColor = ConsoleColor.Gray; break;
            case "DarkRed": targetColor = ConsoleColor.DarkRed; break;
            case "DarkMagenta": targetColor = ConsoleColor.DarkMagenta; break;
            case "DarkYellow": targetColor = ConsoleColor.DarkYellow; break;
            case "DarkGreen": targetColor = ConsoleColor.DarkGreen; break;
            case "DarkBlue": targetColor = ConsoleColor.DarkBlue; break;
            case "DarkCyan": targetColor = ConsoleColor.DarkCyan; break;
            case "DarkGray": targetColor = ConsoleColor.DarkGray; break;

            // Default fallback (you can customize to use Mcolor, etc.)
            default: return;
        }
        if (isBackground)
        {
            Console.BackgroundColor = targetColor;
        }
        else
        {
            Console.ForegroundColor = targetColor;
        }
    }
}
