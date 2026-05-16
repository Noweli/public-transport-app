# System architecture of public transport app

Simple public transport management system that considers three user groups: Public User, Company User and Data User.

Public User:
- Should be able to get information about available lanes
- Should be able to get information about available stops (e.g. bus stops)

Company User:
- Should be able to manipulate lane data
- Should be able to manipulate vehicle data
- Should be able to manipulate stops data

Data User:
- Should be able to get application metrics

## Limitations

- Authentication & Authorization to be a simple JWT flow
  - Two roles: User, Admin
  - No refresh tokens
  - No third-party auth providers
  - Simple encryption

## Possible expansion for AWS deployment

- Authentication & Authorization to be held by AWS Cognito
- PostgreSQL as AWS RDS (Aurora)
- AWS ElastiCache for Redis deployment
- AWS CloudFront for WebApp CDN (can include API)

## System, Container & Component Diagram

### System Diagram

<img src="images/SystemContext.png" alt="System Context Diagram">

<br/><br/><br/>

### Container Diagram

<img src="images/ContainerDiagram.png" alt="Container Diagram">

<br/><br/><br/>

### Component Diagram

<img src="images/ComponentDiagram.png" alt="Component Diagram">