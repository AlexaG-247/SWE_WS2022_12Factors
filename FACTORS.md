# 12Factors

Master Software Engineering | SWE - Software Engineering | WS2022/23 | Alexandra Gierer - Verband B

---

## I. Codebase

> One codebase tracked in revision control, many deploys

⭕ Implemented

To achieve one common codebase, a github repository was created. It can be accessed via <https://github.com/AlexaG-247/SWE_WS2022_12Factors>

## II. Dependencies

> Explicitly declare and isolate dependencies

⭕ Implemented

All dependencies like AspNet 6.0 are managed in the Dockerfile, and are therefore isolated.

## III. Config

> Store config in the environment

⭕ Implemented

Variables and Configurations like the DB-Connection are stored in the `appsettings.json` file. Due to the minimal size of this project only the DB-Connection string is stored there, but other application specific information can be stored there.

Ports are stored by default in the `Properties/launchSettings.json` file.

For other values like sensitive information environment variables may be considered.

## IV. Backing services

> Treat backing services as attached resources

⭕ Implemented

This was achieved by detaching the database from the rest. The database can be easily changed by adapting the configurations, in this case the connection string in the `appsettings.json` file (and under circumstances adapting the Dockerfile or docker-compose.yaml file).

## V. Build, release, run

> Strictly separate build and run stages

❌ Not Implemented

As no CI/CD pipeline was implemented, this factor was also not implemented. It may be implemented in the Docker image, to separate the release pipeline into multiple steps like the build, release and running stages. This serves the purpose of easily identify release failures.

## VI. Processes

> Execute the app as one or more stateless processes

⭕ Implemented

Due to the Implementation of a REST-Service, this factor was implemented. This REST-Service stores data in a database and not in-memory. Therefore the interactions with the REST-Service are stateless.

## VII. Port binding

> Export services via port binding

⭕ Implemented

Because this project is an ASP.NET project, it binds to a port and therefore this factor is implemented. The configurations of the ports can be adapted in the `Properties/launchSettings.json` file.

## VIII. Concurrency

> Scale out via the process model

⭕ Implemented

As needed the application can be scaled vertically as well as horizontally as needed. This was possible due to the containerization.

## IX. Disposability

> Maximize robustness with fast startup and graceful shutdown

⭕ Implemented

Due to the containerization of this project, this factor was implemented implicitly, as Docker containers can be started and stopped immediately. When shutting down all connections are automatically closed.

## X. Dev/prod parity

> Keep development, staging, and production as similar as possible

❌ Not Implemented

This factor was not implemented as no prod-environment exits. This factor may be implemented by keeping the environments for development, staging and production as close as possible, meaning that no specific Dockerfile for each environments is used and artifacts are build and the released.

## XI. Logs

> Treat logs as event streams

⭕ Implemented

For logging ILogger was added in the `Controllers/ProductController.cs` file.

## XII. Admin processes

> Run admin/management tasks as one-off processes

❌ Not Implemented

As no release pipeline was implemented, this factor was also not considered in this project. But it may be implemented by automatization of one-off-processes in a script as a task, which shutdown automatically, so that no manually execution is needed.
