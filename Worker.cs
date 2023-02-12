namespace IHostService;

public class FileWatcherService : IHostedService
{
    FileSystemWatcher? watcher;
    string filePath = "folderEvents.txt";
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        watcher = new FileSystemWatcher("C:\\");
 
        // записываем изменения
        watcher.Changed += async (o, e) => await File.
        AppendAllTextAsync(filePath, $"{DateTime.Now} Changed: {e.FullPath}\n");

        // записываем данные о создании файлов и папок
        watcher.Created += async (o, e) => await File.
        AppendAllTextAsync(filePath, $"{DateTime.Now} Created: {e.FullPath}\n");

        // записываем данные об удалении файлов и папок
        watcher.Deleted += async (o, e) => await File.
        AppendAllTextAsync(filePath, $"{DateTime.Now} Deleted: {e.FullPath}\n");

        // записываем данные о переименовании
        watcher.Renamed += async (o, e) => await File.
        AppendAllTextAsync(filePath, $"{DateTime.Now} Renamed: {e.OldFullPath} to {e.FullPath}\n");

        // записываем данные об ошибках
        watcher.Error += async (o, e) => await File.
        AppendAllTextAsync(filePath, $"{DateTime.Now} Error: {e.GetException().Message}\n");
 
        Console.WriteLine("Hello");

        watcher.IncludeSubdirectories = true; // отслеживаем изменения в подкаталогах
        watcher.EnableRaisingEvents = true;    // включаем события
        await Task.CompletedTask;
    }
 
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        watcher?.Dispose();
        await Task.CompletedTask;
    }

    }
