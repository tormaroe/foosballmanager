# foosballmanager

A web app for managing foosball game stats and leagues. 
Originally hosted on code.google.com/p/foosballmanager.

This project started in september 2007 when we needed a a tool to manage statistics for the foosball games 
we played at work. Due to popular demand I've open sourced it. So feel free to download and use it however 
you like. And if you are a programmer and would like to help me make it better, or even just have suggestions 
on how I could enhance the application, please contact me.

Simple installation instructions follows below the application highlight screen shots.

## The player table

![The player table](https://raw.githubusercontent.com/tormaroe/foosballmanager/master/Screenshots/Players.Png)

## Advanced statistics

![Advanced statistics](https://raw.githubusercontent.com/tormaroe/foosballmanager/master/Screenshots/Stats.Png)

## Create and manage leagues

![Create and manage leagues](https://raw.githubusercontent.com/tormaroe/foosballmanager/master/Screenshots/League.Png)

## Full audit trail available

![Full audit trail available](https://raw.githubusercontent.com/tormaroe/foosballmanager/master/Screenshots/AuditTrail.Png)

## Installing the manager

These instructions are really old :-)

### Requirements

* Up-to-date Windows machine running IIS
* and you should have familiarity with how to manage IIS

### Installation process

* Download the latest version (link to the right)
* Unzip the content to somewhere that IIS can run it, for instance c:\inetpub\wwwroot\.
* Modify the paths to where you want the application to store it's data. Data is stored as XML files, but the app needs to know where it should store it, and have write access to the folder. The four paths you need to change are located at the bottom of the Web.config file. Edit the file using notepad. The properties you need to change are playersfile, audittrailfile, leaguematchesfile, and leagueplayersfile.
* Configure the new app in IIS (like add a new virtual folder if the manager isn't located under the root).
* You should be ready to go. Browse to your application and see it shine.

### Important information

* Some operations, like creating a new League, require you to provide an admin password - mostly so that you don't screw up by mistake. The password is obifuss.
