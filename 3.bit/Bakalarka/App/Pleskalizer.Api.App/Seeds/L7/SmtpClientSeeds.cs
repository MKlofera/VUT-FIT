using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class SmtpClientSeeds
{
    public const int NumberOfSmtpClientMessages = 98;
    public static readonly SmtpClientMessageDTOPageQueryResultDTO SmtpClientMessageListSeed = new();

    public static readonly SmtpClientMessageDTO SmtpClientMessageSeed1 = new()
    {
        SessionId = Guid.Parse("943ae02c-5484-47a5-8935-4530ac27657f"),
        Command = SmtpClientMessageDTO.CommandEnum.NOCOMMANDEnum,
        Parameters = new List<string> {""},
        Envelope = new ImfEnvelopeDTO
        {
            DateTicks =  1678477296,
            Subject = "rendezvous",
            AddressesFrom = new List<string>() {"sneakyg33k@aol.com"},
            Sender = "Ann Dercover",
            ReplyTo = new List<string>(),
            AddressesTo = new List<string>() {"mistersecretx@aol.com"},
            Cc = new List<string>(),
            Bcc = new List<string>(),
            InReplyTo = "",
            MessageId = "001101ca49ae$e93e45b0$9f01a8c0@annlaptop"
        },
        Email = new ImfMessageDTO
        {
            Headers = new List<ImfHeaderDTO>
            {
                new ImfHeaderDTO
                {
                    Name = "",
                    Value = ""
                }
            },
            Payload = "Hi sweetheart! Bring your fake passport and a bathing suit. Address attached. love, Ann"
        },
        MailPath = "mailPath",
        Attachments = new List<ImfAttachmentDTO>()
        {
            new ImfAttachmentDTO
            {
                Path = "cesta k souboru",
                Filename = "secretrendezvous.docx",
                ContentType = "application/octet-stream",
                ContentEncoding = "base64"
            }
        },
        Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(2002, 3, 4)).TotalSeconds
    };
    
    public static readonly SmtpClientMessageDTO SmtpClientMessageSeed2 = new()
    {
        SessionId = Guid.Parse("253ae02c-5484-47a5-8935-4530ac27657f"),
        Command = SmtpClientMessageDTO.CommandEnum.NOCOMMANDEnum,
        Parameters = new List<string> {""},
        Envelope = new ImfEnvelopeDTO
        {
            DateTicks =  1678477296,
            Subject = "rendezvous",
            AddressesFrom = new List<string>() {"sneakyg33k@aol.com"},
            Sender = "Ann Dercover",
            ReplyTo = new List<string>(),
            AddressesTo = new List<string>() {"sec558@gmail.com"},
            Cc = new List<string>(),
            Bcc = new List<string>(),
            InReplyTo = "",
            MessageId = "000901ca49ae$89d698c0$9f01a8c0@annlaptop"
        },
        Email = new ImfMessageDTO
        {
            Headers = new List<ImfHeaderDTO>
            {
                new ImfHeaderDTO
                {
                    Name = "",
                    Value = ""
                }
            },
            Payload = "Sorry-- I can't do lunch next week after all. Heading out of town. Another time! -Ann"
        },
        MailPath = "mailPath",
        Attachments = new List<ImfAttachmentDTO>(),
        Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1990, 1, 1)).TotalSeconds
    };

    static SmtpClientSeeds()
    {
        Random randomNum = new Random();

        var smtpClientFaker = new Faker<SmtpClientMessageDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.Command, f=> f.Random.Enum<SmtpClientMessageDTO.CommandEnum>())
            .RuleFor(o => o.Parameters,  f => f.Make(randomNum.Next(0, 10), () => f.Internet.Email()))
            .RuleFor(o => o.Envelope, f => SharedDTOsSeeds.EmailEnvelopeFaker.Generate(1)[0])
            .RuleFor(o => o.Email, f => SharedDTOsSeeds.EmailMessageFaker.Generate(1)[0])
            .RuleFor(o => o.MailPath, f => f.PickRandom(SharedDTOsSeeds.EmailPath)) 
            .RuleFor(o => o.Attachments, f => SharedDTOsSeeds.EmailAttachmentFaker.Generate(randomNum.Next(0,10)))
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow.Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds);
        SmtpClientMessageListSeed.Items = smtpClientFaker.Generate(NumberOfSmtpClientMessages);
        
        SmtpClientMessageListSeed.Items.Add(SmtpClientMessageSeed1);
        SmtpClientMessageListSeed.Items.Add(SmtpClientMessageSeed2);
    }
}