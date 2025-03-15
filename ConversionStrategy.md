# Strategy for Converting MediatR to gRPC in Parstech.Shop

## Overview

This document outlines the strategy for converting all uses of MediatR in the Parstech.Shop application to use gRPC services instead. The goal is to establish clear separation between the ApiService and Web projects, with communication occurring exclusively through gRPC.

## Step-by-Step Conversion Process

### 1. For Each Feature Area:

1. **Identify MediatR Patterns**:
   - Locate all MediatR requests and handlers in the Application layer
   - Group them by feature/domain area
   - Understand the data flow and operation types (commands/queries)

2. **Create Proto Definitions**:
   - Create or update proto files for each feature area
   - Define request and response messages that mirror the current DTOs
   - Define service methods that correspond to the current MediatR requests

3. **Implement gRPC Services**:
   - Create a gRPC service implementation in ApiService for each feature
   - Service methods should map between gRPC types and domain types
   - Internally use existing MediatR handlers (initially, until they can be refactored)

4. **Create gRPC Clients**:
   - Implement a client class in the Web project for each gRPC service
   - Mirror the interface of the current MediatR requests
   - Handle serialization/deserialization between gRPC and application types

5. **Update Razor Pages**:
   - Replace MediatR injections with gRPC client injections
   - Update method calls to use the gRPC client
   - Handle any differences in response structures

6. **Register Services**:
   - Update ApiService's Program.cs to register the gRPC service
   - Update Web's Program.cs to register the gRPC client

7. **Testing**:
   - Test each converted feature to ensure it works as expected
   - Address any serialization or mapping issues
   - Verify that the UI behavior remains unchanged

### 2. After Initial Conversion:

1. **Refactor ApiService**:
   - Remove direct MediatR dependencies from gRPC services
   - Create proper service layer classes that handle business logic
   - Update gRPC services to use these service classes instead of MediatR

2. **Optimize Proto Files**:
   - Review and consolidate message definitions
   - Ensure consistent naming and structure
   - Implement shared types where appropriate

3. **Performance Optimization**:
   - Implement streaming for large data sets
   - Add caching where appropriate
   - Optimize client implementations

## Feature Areas to Convert

1. **User Management**
   - Authentication and Authorization
   - User Profile
   - User Preferences

2. **Product Management**
   - Product Catalog
   - Product Details
   - Product Gallery
   - Categories
   - Brands

3. **Order Processing**
   - Shopping Cart
   - Checkout
   - Order Status
   - Shipping

4. **Finance**
   - Payments
   - Wallet
   - Transactions
   - Credit Requests

5. **Content Management**
   - Sections
   - Site Settings
   - SEO

## Dependencies and References

- Ensure consistent use of Google.Protobuf
- Add proper references to Grpc.Net.Client packages
- Handle serialization of complex types consistently
- Address validation and error handling uniformly

## Testing and Validation

- Create integration tests for each converted feature
- Test across different environments
- Verify performance under load

## Rollout Strategy

- Convert features incrementally
- Maintain backward compatibility during transition
- Test thoroughly before deploying to production 