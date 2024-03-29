# DisJockey

This is a web application that hosts a Discord Bot, intended to play and track music that is played on a Discord server. It is build using .NET 5 Web API, Angular 11 and the Bot uses Discord.NET and Victoria (https://github.com/Yucked/Victoria).

I used the following repositories as a guide for my work so far, thanks to the authors of them:  
Eliza: https://github.com/Pheubel/Eliza  
StreamMusicBot: https://github.com/DraxCodes/StreamMusicBot  

# Setup

First, setup a google cloud application with access to the YouTube API, which is used to grab video details on track play.

You'll also need access to a LavaLink server (https://github.com/Frederikam/Lavalink). You can host one on your local machine using the latest jar files, run it Docker Container or I found a Github repo that does One click hosting to Heroku for free (https://github.com/karyeet/heroku-lavalink).

I've included a skeleton `appsettings.json` file in the root of the repo, you'll need to populate each of the empty properties with corresponding values and put it in the API folder.

Finally, you need to add `{Application Hostname}/signin-discord` to the redirect URLs for your Discord Application, which is to ensure Discord Auth is successful.

# Running

You can launch the app by running `dotnet run` from inside `/DisJockey`. I've also included vscode launch options so you can debug through that too.

I've started using Visual Studio 2019, so have included the Angular App in `/DisJockey/ClientApp`.

To make changes to Angular, you'll need to run `ng build` inside the client folder, as the API needs to serve static HTML files for Discord Authentication to work (this might be possible with `ng serve` but I haven't managed it). You can use the argument `--watch` to automatically build the Angular solution when you make changes to it. Alternatively, I've added an npm command to `package.json`, which you can run by calling `npm run build`. I may modify this at some point to serve up the Angular App as an SPA, but it wasn't high priority as the current approach works well.

# Contributing

Currently, the project as a whole is still in early stages, but I'm fairly new to .NET and still learning! If you would like to contribute or have any tips, I'm all ears.
