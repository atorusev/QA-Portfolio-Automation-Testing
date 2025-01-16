# QA Portfolio - Automation Testing

A collection of my automation testing projects, showcasing expertise in Selenium, NUnit, and other tools for automated testing.

## Projects

### 1. StorySpoiler

StorySpoiler is an interactive platform for sharing and managing spoilers about stories, books, movies, or any narrative content.

**Key Features:**
- **Home Page (Unregistered/Non-Logged Users):**
  - Navigation menu with "LOG IN" and "SIGN UP" options.
  - Sections like "Summarize the Story," "Upload a Picture," and "Ready to Spoil a Story?"

- **Sign Up Page:**
  - User registration with Username, Email, Name, and Password.

- **Log In Page:**
  - Login for registered users with welcome message and spoiler options.

- **Spoiler Management:**
  - Create, edit, and delete story spoilers.
  - View spoilers on the Home Page.

**Automated Testing:**
- **Tools & Frameworks:**
  - Selenium IDE for recording tests.
  - Selenium WebDriver with NUnit for advanced automation.
  - ChromeDriver for browser interaction.

- **Test Cases:**
  - Home Page Navigation Test.
  - Log In and Profile Validation Tests.
  - Create, Edit, and Delete Spoilers.
  - Handling invalid operations (non-existent spoilers).

**Example Automation Scenarios:**
- Verify navigation as an unregistered user.
- Automate user login and validate profile details.
- Test spoiler creation, modification, and deletion workflows.
- Assert proper error handling for invalid operations.

---

### 2. BookStore

BookStore is a REST API for managing an online bookstore, including functionalities for creating, retrieving, updating, and deleting books.

**Key Features:**
- **Book Management:**
  - Add new books to the store with details like title, author, and price.
  - Retrieve books by ID or get a list of all available books.
  - Update existing book details.
  - Delete books from the store.

- **API Endpoints:**
  - `POST /books`: Add a new book.
  - `GET /books/{id}`: Retrieve a specific book by its ID.
  - `PUT /books/{id}`: Update book details.
  - `DELETE /books/{id}`: Remove a book from the store.

**Automated Testing:**
- **Tools & Frameworks:**
  - NUnit for writing unit tests.
  - RestSharp for API interaction and HTTP request handling.

- **Test Cases:**
  - Verify successful book creation and retrieval.
  - Validate proper handling of invalid book IDs (e.g., book not found).
  - Test update and delete operations for existing books.
  - Assert correct API response codes and messages.

**Example Automation Scenarios:**
- Add a new book and validate its details.
- Retrieve a book by ID and ensure data consistency.
- Update a book's details and verify changes.
- Delete a book and confirm it is no longer accessible.

---

With these projects, I aim to demonstrate my practical knowledge of automation testing in both front-end and back-end environments.
