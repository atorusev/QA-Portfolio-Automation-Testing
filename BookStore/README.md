# BookStore API Test Automation

This repository contains test automation scripts for the **BookStore API**, developed as part of the Back-End Test Automation module. The BookStore API manages book-related data such as books and categories.

## Features

- Comprehensive **CRUD testing** for Books:
  - Retrieve all books and validate their structure and content.
  - Fetch a book by its title and verify details.
  - Add, update, and delete books with assertions on the API responses.

- **Category Management Tests**:
  - Test the entire lifecycle of a category:
    - Create, retrieve, update, and delete categories.
    - Validate response codes, data integrity, and category details.

## How to Use

1. **Prerequisites**:
   - Install [Node.js](https://nodejs.org/en/download/) for running the API locally.

2. **Setup**:
   - Download and unzip the `BookStore.zip` file.
   - Navigate to the unzipped directory and open a terminal.

3. **Run the API**:
   - Install dependencies: `npm install`
   - Start the API server: `npm run start`
   - Access the API documentation at: [http://localhost:5000/api-docs](http://localhost:5000/api-docs)

4. **Test Execution**:
   - Write test cases inside the provided skeleton (`BookTests.cs` and `CategoryTests.cs`).
   - Use the initialized RestClient (`client`) and authentication token (`token`) for making API requests.
   - Run the test cases to validate the API functionality.

## Example Tests

- **Book Tests**:
  - Verify the API retrieves all books with valid data.
  - Test adding a new book and validating its details.
  - Update and delete books, ensuring appropriate API responses.

- **Category Tests**:
  - Create a category and validate it appears in the list.
  - Update and delete the category, confirming changes via API calls.

