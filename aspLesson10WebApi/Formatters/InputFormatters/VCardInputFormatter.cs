using aspLesson10WebApi.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace aspLesson10WebApi.Formatters.InputFormatters;
public class VCardInputFormatter : TextInputFormatter
{
    // constructor for adding some options : 
    public VCardInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public async override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var httpContext = context.HttpContext;
        var serviceProvider = httpContext.RequestServices;


        using var reader = new StreamReader(httpContext.Request.Body, encoding);
        string? fullnameLine = null;
        string? seriaNoLine = null;
        string? ageLine = null;
        string? scoreLine = null;

        try
        {
            await ReadLineAsync("BEGIN:VCARD", reader, context);

            // reading lines : 
            fullnameLine = await ReadLineAsync("FN:", reader, context);
            seriaNoLine = await ReadLineAsync("SNO:", reader, context);
            ageLine = await ReadLineAsync("AGE:", reader, context);
            scoreLine = await ReadLineAsync("SCORE:", reader, context);

            // splitting the splitted data to get main part : 
            var fullNameSplit = fullnameLine.Split(":".ToCharArray());
            var seriaNoSplit = seriaNoLine.Split(":".ToCharArray());
            var ageSplit = ageLine.Split(":".ToCharArray());
            var scoreSplit = scoreLine.Split(":".ToCharArray());

            // creating student :
            var student = new StudentAddDTO {FullName = fullNameSplit[1],SeriaNO = seriaNoSplit[1],Age = int.Parse(ageSplit[1]),Score = double.Parse(scoreSplit[1]) };

            await ReadLineAsync("END:VCARD", reader, context);

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
