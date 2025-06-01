<h1>🎮 MultiLevelTournament API</h1>

<p>A RESTful ASP.NET Core 8.0 Web API to manage players and multi-level CS tournaments.<br>
Supports nested sub-tournaments (up to 5 levels), player registration constraints, and a clean architecture.</p>

<hr/>

<h2>📋 Table of Contents</h2>
<ul>
  <li><a href="#features">Features</a></li>
  <li><a href="#requirements">Requirements</a></li>
  <li><a href="#setup--installation">Setup &amp; Installation</a></li>
  <li><a href="#database-migrations">Database Migrations</a></li>
  <li><a href="#running-the-api">Running the API</a></li>
  <li><a href="#swagger-ui">Swagger UI</a></li>
  <li><a href="#api-endpoints">API Endpoints</a></li>
  <li><a href="#validation-rules">Validation Rules</a></li>
  <li><a href="#project-structure">Project Structure</a></li>
  <li><a href="#contributing">Contributing</a></li>
  <li><a href="#license">License</a></li>
</ul>

<hr/>

<h2 id="features">🕹️ Features</h2>
<ul>
  <li>✅ <strong>Players</strong>: Create, Update, Delete, List</li>
  <li>✅ <strong>Tournaments</strong>:
    <ul>
      <li>Create, Update, Delete, List</li>
      <li>Nested Sub-Tournaments (up to 5 levels)</li>
      <li>Register players (parent tournament validation)</li>
      <li>Recursive details (sub-tournaments + players)</li>
    </ul>
  </li>
  <li>✅ <strong>Swagger UI</strong> for interactive API exploration</li>
  <li>✅ <strong>Clean Architecture</strong>: Controllers &rarr; Services &rarr; Repositories &rarr; Models</li>
</ul>

<hr/>

<h2 id="requirements">⚙️ Requirements</h2>
<ul>
  <li>[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)</li>
  <li>[SQL Server](https://www.microsoft.com/en-us/sql-server/) or LocalDB</li>
  <li>(Optional) [Postman](https://www.postman.com/) for manual testing</li>
</ul>

<hr/>

<h2 id="setup--installation">🚀 Setup &amp; Installation</h2>
<pre><code>git clone https://github.com/Md-Ruhul-Amin-Rony/MultiLevelTournament.git
cd MultiLevelTournament

dotnet restore
dotnet ef database update
dotnet run
</code></pre>
<p>Visit <code>https://localhost:{port}/swagger</code> to view Swagger UI.</p>

<hr/>

<h2 id="database-migrations">📦 Database Migrations</h2>
<p>This project uses Entity Framework Core Code-First migrations. To add a new migration:</p>
<pre><code>dotnet ef migrations add &lt;MigrationName&gt;
dotnet ef database update
</code></pre>
<p>Make sure to update the connection string in <code>appsettings.json</code> before running migrations.</p>

<hr/>

<h2 id="running-the-api">▶️ Running the API</h2>
<pre><code>dotnet run
</code></pre>
<p>By default, it will launch at:</p>
<ul>
  <li><code>https://localhost:7282/</code></li>
  <li><code>http://localhost:5235/</code></li>
</ul>

<hr/>

<h2 id="swagger-ui">📄 Swagger UI</h2>
<p>Navigate to:</p>
<pre><code>https://localhost:7282/swagger
</code></pre>
<p>to explore and test all endpoints interactively.</p>

<hr/>

<h2 id="api-endpoints">🔗 API Endpoints</h2>

<h3>Players</h3>
<ul>
  <li><strong>GET</strong> <code>/api/Player</code><br>
      Retrieve all players.</li>
  <li><strong>GET</strong> <code>/api/Player/{id}</code><br>
      Retrieve details of a single player.</li>
  <li><strong>POST</strong> <code>/api/Player</code><br>
      Create a new player.<br>
      <pre><code>
{
  "name": "Amin",
  "age": 25
}
      </code></pre>
  </li>
  <li><strong>PUT</strong> <code>/api/Player/{id}</code><br>
      Update existing player.<br>
      <pre><code>
{
  "name": "Ruhul AMin",
  "age": 26
}
      </code></pre>
  </li>
  <li><strong>DELETE</strong> <code>/api/Player/{id}</code><br>
      Delete a player by ID.</li>
  <li><strong>GET</strong> <code>/api/Player/Tournaments/{playerId}</code> (optional)<br>
      Retrieve all tournaments a player is registered in.</li>
</ul>

<h3>Tournaments</h3>
<ul>
  <li><strong>GET</strong> <code>/api/Tournament</code><br>
      Retrieve all tournaments (with nested sub-tournaments and players).</li>
  <li><strong>GET</strong> <code>/api/Tournament/{id}</code><br>
      Retrieve a single tournament by ID.</li>
  <li><strong>POST</strong> <code>/api/Tournament</code><br>
      Create a new tournament. If <code>parentTournamentId</code> is specified, it becomes a sub-tournament.<br>
      <pre><code>
{
  "name": "Summer Cup",
  "parentTournamentId": null
}
      </code></pre>
  </li>
  <li><strong>PUT</strong> <code>/api/Tournament/{id}</code><br>
      Update the name of an existing tournament.<br>
      <pre><code>
{
  "name": "Summer Cup Finals"
}
      </code></pre>
  </li>
  <li><strong>DELETE</strong> <code>/api/Tournament/{id}</code><br>
      Delete a tournament. Fails if it has sub-tournaments.</li>
  <li><strong>POST</strong> <code>/api/Tournament/{tournamentId}/register?playerId={playerId}</code><br>
      Register a player in a tournament. If it’s a sub-tournament, the player must be in the parent first.</li>
  <li><strong>GET</strong> <code>/api/Tournament/{tournamentId}/players</code><br>
      Retrieve players registered in a specific tournament.</li>
</ul>

<hr/>

<h2 id="validation-rules">🛡️ Validation Rules</h2>
<ul>
  <li><strong>Tournament Name</strong>: Required, at least 2 characters.</li>
  <li><strong>Player Name</strong>: Required.</li>
  <li><strong>Player Age</strong>: Must be between 1 and 100.</li>
  <li><strong>Tournament Nesting</strong>: Cannot create a sub-tournament deeper than 5 levels (root = level 0, deepest allowed = level 4).</li>
  <li><strong>Player Registration</strong>: Player must be registered in the parent tournament before registering in a sub-tournament.</li>
  <li><strong>Deletion</strong>: Cannot delete a tournament that still has sub-tournaments.</li>
</ul>

<hr/>

<h2 id="project-structure">📂 Project Structure</h2>
<pre><code>MultiLevelTournament/
│
├── Controllers/          # API layer (PlayerController, TournamentController)
├── Services/             # Business logic (IPlayerService, ITournamentService, implementations)
├── Repositories/         # Data access (IPlayerRepository, ITournamentRepository, implementations)
├── Models/               # Domain models &amp; ViewModels/DTOs
│   ├── Player.cs
│   ├── Tournament.cs
│   ├── TournamentPlayer.cs
│   ├── PlayerViewModel.cs
│   ├── TournamentViewModel.cs
│   ├── CreatePlayerModel.cs
│   ├── UpdatePlayerModel.cs
│   ├── CreateTournamentModel.cs
│   ├── UpdateTournamentModel.cs
│   └── BaseResponseModel.cs
│
├── Data/                 # EF Core DbContext (TournamentDbContext.cs, migrations)
├── Program.cs            # App start &amp; Swagger setup
├── MultiLevelTournament.csproj
└── README.md             # This file
</code></pre>

<hr/>

<h2 id="contributing">🤝 Contributing</h2>
<p>1. Fork the repository<br>
2. Create a new branch (<code>git checkout -b feature/YourFeature</code>)<br>
3. Make your changes and commit (<code>git commit -m "Add some feature"</code>)<br>
4. Push to the branch (<code>git push origin feature/YourFeature</code>)<br>
5. Open a Pull Request</p>

<hr/>

<h2 id="license">📜 License</h2>
<p>This project is licensed under the MIT License. See <a href="LICENSE">LICENSE</a> for details.</p>
