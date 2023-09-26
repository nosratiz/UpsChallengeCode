# UpsChallengeCode

 
To start the project, we should include an API key for our HTTP API request. Dotnet Secret is an easy and safe method to save and control secrets in our application. After adding the API key, we can obtain the necessary data and proceed with the project.

for installation
````
dotnet tool install -g dotnet-user-secrets

````

to set api key for use
`````
dotnet user-secrets set "UpsApiService:ApiKey" "Api-key-in-document"
``````

I'm working on a project that involves three distinct sections: Ups.App for the UI, Ups.Application for the business logic, and Ups.Common for utilities and helpers. Maintaining separation between these sections is crucial for an organized and efficient workflow. 



# Reseliancy and Clean Code

This app is built to withstand unexpected server issues and remain reliable. I've implemented a retry policy using Polly to ensure our users have constant access to the resources they need. If there's ever a disruption in our server, our app will automatically try again until it successfully connects, minimizing downtime for our users. You can always count on our app to be there when needed.


#MediatR

My codebase now incorporates the CQRS pattern, significantly improving clarity and organization. This approach lets me differentiate between various components and functionalities, leading to a more streamlined and effective codebase.



#FluentValidation

I have integrated FluentValidation into My code to separate validation and business logic. This enhances the readability and maintainability of our code and enables us to handle validation errors more efficiently and offers improved user feedback. In general, it has been a valuable addition to My development process.



# DI
 I Use two packages for leveraging DI in this app and now we can easlily use DI in our App and for config setting use appsetting.json if we later on we want user IOption pattern in our app

 ````
   <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="7.0.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="7.0.1" />

     AppHost = Host.CreateDefaultBuilder().ConfigureServices(((context, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddApplication(Config);

        } )).Build();

````


### Export

If you're interested in implementing export in Excel, it's recommended to have a separate API that provides a file or Blob from the server. Another useful option would be to have an API that gives you a comprehensive list of all users in one place, without requiring pagination. 














