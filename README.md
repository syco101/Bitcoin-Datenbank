# Krypto-Datenbank (Backend-Projekt)

Dieses Projekt ist eine mit .NET Core entwickelte REST-API zur Verwaltung von Bitcoin-Investments. Die API enthält grundlegende Funktionen zur Datenverarbeitung und bietet ein sicheres JWT-basiertes Authentifizierungssystem.

## 📁 Projektstruktur

- `Controllers/` – API-Endpunkte für Holdings, Benutzer und Authentifizierung
- `Models/` – Datenmodelle für Benutzer, Token, Holdings
- `DTOs/` – Objekte zur sicheren Datenübertragung (ohne sensible Felder)
- `Services/` – Business-Logik, z. B. Token-Generierung, Userverwaltung
- `Repositories/` – Zugriff auf Datenbank mit Entity Framework
- `Data/` – DB-Context mit SQLite-Anbindung
- `Program.cs / Startup.cs` – Konfiguration von Middleware, Services und Auth

## 🔐 Funktionen

- JWT-Authentifizierung mit Access & Refresh Token
- Token-Refresh-Mechanismus
- Benutzer-Registrierung & Login
- CRUD-Funktionen für Bitcoin-Holdings
- Fehlerbehandlung und Validierung
- CORS-Konfiguration für Frontend-Zugriff
- Swagger-UI zur API-Dokumentation (automatisch generiert)
- Coinmarketcap API mit intigriert für Live Bitcoin-Preis Abfrage

## 🧠 Reflexion

Ich habe bei diesem Projekt sehr viel über Backend-Architektur gelernt – besonders wie man Schichten sauber trennt und mit DTOs arbeitet. Das Thema JWT war für mich zu Beginn nicht ganz einfach zu verstehen, vor allem das Zusammenspiel von Access-Token, Refresh-Token und Token-Validity.  
Nach mehreren Versuchen hat es aber geklappt und ich habe ein besseres Verständnis dafür entwickelt, wie sichere Authentifizierung in echten APIs funktioniert.

Auch Entity Framework war eine neue Erfahrung – es war spannend zu sehen, wie schnell man damit Datenmodelle in SQLite persistieren kann. Auch Swagger UI war Neuland für mich aber die Aufträge in diesem Modul hatten mir sehr geholfen.


Insgesamt war das Projekt sehr lehrreich für mich.
