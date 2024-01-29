
# Warehouse Management System
### Overview
This project, developed using WPF for the UI, aims to efficiently manage warehouse operations, users, and products.

### Key Features
User Authentication
The implementation of secure password hashing ensures the protection of user credentials, enhancing the overall system security.

### Diverse Product Management
The system accommodates a wide range of products without the need for additional categories, simplifying the product management process.

### Warehouse Operations
The core functionality revolves around the warehouse, tracking user purchases. The establishment of multiple foreign key relationships seamlessly connects users, products, and warehouse operations.

### Tracking System
The introduction of a unique Tracking ID for each purchase, along with five distinct states, provides a comprehensive view of the order's journey through the warehouse. The restriction of state changes to administrators adds an additional layer of control.

### User Experience
The user interface delivers an intuitive experience for both regular users and administrators. Users can effortlessly view the status of their orders and access the complete history of past orders within their personal accounts.

### Administration
The system includes a super-administrator role, empowering the management of other administrators. The admin interface is designed for simplicity, displaying the latest orders and facilitating state changes.

### State Transition Rules
The implementation strictly adheres to the defined state transition rules, ensuring that product states progress sequentially. This maintains the integrity of the order processing system.