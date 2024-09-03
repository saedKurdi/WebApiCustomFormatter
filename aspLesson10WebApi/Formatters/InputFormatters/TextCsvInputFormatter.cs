using aspLesson10WebApi.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace aspLesson10WebApi.Formatters.InputFormatters;
public class TextCsvInputFormatter : TextInputFormatter
{
    // constructor for adding some options : 
    public TextCsvInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public async override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var httpContext = context.HttpContext;
        var serviceProvider = httpContext.RequestServices;


        using var reader = new StreamReader(httpContext.Request.Body, encoding);
        string? fullInfoLine = null;

        try
        {
            await ReadLineAsync("Id - Fullname - SeriaNo - Age - Score", reader, context);

            // reading lines : 
            fullInfoLine = await ReadLineAsync("",reader,context);

            // splitting the splitted data to get main part : 
            var fullInfoSplit = fullInfoLine.Trim().Split('-');

            // creating student :
            var student = new StudentAddDTO { FullName = fullInfoSplit[1], SeriaNO = fullInfoSplit[2], Age = int.Parse(fullInfoSplit[3]), Score = double.Parse(fullInfoSplit[4]) };

            // returning result to controller : 
            return await InputFormatterResult.SuccessAsync(student);
        }
        catch
        {
            return await InputFormatterResult.FailureAsync();
        }
    }

    private static async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context)
    {
        var line = await reader.ReadLineAsync();
        if (line is null || !line.StartsWith(expectedText))
        {
            var errorMessage = $"Looked for '{expectedText}' and got '{line}'";
            context.ModelState.TryAddModelError(context.ModelName, errorMessage);
            throw new Exception(errorMessage);
        }
        return line;
    }
}
