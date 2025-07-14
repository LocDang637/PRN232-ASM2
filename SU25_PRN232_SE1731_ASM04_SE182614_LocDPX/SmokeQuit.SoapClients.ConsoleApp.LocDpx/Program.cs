using ChatsLocDpxServiceReference; // Use the actual generated namespace

Console.WriteLine("=== SOAP Chat Service Client ===");

// Create SOAP client
var client = new ChatsLocDpxSoapServiceClient(
    ChatsLocDpxSoapServiceClient.EndpointConfiguration.BasicHttpBinding_IChatsLocDpxSoapService);

try
{
    while (true)
    {
        Console.WriteLine("\n1. Get All Chats");
        Console.WriteLine("2. Create New Chat");
        Console.WriteLine("3. Get Chat by User ID");
        Console.WriteLine("4. Exit");
        Console.Write("Choose option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await GetAllChats(client);
                break;
            case "2":
                await CreateChat(client);
                break;
            case "3":
                await GetChatByUserId(client);
                break;
            case "4":
                return;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static async Task GetAllChats(ChatsLocDpxSoapServiceClient client)
{
    try
    {
        var chats = await client.GetAllAsync();

        Console.WriteLine($"\nFound {chats.Length} chats:");
        foreach (var chat in chats)
        {
            Console.WriteLine($"ID: {chat.ChatsLocDpxid}, User: {chat.UserId}, Message: {chat.Message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting chats: {ex.Message}");
    }
}

static async Task CreateChat(ChatsLocDpxSoapServiceClient client)
{
    try
    {
        Console.Write("Enter User ID: ");
        var userId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Coach ID: ");
        var coachId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Message: ");
        var message = Console.ReadLine() ?? "";

        Console.Write("Enter Sent By: ");
        var sentBy = Console.ReadLine() ?? "";

        var newChat = new ChatsLocDpx
        {
            UserId = userId,
            CoachId = coachId,
            Message = message,
            SentBy = sentBy,
            MessageType = "text",
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        var result = await client.CreateAsync(newChat);
        Console.WriteLine($"Chat created with result: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating chat: {ex.Message}");
    }
}

static async Task GetChatByUserId(ChatsLocDpxSoapServiceClient client)
{
    try
    {
        Console.Write("Enter User ID: ");
        var userId = int.Parse(Console.ReadLine() ?? "0");

        var chat = await client.GetByIdAsync(userId);

        if (chat != null)
        {
            Console.WriteLine($"Chat found - ID: {chat.ChatsLocDpxid}, Message: {chat.Message}");
        }
        else
        {
            Console.WriteLine("Chat not found!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting chat: {ex.Message}");
    }
}