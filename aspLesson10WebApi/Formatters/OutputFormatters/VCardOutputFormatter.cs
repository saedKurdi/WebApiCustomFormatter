using aspLesson10WebApi.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace aspLesson10WebApi.Formatters.OutputFormatters;
public class VCardOutputFormatter : TextOutputFormatter
{
    // constructor for adding some options : 
    public VCardOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    // when response comes from server - to controller after this this method works and transfers that data to our new format and returns : 
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var sb = new StringBuilder();
        if (context.Object is IEnumerable<StudentDTO> list)
            foreach (var item in list)
                FormatVCard(sb, item);
        else if (context.Object is StudentDTO student) FormatVCard(sb, student);
        await response.WriteAsync(sb.ToString());
    }

    // method for formatting the card as string (new-type:vcard) and return : 
    private void FormatVCard(StringBuilder sb, StudentDTO student)
    {
        sb.AppendLine("BEGIN:VCARD");
        sb.AppendLine($"FN:{student.FullName}");
        sb.AppendLine($"SNO:{student.SeriaNO}");
        sb.AppendLine($"AGE:{student.Age}");
        sb.AppendLine($"SCORE:{student.Score}");
        sb.AppendLine("END:VCARD");
    }
}
