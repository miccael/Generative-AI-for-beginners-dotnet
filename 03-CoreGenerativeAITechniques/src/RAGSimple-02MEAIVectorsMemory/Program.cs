using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;



IChatClient client =
    new OllamaChatClient(new Uri("http://localhost:11434/"), "phi4-mini");

//var response = client.GetStreamingResponseAsync("What is AI?");



var conversation = new List<ChatMessage>
{

};

            var vectorStore = new InMemoryVectorStore();

            // get movie list
            var movies = vectorStore.GetCollection<int, MovieVector<int>>("movies");
            await movies.CreateCollectionIfNotExistsAsync();
            var movieData = MovieFactory<int>.GetMovieVectorList();

            // get embeddings generator and generate embeddings for movies
            IEmbeddingGenerator<string, Embedding<float>> generator =
                new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "all-minilm");
            foreach (var movie in movieData)
            {
                movie.Vector = await generator.GenerateEmbeddingVectorAsync(movie.Description);
                await movies.UpsertAsync(movie);
            }

            // perform the search
            var query = "A family friendly movie that includes ogres and dragons";
            var queryEmbedding = await generator.GenerateEmbeddingVectorAsync(query);
            var searchOptions = new VectorSearchOptions()
            {
                Top = 2,
                VectorPropertyName = "Vector"
            };

            var results = await movies.VectorizedSearchAsync(queryEmbedding, searchOptions);

            await foreach (var result in results.Results)
            {
                Console.WriteLine($"Title: {result.Record.Title}");
                Console.WriteLine($"Description: {result.Record.Description}");
                Console.WriteLine($"Score: {result.Score}");
                Console.WriteLine();
            }
        
// assuming chatClient is instatiated as before to a language model
// assuming the vector search is done as above
// assuming List<ChatMessage> conversation object is already instantiated and has a system prompt

conversation.Add(new ChatMessage(ChatRole.User, query)); // this is the user prompt

// ... do the vector search

// add the search results to the conversation
await foreach (var result in results.Results)
{
    conversation.Add(new ChatMessage(ChatRole.User, $"This movie is playing nearby: {result.Record.Title} and it's about {result.Record.Description}"));
}



// send the conversation to the model
var response = await client.GetResponseAsync("conversation");

// add the assistant message to the conversation
//conversation.Add(new ChatMessage(ChatRole.Assistant, response.Message));

//display the conversation
Console.WriteLine($"Bot:> {response} .Message.Text");