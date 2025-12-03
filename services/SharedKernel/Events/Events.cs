namespace SharedKernel.Events;

public interface ITrendDiscovered
{
    string TrendName { get; }
    // Potential extra data: Region, SearchVolume, etc.
}

public interface IGameIdeaGenerated
{
    string TrendName { get; }
    string GameTitle { get; }
    string GameDescription { get; }
    string Genre { get; }
    // This ID will track the game through the pipeline
    Guid GameId { get; }
}

public interface IGameCodeGenerated
{
    Guid GameId { get; }
    string HtmlContent { get; }
    string CssContent { get; }
    string JsContent { get; }
}

public interface IGameAssetsGenerated
{
    Guid GameId { get; }
    // Dictionary of AssetName -> AssetUrl or Base64
    System.Collections.Generic.Dictionary<string, string> Assets { get; }
}

public interface IGameBuilt
{
    Guid GameId { get; }
    string BuildPath { get; } // Or URL if uploaded immediately
}
