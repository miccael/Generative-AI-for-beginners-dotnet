# What's New in Generative AI for Beginners .NET

This page tracks the history of new features, tools, and models added to the course. Check back for updates!

## March 2025

### MCP Library Integration
- **Model Context Protocol for .NET**: We've added support for the [MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk), which provides a standardized way to communicate with AI models across different providers.
- This integration enables more consistent interactions with models while reducing provider lock-in.
- Check out our new samples demonstrating MCP integration in the [Core Generative AI Techniques](../03-CoreGenerativeAITechniques/) section.

## February 2025

### phi4-mini Model Support
- The [Ollama Codespace](https://github.com/microsoft/Generative-AI-for-beginners-dotnet/blob/main/02-SetupDevEnvironment/getting-started-ollama.md) now automatically downloads the [phi4-mini model](https://ollama.com/library/phi4-mini) - Microsoft's compact yet powerful LLM.
- Try it in samples like:
  - [Chat Application](https://github.com/microsoft/Generative-AI-for-beginners-dotnet/blob/main/03-CoreGenerativeAITechniques/src/BasicChat-03Ollama/Program.cs) - Experience fast responses with this efficient model
  - [RAG Implementation](https://github.com/microsoft/Generative-AI-for-beginners-dotnet/blob/main/03-CoreGenerativeAITechniques/src/RAGSimple-10SKOllama/Program.cs) - See how phi4-mini handles retrieval-augmented generation tasks
  - Learn more about the model in the [Phi Cookbook](https://aka.ms/phicookbook)