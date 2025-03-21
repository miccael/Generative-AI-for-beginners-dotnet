using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using System.Text;



static string GetTheWeather()
{    
    var temperature = Random.Shared.Next(5, 20);

    var conditions = Random.Shared.Next(0, 1) == 0 ? "sunny" : "rainy";

    return $"The weather is {temperature} degrees C and {conditions}.";
}

var chatOptions = new ChatOptions
{
    Tools = [AIFunctionFactory.Create(GetTheWeather)]
};

var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
if(string.IsNullOrEmpty(githubToken))
{
    var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
    githubToken = config["GITHUB_TOKEN"];
}

// IChatClient client = new ChatCompletionsClient(
//         endpoint: new Uri("https://models.inference.ai.azure.com"),
//         new AzureKeyCredential(githubToken))
//         .AsChatClient("Phi-3.5-MoE-instruct");

IChatClient client = new ChatCompletionsClient(
    endpoint: new Uri("https://models.inference.ai.azure.com"),
    new AzureKeyCredential(githubToken)) // githubToken is retrieved from the environment variables
.AsChatClient("gpt-4o-mini")
.AsBuilder()
.UseFunctionInvocation()  // here we're saying that we could be invoking functions!
.Build();

// // here we're building the prompt
// StringBuilder prompt = new StringBuilder();
// prompt.AppendLine("You will analyze the sentiment of the following product reviews. Each line is its own review. Output the sentiment of each review in a bulleted list and then provide a generate sentiment of all reviews. ");
// prompt.AppendLine("I bought this product and it's amazing. I love it!");
// prompt.AppendLine("This product is terrible. I hate it.");
// prompt.AppendLine("I'm not sure about this product. It's okay.");
// prompt.AppendLine("I found this product based on the other reviews. It worked for a bit, and then it didn't.");

// // send the prompt to the model and wait for the text completion
// var response = await client.GetResponseAsync(prompt.ToString());

var responseOne = await client.GetResponseAsync("What is today's date", chatOptions); // won't call the function

var responseTwo = await client.GetResponseAsync("Should I bring an umbrella with me today?", chatOptions); // will call the function

// display the response
Console.WriteLine(responseTwo.Message);
