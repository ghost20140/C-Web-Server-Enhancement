﻿using System.Text.Json;

static void TestJSON() {
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    string text = File.ReadAllText(@"config.json");
    var config = JsonSerializer.Deserialize<Config>(text, options);

    Console.WriteLine($"MimeTypes : {config.MimeTypes[".html"]}");
    Console.WriteLine($"IndexFiles : {config.IndexFiles[0]}");
}
static void TestJSON2() {
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    string text = File.ReadAllText(@"json/books.json");
    var books = JsonSerializer.Deserialize<List<Book>>(text, options);

    Book book = books[4];
    Console.WriteLine($"title: {book.Title}");
    Console.WriteLine($"authors: {book.Authors[0]}");
}

static void TestServer() {
    SimpleHTTPServer server = new SimpleHTTPServer("files", 8080,"config.json");
    string helpMessage = @"Server started. You can use the following commands:
        help - display this message
        stop - stop the server
        numreqs - display the number of requests
        path - display the number of times each path was requested
        404req - display the number of 404 requests
        ";
Console.WriteLine($"Server started !\n {helpMessage}");
    while (true)
    {
        Console.Write("> ");
        // read line from console
        String command = Console.ReadLine();
        if (command.Equals("stop"))
        {
            server.Stop();
            break;
        }else if(command.Equals("help")){
            Console.WriteLine(helpMessage);
        }else if(command.Equals("numreqs")){
            Console.WriteLine($"Number of requests: {server.NumRequests}");
        }else if(command.Equals("paths")){
            foreach(var path in server.PathsRequessted){
                Console.WriteLine($"{path.Key} : {path.Value}");
            }
        }else if(command.Equals("404req")){
            foreach(var path in server.Errorcount)
            {
                Console.WriteLine($"{path.Key} : {path.Value}");
            }
        }
        else{
            Console.WriteLine($"Unknown command: {command}");
        }
       // else Console.Write("Not accessible");
    }
}

//TestJSON();
TestServer();
