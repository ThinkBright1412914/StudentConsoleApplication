using Microsoft.Extensions.DependencyInjection;
using StudentConsoleApplication;


var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<App>();
serviceCollection.AddSingleton<Repository, Services>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<App>();

app.Main();