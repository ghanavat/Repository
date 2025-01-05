# Ghanavats.Repository
A Versatile Repository Framework for EF Core and Beyond

## Overview
Ghanavats.Repository is a complete repository framework designed to streamline and standardise data access layers in modern applications. Built on best practices and principles of abstraction, it provides a flexible and extensible solution for developers to implement repositories tailored to their needs.

### This solution includes

#### Ghanavats.Repository.Abstractions
A package offering repository interfaces and abstractions to define consistent data access contracts, suitable for any implementation.

#### Ghanavats.Repository
A concrete implementation of the abstractions using Entity Framework Core, offering ready-to-use, production-quality repositories.
With its modular design, Ghanavats.Repository empowers developers to use its components individually or together, depending on their project requirements.

## Features
* **Abstractions for Flexibility:** Define data access contracts with the Ghanavats.Repository.Abstractions package, providing a clear separation of concerns and enabling custom implementations.
* **Entity Framework Core Integration:** Leverage a robust, pre-built repository layer with Ghanavats.Repository, optimised for EF Core.
* **Customizability:** Build your own repository implementations while adhering to a consistent interface.
* **Future-Ready:** The solution is designed to evolve, with plans to add MongoDB support and extend beyond relational databases.

## Getting Started
1. Install the Packages:
    
    Add the desired NuGet packages to your project:
    * For abstractions: `dotnet add package Ghanavats.Repository.Abstractions`
    * For EF Core implementation: `dotnet add package Ghanavats.Repository`

2. Choose Your Approach:
    * Use **Ghanavats.Repository** for EF Core-based projects. 
    * Implement your custom repository logic by referencing **Ghanavats.Repository.Abstractions**.

3. Define Your Domain:
Use the provided interfaces to set up repositories for your domain entities, ensuring a clean, maintainable architecture.

## Future Roadmap
* MongoDB Support: Expanding the framework to include NoSQL database support, providing greater flexibility and enabling hybrid application scenarios.
* Advanced Features: Planned enhancements include custom query builders, caching support, and more.

## Contributing
We welcome contributions to enhance the Ghanavats.Repository framework! 
If you have ideas, suggestions, or code improvements, feel free to create an issue or submit a pull request.
