using System.Text;
using System.Text.RegularExpressions;

namespace Bau.Libraries.LibHelper.Consoles;

/// <summary>
///     Clase de ayuda que facilita escribir en la consola utilizando colores
/// </summary>
public class ColorConsole
{
    /// <summary>
    ///     Escribe una línea co un color
    /// </summary>
    public void WriteLine(string text, ConsoleColor? color = null)
    {
        InnerWrite(text, true, color);
    }

    /// <summary>
    ///     Escribe una línea con un color específico utilizando una cadena
    /// </summary>
    public void WriteLine(string text, string color)
    {
        InnerWrite(text, true, Parse(color));
    }

    /// <summary>
    ///     Escribe con un color (sin saltar de línea)
    /// </summary>
    public void Write(string text, ConsoleColor? color = null)
    {
        InnerWrite(text, false, color);
    }

    /// <summary>
    ///     Escribe con un color (sin saltar de línea)
    /// </summary>
    public void Write(string text, string color)
    {
        InnerWrite(text, false, Parse(color));
    }

    /// <summary>
    ///     Interpreta un color escrito como cadena
    /// </summary>
    private ConsoleColor? Parse(string color)
    {
        if (string.IsNullOrEmpty(color) || !Enum.TryParse(color, true, out ConsoleColor consoleColor))
            return null;
        else
            return consoleColor;
    }

    /// <summary>
    ///     Escribe un texto (pasando o no a una nueva línea)
    /// </summary>
    private void InnerWrite(string text, bool withNewLine, ConsoleColor? color = null)
    {
        ConsoleColor oldColor = Console.ForegroundColor;
        string indentation = new string('\t', Indent);

            // Cambia el color
            if (color != null)
                Console.ForegroundColor = color.Value;
            // Escribe la línea
            if (withNewLine)
            {
                Console.WriteLine(indentation + text);
                StartLine = true;
            }
             else if (StartLine)
             {
                Console.Write(indentation + text);
                StartLine = false;
            }
            else
                Console.Write(text);
            // Vuelve al color anterior
            Console.ForegroundColor = oldColor;
    }

    /// <summary>
    ///     Escribe una línea de cabecera entre dos líneas de guiones
    /// </summary>
    /// <remarks>
    ///     El resultado sería algo así:
    /// -----------
    /// Header Text
    /// -----------
    /// </remarks>
    public void WriteWrappedHeader(string headerText, char wrapperChar = '-',
                                   ConsoleColor headerColor = ConsoleColor.Yellow,
                                   ConsoleColor dashColor = ConsoleColor.DarkGray)
    {
        string line = new StringBuilder().Insert(0, wrapperChar.ToString(), headerText.Length).ToString();

            WriteLine(line, dashColor);
            WriteLine(headerText, headerColor);
            WriteLine(line, dashColor);
    }

    /// <summary>
    ///     Escribe una línea correcta (verde)
    /// </summary>
    public void WriteSuccess(string text)
    {
        WriteLine(text, ConsoleColor.Green);
    }
    
    /// <summary>
    ///     Escribe una línea de error (rojo)
    /// </summary>
    public void WriteError(string text)
    {
        WriteLine(text, ConsoleColor.Red);
    }

    /// <summary>
    ///     Escribe una línea de advertencia (amarillo)
    /// </summary>
    public void WriteWarning(string text)
    {
        WriteLine(text, ConsoleColor.DarkYellow);
    }

    /// <summary>
    ///     Escribe una línea informativa (cyan oscuro)
    /// </summary>
    public void WriteInfo(string text)
    {
        WriteLine(text, ConsoleColor.DarkCyan);
    }

    /// <summary>
    ///     Escribe una cadena con colores, por ejemplo:
    ///     Esto es [red]texto en rojo[/red] y esto es [blue]texto azul[/blue]
    /// </summary>
    public void WriteParsedLine(string text, ConsoleColor? color = null)
    {
        // Cambia el color
        if (color == null)
            color = Console.ForegroundColor;
        // Si hay algo que escribir...
        if (string.IsNullOrEmpty(text))
            WriteLine(string.Empty);
        else
        {
            int start = text.IndexOf("[");
            int end = text.IndexOf("]");

                if (start == -1 || end <= start)
                    WriteLine(text, color);
                else
                {
                    while (true)
                    {
                        Match match = Regex.Match(text, "\\[.*?\\].*?\\[/.*?\\]");

                            if (match.Length < 1)
                            {
                                Write(text, color);
                                break;
                            }

                            // Escribe la expresión
                            Write(text.Substring(0, match.Index), color);
                            // Escribe el texto con el color especificado entre los corchetes
                            Write(ExtractString(text, "]", "["), ExtractString(text, "[", "]"));
                            // Escribe el resto de la cadena
                            text = text.Substring(match.Index + match.Value.Length);
                    }
                }
                // Salta de línea
                WriteLine(string.Empty);
        }
    }
    
    /// <summary>
    ///     Extrae una cadena entre delimitadores
    /// </summary>
    private string ExtractString(string source, string startDelimitier, string endDelimitier, bool returnDelimiters = false)
    {
        // Si hay algo de donde extraer...
        if (!string.IsNullOrEmpty(source))
        {
            int start = source.IndexOf(startDelimitier, 0, source.Length, StringComparison.OrdinalIgnoreCase);
            int end = source.IndexOf(endDelimitier, start + startDelimitier.Length, StringComparison.OrdinalIgnoreCase);

                // Si se ha encontrado el valor del delimitador
                if (start > -1 && end > 1)
                {
                    if (!returnDelimiters)
                        return source.Substring(start + startDelimitier.Length, end - start - startDelimitier.Length);
                    else
                        return source.Substring(start, end - start + endDelimitier.Length);
                }
        }
        // Si ha llegado hasta aquí es porque no hay nada que extraer
        return string.Empty;
    }

    /// <summary>
    ///     Incrementa la indentación
    /// </summary>
    public void AddIndent()
    {
        Indent++;
    }

    /// <summary>
    ///     Decrementa la indentación
    /// </summary>
    public void RemoveIndent()
    {
        Indent--;
    }

    /// <summary>
    ///     Indentación
    /// </summary>
    public int Indent { get; private set; } = 0;

    /// <summary>
    ///     Indica si estamos al inicio de línea
    /// </summary>
    public bool StartLine { get; private set; } = true;
}