using BestPath;
using BestPath.Algos;
using BestPath.Algos.AStar;
using BestPath.Algos.AStar.Heuristics;
using BestPath.Models.EventArgs;
using Spectre.Console;

GraphInputs graphInputs = new();
string location = AnsiConsole.Prompt(new SelectionPrompt<string>()
    .Title("Selecione uma [bold]localização[/] dos EUA:")
    .AddChoices(graphInputs.locations));
var (coordinatesFile, distanceFile) = graphInputs.GetLocationFiles(location);

uint startNodeId = AnsiConsole.Prompt(
    new TextPrompt<uint>("Digite o ID do [cyan]nó de origem[/]:")
        .ValidationErrorMessage("[red]Nó não encontrado![/]"));
uint goalNodeId = AnsiConsole.Prompt(
    new TextPrompt<uint>("Digite o ID do nó de destino:")
        .ValidationErrorMessage("Nó não encontrado!"));

bool runSync = AnsiConsole
    .Confirm("Deseja executar os algoritimos de forma [bold]sincrona[/]?");

Table algorithmsTable = new Table()
    .BorderColor(Color.Navy)
    .Title("Algoritimos que serão executados.");
algorithmsTable.AddColumn("Algoritimo");
algorithmsTable.AddColumn("Origem");
algorithmsTable.AddColumn("Destino");
algorithmsTable.AddRow("DFS", startNodeId.ToString(), goalNodeId.ToString());
algorithmsTable.AddRow("BFS", startNodeId.ToString(), goalNodeId.ToString());
algorithmsTable.AddRow("UCS", startNodeId.ToString(), goalNodeId.ToString());
algorithmsTable.AddRow("A*", startNodeId.ToString(), goalNodeId.ToString());
AnsiConsole.Write(algorithmsTable);

GraphParser graphParser = new(coordinatesFile, distanceFile);
var graphs = GetGraphsGenerator();
List<IAlgorithmGraph> algorithmGraphs = new();
List<IResultSnapshot> results = new();
await AnsiConsole.Status()
    .Spinner(Spinner.Known.Earth)
    .StartAsync("Executando algoritimos...", GenerateGraphs);
await AnsiConsole.Progress()
    .Columns(new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new SpinnerColumn(Spinner.Known.Earth),
        new ElapsedTimeColumn())
    .StartAsync(context => RunAlgos(context, runSync));

Console.WriteLine("Algoritmos executados!");

foreach (IResultSnapshot result in results)
    Console.WriteLine(result.ToString());

return;

async IAsyncEnumerable<IAlgorithmGraph> GetGraphsGenerator()
{
    yield return await graphParser.ParseToBfsGraph();
    yield return await graphParser.ParseToDfsGraph();
    yield return await graphParser.ParseToUcsGraph();
    yield return await graphParser.ParseToAStarGraph();
}

async Task GenerateGraphs(StatusContext context)
{
    context.Spinner(Spinner.Known.Circle);
    context.Status("[cyan]Gerando grafos...[/]");
    await foreach(IAlgorithmGraph? graph in graphs)
    {
        AnsiConsole.MarkupLine($"[springgreen1]Grafo {graph.algorithmName} gerado![/]");
        algorithmGraphs.Add(graph);
    }
    AnsiConsole.MarkupLine("[green]Grafos gerados![/]");
    context.Spinner(Spinner.Known.Earth);
    context.Status("[cyan]Executando algoritimos...[/]");
}

async Task RunAlgos(ProgressContext context, bool runAlgoSync)
{
    var tasks = algorithmGraphs.Select(algo =>
    {
        return Task.Run(async () =>
        {
            switch(algo)
            {
                case AStarGraph aStarGraph:
                    await RunAStarGraphAlgo(aStarGraph);
                    break;
                default:
                    RunRegularAlgorithm(algo);
                    break;
            };
        });
    });
    
    if(runAlgoSync)
        foreach(Task task in tasks)
            await task;
    else await Task.WhenAll(tasks);
    
    return;
    void Finish(object sender, AlgosPartialResultSnapshotEventArgs eventArgs)
    {
        AnsiConsole.MarkupLine($"[purple]{sender.GetType()} encerrado:[/] [green]{eventArgs.elapsedTime}[/]");
    }
    void RunRegularAlgorithm(IAlgorithmGraph algo)
    {
        ProgressTask progress = context.AddTask($"[cyan]{algo.algorithmName}[/]")
            .IsIndeterminate();
        algo.OnFinish += Finish;
        IResultSnapshot resultSnapshot = algo.RunAlgo(startNodeId, goalNodeId);
        progress.Increment(100);
        progress.StopTask();
        results.Add(resultSnapshot);
    }
    async Task RunAStarGraphAlgo(AStarGraph graph)
    {
        ProgressTask progress = context.AddTask($"[cyan]{graph.algorithmName}[/]");
        graph.OnFinish += Finish;
        IResultSnapshot flatEarthResult = graph.RunAlgo(startNodeId, goalNodeId);
        results.Add(flatEarthResult);
        progress.Increment(30);
        AnsiConsole.MarkupLine("[springgreen1]Heuristica da terra-plana concluida[/]");
        
        AnsiConsole.MarkupLine("[cyan]Reiniciando grafo...[/]");
        graph = await graphParser.ParseToAStarGraph();
        
        progress.Increment(20);
        graph.Heuristic(new HaversineHeuristic());
        AnsiConsole.MarkupLine("[blue]Iniciando a heuristica de Haversine[/]");
        IResultSnapshot haversineResult = graph.RunAlgo(startNodeId, goalNodeId);
        results.Add(haversineResult);
        
        progress.Increment(50);
        progress.StopTask();
    }
}