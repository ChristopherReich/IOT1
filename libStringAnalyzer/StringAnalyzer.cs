using System;
using System.Text.RegularExpressions;

namespace libStringAnalyzer
{
    public class StringAnalyzer
    {
        public static string Evaluate(string req)
        {
            string response;

            // Leerzeichen entfernen
            string theReqest = req.Replace(" ", "");

            // Vergleich Operator erkannt
            if (theReqest.Contains("=="))
            {
                Match match = Regex.Match(theReqest, @"((.*)(==)(.*))");

                if (match.Groups[2].Value == match.Groups[4].Value)
                    response = "Die Zeichenfolge stimmt überein!";
                else
                    response = "Die Zeichenfolge stimmt NICHT überein!";
            }
            // Vergleich Operator erkannt
            if (theReqest.Contains(";"))
            {
                Match match = Regex.Match(theReqest, @"((.*)(;)(.*))");

                if (match.Groups[2].Value == match.Groups[4].Value)
                    response = "Die Zeichenfolge stimmt überein!";
                else
                    response = "Die Zeichenfolge stimmt NICHT überein!";
            }

            // Mathematischer Operator erkannt
            else if (theReqest.Contains("+") || theReqest.Contains("-") || theReqest.Contains("*") || theReqest.Contains("/"))
            {
                Match match = Regex.Match(theReqest, @"(\d*\,?\.?\d*)([+-/*])(\d*\,?\.?\d*)");
                double number1;
                double number2;
                string mathOperator;
                double solution;

                // Zahlen extrahieren
                if (double.TryParse(match.Groups[1].Value, out number1) && double.TryParse(match.Groups[3].Value, out number2))
                {
                    mathOperator = match.Groups[2].Value;

                    switch (mathOperator)
                    {
                        case "+":
                            solution = number1 + number2;
                            response = solution.ToString();
                            break;

                        case "-":
                            solution = number1 - number2;
                            response = solution.ToString();
                            break;

                        case "/":
                            if (number2 != 0)
                            {
                                solution = number1 / number2;
                                response = solution.ToString();
                            }
                            else
                            {
                                response = "Division durch '0' erkannt!";
                            }
                            break;

                        case "*":
                            solution = number1 * number2;
                            response = solution.ToString();
                            break;

                        default:
                            response = "Fataler Fehler!";
                            break;
                    }
                }
                else
                    response = "Zahlen des mathematischen Ausdruckes konnten nicht extrahiert werden. Prüfen Sie Ihre Eingabe!";
            }
            else
                response = "Eingabe konnte nicht ausgewertet werden. Prüfen Sie ihre Eingabe!";

            return response;

        }
    }
}
