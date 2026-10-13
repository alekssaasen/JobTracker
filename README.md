# JobTracker

Et personlig prosjekt hvor jeg bygger en tjeneste for å holde oversikt over jobbsøknader og lagre CV-informasjon på ett sted. Jeg studerer backend-programmering (høyere fagskole), og prosjektet er laget for å lære og øve på det jeg møter i studiet.

## Hva prosjektet inneholder

**Backend (`JobTracker.API`)** – ASP.NET Core Web API med C#

- Registrering og innlogging med JWT (JSON Web Token), og passord som lagres hashet
- CRUD (opprette, lese, oppdatere, slette) for:
  - Jobbsøknader og selskaper
  - CV-data: arbeidserfaring, utdanning, ferdigheter, prosjekter og referanser
- Hver bruker ser og endrer kun sine egne data
- DTO-er (Data Transfer Objects) for å styre hva klienten kan sende inn
- Database med Entity Framework Core og SQLite
- CORS (Cross-Origin Resource Sharing) og global feilhåndtering

**Frontend (`JobTracker.Client`)** – Blazor WebAssembly

- Registreringsside som kaller API-et (under utvikling)

## Teknologi

C#, .NET 10, ASP.NET Core, Entity Framework Core, SQLite, Blazor WebAssembly, Git

## Slik kjører du prosjektet

1. Opprett filen `JobTracker.API/appsettings.Development.json` (den ligger ikke i Git fordi den inneholder en hemmelig nøkkel):

   ```json
   {
     "Jwt": {
       "Key": "en-lang-tilfeldig-tekst-på-minst-32-tegn",
       "Issuer": "JobTrackerAPI",
       "Audience": "JobTrackerClient"
     }
   }
   ```

2. Opprett databasen:

   ```
   cd JobTracker.API
   dotnet ef database update
   ```

3. Start API-et (kjører på `http://localhost:5074`):

   ```
   dotnet run
   ```

4. Start klienten i et nytt terminalvindu:

   ```
   cd JobTracker.Client
   dotnet run
   ```

## Planer videre

- Innlogging og beskyttede sider i frontend
- Hente inn stillingsannonser
- Foreslå stillinger basert på brukerens profil
- Generere utkast til søknadstekst

## Hva jeg har lært

Relasjoner mellom tabeller, autentisering, sikker håndtering av brukerdata, feilsøking av migrasjoner og pakkeversjoner, og hvordan en frontend og en backend snakker sammen.
