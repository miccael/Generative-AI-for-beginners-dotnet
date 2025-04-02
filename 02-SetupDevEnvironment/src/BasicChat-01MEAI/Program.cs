using Azure;
using Azure.AI.Inference;


using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;



var deploymentName = "gpt-4o-mini"; // e.g. "gpt-4o-mini"
var endpoint = new Uri("https://ai-mikaelglanz6441ai242952280448.services.ai.azure.com/"); // e.g. "https://< your hub name >.openai.azure.com/"
var apiKey = new ApiKeyCredential(Environment.GetEnvironmentVariable("AZURE_AI_SECRET"));

IChatClient client = new AzureOpenAIClient(
    endpoint,
    apiKey)
.AsChatClient(deploymentName);

var response = await client.GetResponseAsync("What is a Banana? Write only a single sentence.");

// Write a line with the response
Console.WriteLine(response.Message);