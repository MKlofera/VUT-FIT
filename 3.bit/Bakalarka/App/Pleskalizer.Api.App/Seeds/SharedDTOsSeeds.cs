using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class SharedDTOsSeeds
{
    public static Faker<ImfEnvelopeDTO> EmailEnvelopeFaker { get; set; }
    public static Faker<ImfHeaderDTO> EmailHeaderFaker { get; set; }
    public static Faker<ImfMessageDTO> EmailMessageFaker { get; set; }
    public static Faker<ImfAttachmentDTO> EmailAttachmentFaker { get; set; }

    public static Faker<FlowStatisticsSnapshotDTO> FlowStatisticsSnapshotFaker { get; set; }

    public static readonly string[] L7Protocols =
        { "HTTP", "HTTPS", "DNS", "FTP", "SMTP", "POP3", "IMAP", "SSH", "Telnet", "SNMP" };

    public static readonly int[] HttpStatusCodes = new int[]
    {
        200,
        201,
        204,
        301,
        302,
        400,
        401,
        403,
        404,
        500
    };

    public static readonly string[] EmailPath = new[]
    {
        "Received: from smtp.gmail.com (smtp.gmail.com [172.217.194.108]) by mx.example.net (Postfix) with ESMTPS id 1234567 for jane.doe@example.com; Mon, 15 Feb 2023 10:11:13 -0800 (PST) Received: from [192.168.0.1] ([82.142.3.201]) by smtp.gmail.com with ESMTPSA id abcdefghijklmnopqrs for jane.doe@example.com (version=TLS1_3 cipher=TLS_AES_256_GCM_SHA384 bits=256/256);",
        "Received: from mail.example.com (mail.example.com [192.0.2.1]) by mx.example.net with ESMTP id abcdefgh.12345678 for jane.doe@example.com; Thu, 20 Jan 2023 09:28:00 -0500 (EST) Received: from [192.168.1.10] (unknown [192.168.1.10]) by mail.example.com (Postfix) with ESMTPSA id 12345678 for jane.doe@example.com; Thu, 20 Jan 2023 09:28:00 -0500 (EST)",
        "Received: from smtp.example.com (smtp.example.com [192.0.2.1]) by mx.example.net with ESMTP id abcdefgh.12345678 for jane.doe@example.com; Wed, 1 Dec 2022 16:22:00 -0500 (EST) Received: from [192.168.1.10] (unknown [192.168.1.10]) by smtp.example.com (Postfix) with ESMTPSA id 12345678 for jane.doe@example.com; Wed, 1 Dec 2022 16:22:00 -0500 (EST)"
    };

    public static readonly string[] FileEncodings = new string[]
        { "7bit", "8bit", "binary", "quoted-printable", "base64" };

    public static readonly string[] FileTypes = new string[]
    {
        ".txt",
        ".doc",
        ".docx",
        ".pdf",
        ".jpeg",
        ".png",
        ".gif",
        ".mp3",
        ".mp4",
        ".wav"
    };
    
    public static readonly string[] EmailSenders = new string[]
    {
        "Tabitha_Hayes",
        "Justyn_Larson",
        "Zola97",
        "Roma31",
        "Kiara26",
        "Marianne_Quigley",
        "Catalina",
        "Olen",
    };
    
    public static readonly string[] IpAddresses = new string[]
    {
        "192.168.0.1",
        "10.0.0.1",
        "172.16.0.1",
        "8.8.8.8",
        "192.168.1.1",
        "10.1.1.1",
        "172.17.0.1",
        "8.8.4.4",
        "192.168.0.10",
        "10.0.0.2",
        "172.16.1.1",
        "4.4.4.4",
        "192.168.1.10",
        "10.1.1.2",
        "172.17.1.1"
    };
    
    public static readonly int[] PortNumbers = new int[]
    {
        80,
        443,
        22,
        21,
        25,
        587,
        143,
        993,
        3306,
        5432,
        3389,
        5900,
        5901,
        5902,
        5903
    };

    public static readonly string[] HeaderMessageNames = new[] { "From", "To", "Cc", "Bcc", "Reply-to" };

    public static readonly Guid[] GuidArray = new Guid[]
    {
        new Guid("aafefabe-3b6e-4c69-bd66-7fc71825ab7a"),
        new Guid("0ab2d3bf-795b-4de7-8126-3e5f35eb9dc1"),
        new Guid("ae1551ae-29d8-4279-8fba-5e425a5b8788"),
        new Guid("74714946-8755-4976-b30f-757f1f7fe028"),
        new Guid("3a5de800-3ea4-42d6-9402-eaf37334de46"),
        new Guid("3e69f166-1e25-4074-a4b3-153adc14135c"),
        new Guid("4fa6dfae-90be-49ad-92d6-19eb1ebf7fab"),
        new Guid("d79b65aa-e060-4086-9c95-be323e7ea71e"),
        new Guid("6d27750c-798e-467d-be2e-3ebdea7e2eac"),
        new Guid("258fc450-3539-4b5f-964f-422aa1fed445"),
        new Guid("5e1e93de-44fd-4830-b264-1d36dfec68df"),
        new Guid("040de6ef-a08e-43e3-9f9a-4f4c3c5f5184"),
        new Guid("f73eb2a1-c3a5-4f23-a794-d65f22f77c23"),
        new Guid("ea1126b2-f596-416a-8795-8dbafb219658"),
        new Guid("98310ffc-ec1c-4e2f-a3fc-7978e11a63bd"),
        new Guid("de14ea78-cc3e-477e-a631-64523d79011d"),
        new Guid("9324c204-1063-44ee-a2b5-a89e68102a2e"),
        new Guid("d2a09c02-7385-4503-a95f-528ebd1ea94a"),
        new Guid("422f6174-03ea-4671-b3a9-a0344faa4658"),
        new Guid("8f2bf928-950c-4d8b-9cb9-0776ef6a12b1")
    };

    public static readonly string[] HttpHeaderNames = new string[]
    {
        "Accept",
        "Accept-Encoding",
        "Accept-Language",
        "Cache-Control",
        "Connection",
        "Content-Length",
        "Content-Type",
        "Cookie",
        "Host",
        "Origin",
        "Referer",
        "User-Agent"
    };
    
    public static readonly string[] Urls = new string[]
    {
        "https://www.google.com/",
        "https://www.youtube.com/",
        "https://www.facebook.com/",
        "https://www.wikipedia.org/",
        "https://www.amazon.com/",
        "https://www.twitter.com/",
        "https://www.instagram.com/",
        "https://www.linkedin.com/",
        "https://www.reddit.com/",
        "https://www.netflix.com/"
    };
    
    public static readonly string[] EmailAddresses = new string[] {
        "Kendall_Graham@gmail.com",
        "Alec_Nolan@hotmail.com",
        "Kenneth_Beer39@gmail.com",
        "Kendall_Becker23@yahoo.com",
        "Kaylah_Rodriguez@yahoo.com",
        "Deshawn.McKenzie@yahoo.com",
        "Carole67@gmail.com",
        "Dorian_Bayer@yahoo.com",
        "Immanuel_Hyatt@hotmail.com",
        "Jolie_Glover@hotmail.com",
        "Floyd51@hotmail.com",
        "Krista_Leffler@hotmail.com",
        "Cydney_Haley@yahoo.com",
        "Elyse.Barton31@yahoo.com",
        "Phoebe_Ryan@yahoo.com",
        "Aylin_Senger58@hotmail.com",
        "Delmer.Crona73@hotmail.com"
    };

    public static readonly string[][] HttpHeaderValues = new string[][]
    {
        new string[] { "application/json", "application/xml", "text/html", "text/plain" },
        new string[]
        {
            "gzip, deflate", "br", "compress", "identity", "deflate", "exi", "pack200-gzip", "bzip2", "deflate64",
            "gzip"
        },
        new string[] { "en-US,en;q=0.9", "fr-FR,fr;q=0.8", "es-ES;q=0.5", "es;q=0.3" },
        new string[]
        {
            "max-age=0", "max-age=3600", "no-cache", "public", "private", "must-revalidate", "no-store", "no-transform",
            "proxy-revalidate", "s-maxage=3600"
        },
        new string[] { "keep-alive", "close", "upgrade", "TE" },
        new string[] { "1024", "2048", "4096", "8192" },
        new string[]
        {
            "text/plain",
            "text/html",
            "text/css",
            "text/javascript",
            "text/xml",
            "text/markdown",
            "application/json",
            "application/xml",
            "application/pdf",
            "application/octet-stream",
            "application/zip",
            "application/vnd.ms-excel",
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/svg+xml",
            "image/webp",
            "image/bmp",
            "multipart/form-data",
            "multipart/byteranges",
            "multipart/related",
            "multipart/mixed",
            "multipart/alternative",
            "multipart/signed",
            "audio/mpeg",
            "audio/wav",
            "audio/ogg",
            "audio/flac",
            "audio/mp4",
            "audio/aac",
            "video/mp4",
            "video/mpeg",
            "video/avi",
            "video/webm",
            "video/quicktime",
            "video/x-ms-wmv"
        },
        new string[]
        {
            "session_id=123456789", "session_id=abcdefghi", "access_token=abc123def456", "refresh_token=def456ghi789"
        },
        new string[] { "example.com", "www.example.com", "api.example.com" },
        new string[] { "https://example.com", "http://example.com", "https://www.example.com" },
        new string[]
            { "https://example.com/page.html", "https://example.com/page2.html", "https://example.com/page3.html" },
        new string[]
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:94.0) Gecko/20100101 Firefox/94.0",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.1 Safari/605.1.15"
        }
    };

    static SharedDTOsSeeds()
    {
        Random randomNum = new Random();
        EmailEnvelopeFaker = new Faker<ImfEnvelopeDTO>()
            .RuleFor(o => o.DateTicks, f => randomNum.NextInt64(636929668, 1678308868))
            .RuleFor(o => o.Subject, f => f.Commerce.ProductName())
            .RuleFor(o => o.AddressesFrom, f => f.Make(randomNum.Next(1, 3), () => f.Internet.Email()))
            .RuleFor(o => o.Sender, f => EmailSenders[randomNum.Next(0, EmailSenders.Length - 1)])
            .RuleFor(o => o.ReplyTo, f => f.Make(randomNum.Next(0, 10), () => f.Internet.Email()))
            .RuleFor(o => o.AddressesTo, f => f.Make(randomNum.Next(1, 10), () => f.Internet.Email()))
            .RuleFor(o => o.Cc, f => f.Make(randomNum.Next(0, 10), () => f.Internet.Email()))
            .RuleFor(o => o.Bcc, f => f.Make(randomNum.Next(0, 10), () => f.Internet.Email()))
            .RuleFor(o => o.InReplyTo, f => f.Internet.Email())
            .RuleFor(o => o.MessageId, f => Guid.NewGuid().ToString());

        EmailHeaderFaker = new Faker<ImfHeaderDTO>()
            .RuleFor(o => o.Name, f => f.PickRandom(HeaderMessageNames))
            .RuleFor(o => o.Value, f => f.Internet.Email());

        EmailMessageFaker = new Faker<ImfMessageDTO>()
            .RuleFor(o => o.Headers, f => EmailHeaderFaker.Generate(randomNum.Next(0, 5)))
            .RuleFor(o => o.Payload, f => f.Lorem.Paragraphs(randomNum.Next(1, 5)));

        EmailAttachmentFaker = new Faker<ImfAttachmentDTO>()
            .RuleFor(o => o.Filename, f => f.System.FileName())
            .RuleFor(o => o.Path, f => f.System.FilePath())
            .RuleFor(o => o.ContentEncoding, f => f.PickRandom(SharedDTOsSeeds.FileEncodings))
            .RuleFor(o => o.ContentType, f => f.PickRandom(SharedDTOsSeeds.FileTypes));

        FlowStatisticsSnapshotFaker = new Faker<FlowStatisticsSnapshotDTO>()
            .RuleFor(o => o.FirstSeenTimestampTicks,
                f => (int)DateTime.UtcNow.Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                    .TotalSeconds)
            .RuleFor(o => o.LastSeenTimestampTicks,
                f => (int)DateTime.UtcNow.Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                    .TotalSeconds)
            .RuleFor(o => o.UpdatedAtTicks,
                f => (int)DateTime.UtcNow.Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                    .TotalSeconds)
            .RuleFor(o => o.ProcessedFrames, f => randomNum.Next(0, 99999))
            .RuleFor(o => o.ProcessedHeaderBytes, f => randomNum.Next(0, 99999))
            .RuleFor(o => o.ProcessedPayloadBytes, f => randomNum.Next(0, 99999))
            .RuleFor(o => o.MalformedFrames, f => randomNum.Next(0, 99999))
            .RuleFor(o => o.MalformedBytes, f => randomNum.Next(0, 99999))
            .RuleFor(o => o.DiscardedFrames, f => randomNum.Next(0, 99999))
            .RuleFor(o => o.DiscardedBytes, f => randomNum.Next(0, 10000));
    }

    public static Faker<HttpHeaderDTO> GetHttpHeaderFaker()
    {
        Random randomNum = new Random();
        var headerNameIndex = randomNum.Next(0, 11);
        return new Faker<HttpHeaderDTO>()
            .RuleFor(o => o.Name, f => HttpHeaderNames[headerNameIndex])
            .RuleFor(o => o.Value, f => f.PickRandom(HttpHeaderValues[headerNameIndex]));
    }
}