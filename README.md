# FleetManagement.Equipment

Microservice for the Fleet Management system.

## Table of Contents

- [Domain Entities Overview](#domain-entities-overview)
- [Local Development & Database Setup](docs/database.md)
- [Cloud Infrastructure (IaC)](docs/infrastructure.md)
- [Containerization](docs/containerization.md)
- [Future Ideas & Enhancements](Docs/Ideas/README.md)

## Domain Entities Overview

The following diagram presents the main domain entities and value objects in the solution. **Only `Car` and `Manufacturer` are persisted in the database**; the rest serve as abstractions or value objects to support the domain model.

```mermaid
classDiagram
	class BaseEntity {
		+Guid Id
		+bool IsActive
		+DateTime CreatedAt
		+string CreatedBy
		+DateTime? UpdatedAt
		+string? UpdatedBy
	}
	class Manufacturer {
		+string Name
		+string Country
		+ICollection~Car~ Cars
	}
	class BaseEquipment {
		+Money InitialValue
		+Money CurrentValue
		+string Title
		+string Description
	}
	class Car {
		+Guid ManufacturerId
		+Manufacturer Manufacturer
		+decimal Mileage
		+int ProductionYear
	}
	class Money {
		+decimal Amount
		+string Currency
	}
	class Percentage {
		+decimal Percent
	}

	BaseEntity <|-- Manufacturer
	BaseEntity <|-- BaseEquipment
	BaseEquipment <|-- Car
	Manufacturer "1" o-- "*" Car : owns
	Car o-- Manufacturer : references
	Car o-- Money : uses
	Car o-- Percentage : uses
	BaseEquipment o-- Money : uses

	style Car fill:#fff,stroke:#333,stroke-width:2px,color:#000
	style Manufacturer fill:#fff,stroke:#333,stroke-width:2px,color:#000
```

**Legend:**
- <span style="background-color:#fff; color:#000; padding:2px 6px; border-radius:3px; border:1px solid #333;">White</span> = Entity persisted in the database
- Other classes are domain abstractions or value objects
