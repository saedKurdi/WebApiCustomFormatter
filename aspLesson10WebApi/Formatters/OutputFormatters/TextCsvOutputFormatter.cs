using aspLesson10WebApi.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace aspLesson10WebApi.Formatters.OutputFormatters;
public class TextCsvOutputFormatter : TextOutputFormatter
{
    // constructor for adding some options : 
    public TextCsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    // when response comes from server - to controller after this this method works and transfers that data to our new format and returns : 
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var sb = new StringBuilder();
        sb.AppendLine("Id - FullName - SeriaNo - Age - Score");
        if (context.Object is IEnumerable<StudentDTO> list)
        {
            foreach (var item in list)
            {
                FormatCsvCard(sb, item);
            }
        }
        else if (context.Object is StudentDTO student) FormatCsvCard(sb, student);
        await response.WriteAsync(sb.ToString());
    }

    // method for formatting the card as string (new-type:vcard) and return : 
    private void FormatCsvCard(StringBuilder sb, StudentDTO student)
    {
        sb.AppendLine($"{student.Id} - {student.FullName} - {student.SeriaNO} - {student.Age} - {student.Score}");
    }
}
