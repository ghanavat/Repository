# Ghanavats Repository Abstractions NuGet Package

Flexible Repository Abstractions for Custom or Base Implementations.

## Overview
The Ghanavats.Repository.Abstractions NuGet package provides a comprehensive set of repository interfaces 
that can be implemented in any project. 

Whether you're building your own repository layer or integrating with existing solutions, 
this package offers the flexibility and structure needed to standardise data access in your application. 
Currently used in Ghanavats.Repository, these abstractions are designed to be adaptable for any use case, 
empowering developers to create custom implementations tailored to their specific needs.

## About Repository Pattern Abstraction
A REPOSITORY lifts a huge burden from the consumer code, 
which can now talk to a simple, intention-revealing interface and ask for what it needs in terms of the model.
The abstraction in this package is straightforward and conceptually connected to the domain model.

Provide REPOSITORIES only for AGGREGATE roots that actually need direct access. 
Keep the client focused on the model, delegating all object storage and access to the REPOSITORIES.
