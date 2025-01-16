# BookStore API Test Automation

This folder contains automated tests for the **BookStore API**, showcasing skills in back-end test automation. The API provides functionality for managing books and categories, and this project demonstrates comprehensive testing of its features.

## Project Highlights

### 1. Features Tested
- **Books**:
  - Retrieve all books and validate their structure and content.
  - Fetch books by title and assert detailed content validation.
  - Add, update, and delete books while verifying API responses and data consistency.

- **Categories**:
  - Validate creating, retrieving, updating, and deleting categories.
  - Ensure data integrity and correct lifecycle management.

### 2. Tools & Frameworks Used
- **NUnit**: To structure and execute test cases.
- **RestSharp**: For interacting with the API and making HTTP requests.
- **Newtonsoft.Json**: To parse and validate JSON responses.

### 3. Automated Testing Goals
- Verify proper functionality of CRUD operations for books and categories.
- Ensure robust handling of edge cases, including:
  - Invalid or missing data.
  - API responses for non-existent resources.
  - Correct HTTP status codes for all operations.

### 4. Example Test Scenarios
- Retrieve all books and validate properties like title, author, and price.
- Add a new book, retrieve it by ID, and confirm the data matches.
- Update book details and verify changes through API calls.
- Delete a book and ensure it is no longer accessible.
- Create and manage categories, confirming their availability and integrity.

## Why This Project?
This project demonstrates expertise in back-end test automation, including:
- Designing structured test cases for REST APIs.
- Using modern frameworks for efficient API testing.
- Applying best practices in error handling, assertions, and response validation.

By focusing on real-world scenarios, this project reflects practical, job-ready testing skills for API-driven applications.