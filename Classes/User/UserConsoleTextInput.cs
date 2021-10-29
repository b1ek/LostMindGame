using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LostMind.Classes.User
{
    public static class UserConsoleTextInput
    {

        public const string programName = "LG";

        public static string getUserAnswer(string question)
        {
            while (true)
            {
                Console.Write(programName + ":" + question + "> ");
                var ans = Console.ReadLine();
                if (ans != "")
                    return ans;
            }
        }

        public static string getUserAnswer(string question, string senderPath)
        {
            while (true)
            {
                Console.Write(senderPath + ":" + question + ": ");
                var ans = Console.ReadLine();
                if (ans != "")
                    return ans;
            }
        }

        public static string getUserAnswer(string question, Regex answerRegex)
        {
            Regex re = answerRegex;
            while (true)
            {
                Console.Write(programName + ":" + question + "> ");
                var ans = Console.ReadLine();
                if (ans != "" && re.IsMatch(ans))
                    return ans;
            }
        }

        public static string getUserAnswer(string question, string[] userAnswerVariants)
        {
            while (true)
            {
                Console.Write(programName + ":" + question + "> ");
                var ans = Console.ReadLine();
                if (ans != "" && userAnswerVariants.Contains(ans))
                    return ans;
            }
        }

        public static string getUserAnswer(string question, string senderPath, string[] userAnswerVariants)
        {
            while (true)
            {
                Console.Write(senderPath + ":" + question + "> ");
                var ans = Console.ReadLine();
                if (ans != "" && userAnswerVariants.Contains(ans))
                    return ans;
            }
        }
    }
}
